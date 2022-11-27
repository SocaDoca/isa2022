using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicController : AbastractController
    {
        private readonly AppDbContext _appDbContext;
        private readonly IClinicIntegration _clinicIntegration;
        public ClinicController(AppDbContext appDbContext, IClinicIntegration clinicIntegration)
        {

            _appDbContext = appDbContext;
            _clinicIntegration = clinicIntegration;

        }

        [HttpGet("/save-clinic")]
        public DbClinic SaveClinic(ClinicSaveModel clinic)
        {
            return _clinicIntegration.SaveClinic(clinic);
        }

        [HttpPost("/load-clinic-by-id")]
        public ClinicSaveModel LoadClinicById(Guid id)
        {
            return _clinicIntegration.LoadClinicById(id);
        }
        //[HttpGet("/load-clinics")]
        //public async Task<ClinicListModel> LoadAllClinics()
        //{
        //    return await _clinicIntegration.LoadAllClinics();
        //}
    }


}
