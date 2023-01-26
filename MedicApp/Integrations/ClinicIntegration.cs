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


        public List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters)
        {
            List<Clinic> dbClinics = _appDbContext.Clinics.ToList();
            List<ClinicList> resultList = new List<ClinicList>();
            var workHoursList = new List<LoadWorkingHoursModel>();
            var dbWorkingHours = _appDbContext.WorkingHours.ToList();
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.ToList().Where(x => !x.IsDeleted)
                .GroupBy(x => x.Clinic_RefID)
                .ToDictionary(x => x.Key, x => x.Select(x => x.WorkingHours_RefID).ToList());
            foreach (var clinic in dbClinics)
            {
                var clinicModel = new ClinicList
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = clinic.Address, 
                    City =clinic.City, 
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
                        x.Country.Contains(parameters.SearchCriteria) ||
                        
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
            var clinicAppointment = _appDbContext.Appointments.Where(x => clinic2Appointment.Any(s => s.Appointment_RefID == x.Id)).ToList();
            var clinic2WorkingHoursIds = _appDbContext.Clinic2WorkingHours.Where(x => !x.IsDeleted && x.Clinic_RefID == dbClinic.Id).Select(x => x.WorkingHours_RefID).ToList();
            var workingHoursList = new List<LoadWorkingHoursModel>();

            if (dbClinic == null)
            {
                throw new KeyNotFoundException("Clinic does not exist");
            }

            if (clinic2WorkingHoursIds.Any())
            {
                foreach (var id in clinic2WorkingHoursIds)
                {
                    workingHoursList.Add(_workingHoursIntegration.LoadWorkingHourById(Id));
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

            _appDbContext.Clinics.Update(getClinic);
            _appDbContext.SaveChanges();
            return true;
        }

    }
    #endregion

}
