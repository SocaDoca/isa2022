using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClinicController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IClinicIntegration _clinicIntegration;
        private readonly UserManager<IdentityUser> _userManager;
        public ClinicController(AppDbContext appDbContext, IClinicIntegration clinicIntegration, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _clinicIntegration = clinicIntegration;

        }
        [Authorize(Roles = "AdminCenter")]
        [HttpGet("save-clinic")]
        public async Task<Guid> SaveClinic(ClinicSaveModel clinic)
        {
            return await _clinicIntegration.SaveClinic(clinic);
        }

        [HttpPost("load-clinic-by-id")]
        public ClinicSaveModel LoadClinicById([FromBody] Guid id)
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
