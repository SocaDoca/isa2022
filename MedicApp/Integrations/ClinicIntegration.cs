﻿using MedicApp.Database;
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
        List<ClinicBasicInfo> LoadClinicBasicInfoByIds(List<Guid> clinicIds);
        bool UpdateClinic(ClinicSaveModel updateClinic);
        Complaints SaveComplaint(Complaints complaint);
        List<Complaints> LoadAllComplaints();
        
    }
    public class ClinicIntegration : IClinicIntegration
    {
        public readonly AppDbContext _appDbContext;


        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            dbClinic.WorksFrom = clinicSave.WorksFrom;
            dbClinic.WorksTo = clinicSave.WorksTo;

            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => !x.IsDeleted && x.Clinic_RefID == dbClinic.Id).ToList();
            var dbWorkingHoursIds = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();            

            _appDbContext.SaveChanges();
            return dbClinic;
        }

        public List<Complaints> LoadAllComplaints()
        {
            var dbComplaints = _appDbContext.Complaints.Where(x => x.IsDeleted == false);
            return dbComplaints.ToList();
        }

        public List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters)
        {
            List<Clinic> dbClinics = _appDbContext.Clinics.ToList();
            List<ClinicList> resultList = new List<ClinicList>();
            var dbPatient = _appDbContext.Users.Where(x => x.IsDeleted == false).ToList();
            var clinic2Appointments = _appDbContext.Appointment2Clinics.ToList().GroupBy(x => x.Clinic_RefID).ToDictionary(x => x.Key, x => x.Select(s => s.Appointment_RefID).ToList());
            var dbAppointments = _appDbContext.Appointments.Where(x => !x.IsDeleted).ToList();
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
                    City = clinic.City,
                    Country = clinic.Country,
                    Phone = clinic.Phone,
                    Description = clinic.Description,
                    Rating = clinic.Rating,
                    WorksFrom = clinic.WorksFrom,
                    WorksTo = clinic.WorksTo,
                };
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

        public Complaints SaveComplaint(Complaints complaint)
        {
            var dbComplaint = _appDbContext.Complaints.Where(x => x.IsDeleted == false && x.Id == complaint.Id).FirstOrDefault();
            if(dbComplaint == null)
            {
                dbComplaint = new Complaints();
            }
            dbComplaint.Answer = complaint.Answer;
            dbComplaint.IsAnswered = complaint.IsAnswered;
            dbComplaint.IsForClinic = complaint.IsForClinic;
            dbComplaint.IsForEmployee = complaint.IsForEmployee;
            dbComplaint.UserInput = complaint.UserInput;

            _appDbContext.Complaints.Add(dbComplaint);
            _appDbContext.SaveChanges();
            return dbComplaint;
        }

        public ClinicLoadModel GetClinicById(Guid Id)
        {
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
            var clinic2Appointment = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == dbClinic.Id).ToList();
            var appointmentIDs = clinic2Appointment.Select(x => x.Appointment_RefID).ToList();
            var clinicAppointment = _appDbContext.Appointments.Where(x => appointmentIDs.Contains(x.Id)).ToList();          

            if (dbClinic == null)
            {
                throw new KeyNotFoundException("Clinic does not exist");
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
                WorksFrom = dbClinic.WorksFrom,
                WorksTo  = dbClinic.WorksTo,
                Description = dbClinic.Description,
                Name = dbClinic.Name,
                Phone = dbClinic.Phone,
                Rating = dbClinic.Rating,
                Appointments = appointmentList,

            };

            return result;
        }

        public List<ClinicBasicInfo> LoadClinicBasicInfoByIds(List<Guid> clinicIds)
        {
            var dbClinics = _appDbContext.Clinics.Where(x => clinicIds.Any(Id => x.Id == Id) && !x.IsDeleted).ToList();
            return dbClinics.Select(clinc => new ClinicBasicInfo
            {
                Id = clinc.Id,
                Address = clinc.Address,
                City = clinc.City,
                Country = clinc.Country,
                Description = clinc.Description,
                Name = clinc.Name,
                Phone = clinc.Phone,
                WorksFrom = clinc.WorksFrom,
                WorksTo = clinc.WorksTo
            }).ToList();
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
            getClinic.WorksFrom = updateClinic.WorksFrom;
            getClinic.WorksTo = updateClinic.WorksTo;

            _appDbContext.Clinics.Update(getClinic);
            _appDbContext.SaveChanges();
            return true;
        }

    }
    #endregion

}
