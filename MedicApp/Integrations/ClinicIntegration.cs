using MedicApp.Database;
using MedicApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        Task<Clinic> SaveClinic(ClinicSaveModel clinicSave);
    }
    public class ClinicIntegration : IClinicIntegration
    {
        public readonly AppDbContext _appDbContext;
        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<Clinic> SaveClinic(ClinicSaveModel clinicSave)
        {

            var dbClinic = await _appDbContext.Clinics.FirstOrDefaultAsync(x => x.Id == clinicSave.Id && !x.IsDeleted);
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


            _appDbContext.SaveChanges();
            return dbClinic;
        }


        public List<ClinicList> LoadAllCustomers(ClinicLoadParameters parameters)
        {
            List<Clinic> dbClinics = _appDbContext.Clinics.ToList();
            List<ClinicList> resultList = new List<ClinicList>();
            foreach (var clinic in dbClinics)
            {
                if (dbClinics != null && dbClinics.Any())
                {
                    var customerModel = new ClinicList
                    {
                        Id = clinic.Id,
                        Name = clinic.Name,
                        Address = String.Format("{0}, {1}, {2}" , clinic.Address, clinic.City, clinic.Country),
                        Phone = clinic.Phone,
                        Description = clinic.Description,
                        Rating = clinic.Rating,
                      

                    };
                    resultList.Add(customerModel);
                }
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
        public string City { get;set; }
        public string Country { get; set; }
    }



}
