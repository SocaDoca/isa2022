using MedicApp.Controllers;
using MedicApp.Database;
using MedicApp.Models;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        Task<ClinicListModel> LoadAllClinics(Guid id);
    }
    public class ClinicIntegration
    {
        private readonly AppDbContext _appDbContext;
        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<ClinicListModel>> LoadAllClinics()
        {
            var dbClinics = await _appDbContext.Clinics.Where(clinic => clinic.IsDeleted == false).ToList();
            var returnList = new List<ClinicListModel>();
            if(dbClinics.Any() && dbClinics is not null)
            {
                foreach(var clinic in dbClinics)
                {
                    var model = new ClinicListModel()
                    {
                        Id = clinic.Id,
                        Capacity = clinic.Capacity,
                        Name = clinic.Name ?? string.Empty,
                        Description = clinic.Description ?? string.Empty,
                    };
                    returnList.Add(model);
                }
            }
            return returnList;
        }

        #region Sort
        private List<ClinicListModel> Sort(List<ClinicListModel> clinics , string sortBy , bool sortAscending)
        {

            switch (sortBy)
            {
                case "name":
                    clinics = sortAscending ?
                         clinics.OrderBy(x => x.Name.ToLower()).ToList() : clinics.OrderByDescending(x => x.Name.ToLower()).ToList();
                    break;

                case "rating":
                    clinics = sortAscending ?
                          clinics.OrderBy(x => x.Name.ToLower()).ToList() : clinics.OrderByDescending(x => x.Name.ToLower()).ToList();
                    break;

                case "address":
                    clinics = sortAscending ?
                         clinics.OrderBy(x => x.Name.ToLower()).ToList() : clinics.OrderByDescending(x => x.Name.ToLower()).ToList();
                    break;
            }

            return clinics; 
        }
        #endregion
    }
}
