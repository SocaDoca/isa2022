using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        Task<Clinic> SaveClinic(ClinicSaveModel clinicSave);
        List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters);
    }
    public class ClinicIntegration : IClinicIntegration
    {
        public readonly AppDbContext _appDbContext;
        public readonly IWorkingHoursIntegration _workingHoursIntegration;

        public ClinicIntegration(AppDbContext appDbContext , IWorkingHoursIntegration workingHoursIntegration)
        {
            _appDbContext = appDbContext;
            _workingHoursIntegration = workingHoursIntegration;
        }


        public async Task<Clinic> SaveClinic(ClinicSaveModel clinicSave)
        {
            var dbClinic = await _appDbContext.Clinics.FirstOrDefaultAsync(x => x.Id == clinicSave.Id && !x.IsDeleted);
            if (dbClinic == null)
            {
                dbClinic = new Clinic();
                _appDbContext.Clinics.Add(dbClinic);
            }
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => !x.IsDeleted && x.Clinic_RefID == dbClinic.Id).ToList();
            var dbWorkingHoursIds = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();
            

            dbClinic.Name = clinicSave.Name;
            dbClinic.Address = clinicSave.Address;
            dbClinic.City = clinicSave.City;
            dbClinic.Country = clinicSave.Country;
            dbClinic.Description = clinicSave.Description;
            dbClinic.Phone = clinicSave.Phone;
            dbClinic.Rating = clinicSave.Rating;

            foreach (var item in clinicSave.WorkHours)
            {
                if(!dbWorkingHoursIds.Contains(item.Id))
                {
                    var workHours = _workingHoursIntegration.SaveWorkingHours(item);
                    var dbClinic2WorkingHours = new Clinic2WorkingHours
                    {
                        Clinic_RefID = dbClinic.Id,
                        WorkingHours_RefID = workHours.Id
                    };

                    _appDbContext.Clinic2WorkingHours.Add(dbClinic2WorkingHours);
                }
                var workingHour = _workingHoursIntegration.LoadWorkingHourById(item.Id);
                workingHour.Start = item.Start;
                workingHour.End = item.End;
                workingHour.IsMonday = item.IsMonday;
                workingHour.IsTuesday = item.IsTuesday;
                workingHour.IsWednesday = item.IsWednesday;
                workingHour.IsThursday = item.IsThursday;
                workingHour.IsFriday = item.IsFriday;
                workingHour.IsSaturday = item.IsSaturday;

                _appDbContext.WorkingHours.Update(workingHour);          
         
            }

            _appDbContext.SaveChanges();
            return dbClinic;
        }


        public List<ClinicList> LoadAllClinics(ClinicLoadParameters parameters)
        {
            List<Clinic> dbClinics = _appDbContext.Clinics.ToList();
            List<ClinicList> resultList = new List<ClinicList>();
            var dbWorkingHours = _appDbContext.WorkingHours.ToList();
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Clinic_RefID)
                .ToDictionary(x => x.Key, x => x.Select(x => x.WorkingHours_RefID).ToList());
            foreach (var clinic in dbClinics)
            {
                var clinicModel = new ClinicList
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = String.Format("{0}, {1}, {2}", clinic.Address, clinic.City, clinic.Country),
                    Phone = clinic.Phone,
                    Description = clinic.Description,
                    Rating = clinic.Rating,
                };
                if (clinic2WorkingHours.ContainsKey(clinicModel.Id))
                {
                   if(clinic2WorkingHours.TryGetValue(clinic.Id , out var workHoursList))
                    {
                        if (workHoursList.Any() && dbWorkingHours.Any() && dbWorkingHours != null)
                        {
                            foreach (var workDay in workHoursList)
                            {
                                clinicModel.WorkingHours.Add(dbWorkingHours?.FirstOrDefault(x => x.Id == workDay));
                            }
                        }
                    }
                }

                resultList.Add(clinicModel);
            }


            #region SEARCH
            if (!String.IsNullOrEmpty(parameters.SearchCriteria))
                resultList = resultList.Where(
                    x => x.Name.Contains(parameters.SearchCriteria) ||
                         x.City.Contains(parameters.SearchCriteria) ||
                         x.Country.Contains(parameters.SearchCriteria) ||
                         x.Address.Contains(parameters.SearchCriteria)).ToList();

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

        
    }


    public class ClinicLoadParameters
    {
        public string SearchCriteria { get; set; }
        public ClinicFilterData ClinicFilterData { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public bool OrderAsc { get; set; }

    }

    public class ClinicFilterData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }



}
