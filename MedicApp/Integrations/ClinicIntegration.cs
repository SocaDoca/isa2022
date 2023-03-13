using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        Clinic SaveClinic(ClinicSaveModel clinicSave);
        List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters);
        ClinicLoadModel GetClinicById(Guid Id);

        bool UpdateClinic(ClinicSaveModel updateClinic);

        public Complaint SaveComplaint(Complaint complaint);

        public List<Complaint> LoadAllComplaints();
    }
    public class ClinicIntegration : IClinicIntegration
    {
        public readonly AppDbContext _appDbContext;
        public readonly IWorkingHoursIntegration _workingHoursIntegration;


        public ClinicIntegration(AppDbContext appDbContext, IWorkingHoursIntegration workingHoursIntegration)
        {
            _appDbContext = appDbContext;
            _workingHoursIntegration = workingHoursIntegration;

        }

        public Complaint SaveComplaint(Complaint complaint)
        {
            var dbComplaint = _appDbContext.Complaints.FirstOrDefault(x => x.Id == complaint.Id);
            if (dbComplaint == null)
            {
                dbComplaint = new Complaint();
                _appDbContext.Complaints.Add(dbComplaint);
            }

            dbComplaint.Type = complaint.Type;
            dbComplaint.Description = complaint.Description;
            dbComplaint.Status = complaint.Status;



            _appDbContext.SaveChanges();
            return dbComplaint;
        }

        public Clinic SaveClinic(ClinicSaveModel clinicSave)
        {
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == clinicSave.Id && !x.IsDeleted);
            if (dbClinic == null)
            {
                dbClinic = new Clinic();
                _appDbContext.Clinics.Add(dbClinic);
            }

            dbClinic.Name = clinicSave.Name;
            dbClinic.Address = clinicSave.Address;
            dbClinic.City = clinicSave.City;
            dbClinic.Country = clinicSave.Country;
            dbClinic.Description = clinicSave.Description;
            dbClinic.Phone = clinicSave.Phone;
            dbClinic.Rating = clinicSave.Rating;

            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => !x.IsDeleted && x.Clinic_RefID == dbClinic.Id).ToList();
            var dbWorkingHoursIds = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();

            foreach (var item in clinicSave.WorkHours)
            {
                var workingHours = new WorkingHours();
                if (!dbWorkingHoursIds.Contains(item.Id))
                {
                    workingHours = _workingHoursIntegration.SaveWorkingHours(item);
                    var dbClinic2WorkingHours = new Clinic2WorkingHours
                    {
                        Clinic_RefID = dbClinic.Id,
                        WorkingHours_RefID = workingHours.Id
                    };

                    _appDbContext.Clinic2WorkingHours.Add(dbClinic2WorkingHours);
                    _appDbContext.SaveChanges();
                }
                else
                {
                    workingHours = _workingHoursIntegration.LoadDBWorkingHourById(item.Id);
                    workingHours.Start = item.Start;
                    workingHours.End = item.End;
                    workingHours.DayOfWeek = item.Day;

                    _appDbContext.WorkingHours.Update(workingHours);
                }
            }

            _appDbContext.SaveChanges();
            return dbClinic;
        }

        public List<Complaint> LoadAllComplaints()
        {
            var dbComplaint = _appDbContext.Complaints.FirstOrDefault();
            List<Complaint> resultList = new List<Complaint>();

                var complaintModel = new Complaint
                {
                    Id = dbComplaint.Id,
                    Description = dbComplaint.Description,
                    Type = dbComplaint.Type,
                    Status = dbComplaint.Status,

                };
            resultList.Add(complaintModel);
            return resultList.ToList();
        }

        public List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters)
        {
            List<Clinic> dbClinics = _appDbContext.Clinics.ToList();
            List<ClinicList> resultList = new List<ClinicList>();
            var dbPatient = _appDbContext.Users.Where(x => x.IsDeleted == false).ToList();
            var dbWorkingHours = _appDbContext.WorkingHours.ToList();
            var clinic2Appointments = _appDbContext.Appointment2Clinics.ToList().GroupBy(x => x.Clinic_RefID).ToDictionary(x => x.Key, x => x.Select(s => s.Appointment_RefID).ToList());
            var dbAppointments = _appDbContext.Appointments.Where(x => !x.IsDeleted).ToList();
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.ToList().Where(x => !x.IsDeleted)
                .GroupBy(x => x.Clinic_RefID)
                .ToDictionary(x => x.Key, x => x.Select(x => x.WorkingHours_RefID).ToList());
            foreach (var clinic in dbClinics)
            {
                var workHoursList = new List<LoadWorkingHoursModel>();
                var clinicModel = new ClinicList
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = clinic.Address,
                    City = clinic.City,
                    Country = clinic.Country,
                    Phone = clinic.Phone,
                    Description = clinic.Description,
                    Rating = clinic.Rating,
                };
                if (clinic2WorkingHours.ContainsKey(clinicModel.Id))
                {
                    if (clinic2WorkingHours.TryGetValue(clinic.Id, out var workHoursListIds))
                    {
                        if (workHoursListIds.Any() && dbWorkingHours.Any() && dbWorkingHours != null)
                        {
                            foreach (var workDayId in workHoursListIds)
                            {
                                var dbWorkHour = dbWorkingHours.FirstOrDefault(x => x.Id == workDayId);
                                var model = new LoadWorkingHoursModel
                                {
                                    Id = workDayId,
                                    Start = dbWorkHour.Start,
                                    Day = dbWorkHour.DayOfWeek,
                                    End = dbWorkHour.End,
                                };
                                workHoursList.Add(model);
                            }
                            clinicModel.WorkingHours = workHoursList;
                        }

                    }
                }

                resultList.Add(clinicModel);
            }


            #region SEARCH
            if (!String.IsNullOrEmpty(parameters.SearchCriteria))
                resultList = resultList.Where(
                    x => x.Name.Contains(parameters.SearchCriteria) ||
                        x.Address.Contains(parameters.SearchCriteria) ||
                        x.City.Contains(parameters.SearchCriteria) ||

                        x.Country.Contains(parameters.SearchCriteria)

                         ).ToList();


            #endregion


            #region FILTER

            if (parameters.ClinicFilterData != null)
            {
                if (parameters.ClinicFilterData.Country != null)
                {
                    resultList = resultList.Where(x => x.Country.ToLower().Contains(parameters.ClinicFilterData.Country.ToLower())).ToList();
                }
                if (parameters.ClinicFilterData.Name != null)
                {
                    resultList = resultList.Where(x => x.Name.ToLower().Contains(parameters.ClinicFilterData.Name.ToLower())).ToList();
                }
                if (parameters.ClinicFilterData.City != null)
                {
                    resultList = resultList.Where(x => x.City.ToLower().Contains(parameters.ClinicFilterData.City.ToLower())).ToList();
                }
            }

            #endregion

            #region SORT
            switch (parameters.SortBy)
            {
                case "name":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.Name.ToLower()).ToList() : resultList.OrderByDescending(x => x.Name.ToLower()).ToList();
                    break;

                case "city":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.City.ToLower()).ToList() : resultList.OrderByDescending(x => x.City.ToLower()).ToList();
                    break;
                case "country":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.Country.ToLower()).ToList() : resultList.OrderByDescending(x => x.Country.ToLower()).ToList();
                    break;
            }
            #endregion

            return resultList.Skip(parameters.Offset).Take(parameters.Limit).ToList();
        }

        public ClinicLoadModel GetClinicById(Guid Id)
        {
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
            var clinic2Appointment = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == dbClinic.Id).ToList();
            var appointmentIDs = clinic2Appointment.Select(x => x.Appointment_RefID).ToList();
            var clinicAppointment = _appDbContext.Appointments.Where(x => appointmentIDs.Contains(x.Id)).ToList();
            var WorkingHoursIds = _appDbContext.Clinic2WorkingHours.ToList()
                .Where(x => !x.IsDeleted && x.Clinic_RefID == dbClinic.Id)
                .Select(x => x.WorkingHours_RefID).ToList();
            var workingHours = _appDbContext.WorkingHours.Where(x => x.IsDeleted == false).ToList();
            var workingHoursList = new List<LoadWorkingHoursModel>();

            if (dbClinic == null)
            {
                throw new KeyNotFoundException("Clinic does not exist");
            }

            if (WorkingHoursIds.Any())
            {
                foreach (var id in WorkingHoursIds)
                {
                    var workH = workingHours.FirstOrDefault(x => x.Id == id);
                    var model = new LoadWorkingHoursModel
                    {
                        Day = workH.DayOfWeek,
                        End = workH.End,
                        Id = workH.Id,
                        Start = workH.Start,
                    };
                    workingHoursList.Add(model);
                }
            }
            var appointmentList = new List<Appointment>();
            if (clinicAppointment.Any())
            {
                foreach (var item in clinicAppointment)
                {
                    appointmentList.Add(item);
                }
            }

            var result = new ClinicLoadModel
            {
                Id = dbClinic.Id,
                Address = dbClinic.Address,
                City = dbClinic.City,
                Country = dbClinic.Country,
                Description = dbClinic.Description,
                Name = dbClinic.Name,
                Phone = dbClinic.Phone,
                Rating = dbClinic.Rating,
                WorkHours = workingHoursList,
                Appointments = appointmentList,

            };

            return result;
        }

        #region Update 
        public bool UpdateClinic(ClinicSaveModel updateClinic)
        {
            var getClinic = _appDbContext.Clinics.First(x => x.Id == updateClinic.Id);

            if (getClinic == null)
            {
                return false;
            }
            getClinic.Name = updateClinic.Name;
            getClinic.Description = updateClinic.Description;
            getClinic.Address = updateClinic.Address;
            getClinic.City = updateClinic.City;
            getClinic.Country = updateClinic.Country;
            getClinic.Phone = updateClinic.Phone;
            getClinic.Rating = updateClinic.Rating;

            var dbWorkingHoursIds = _appDbContext.Clinic2WorkingHours.ToList()
                .Where(x => !x.IsDeleted && x.Clinic_RefID == getClinic.Id)
                .Select(x => x.WorkingHours_RefID)
                .ToList();
            var dbWorkingHours = _appDbContext.WorkingHours.Where(x => dbWorkingHoursIds.Any(s => s == x.Id)).ToList();
            foreach (var item in updateClinic.WorkHours)
            {
                var workHour = dbWorkingHours.First(x => x.Id == item.Id);
                workHour.Start = item.Start;
                workHour.End = item.End;
                workHour.DayOfWeek = item.Day;

                _appDbContext.WorkingHours.Update(workHour);
                _appDbContext.SaveChanges();
            }

            _appDbContext.Clinics.Update(getClinic);
            _appDbContext.SaveChanges();
            return true;
        }

    }
    #endregion

}
