using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
    public class ClinicController : ControllerBase
    {  private readonly IClinicIntegration _clinicIntegration;

        public ClinicController(IClinicIntegration clinicIntegration)
        {
            _clinicIntegration = clinicIntegration;
        }

        [HttpPost("save-clinic")]
        public Clinic SaveClinic([FromBody] ClinicSaveModel clinic)
        {
            return _clinicIntegration.SaveClinic(clinic);
        }

        [HttpPost("load-all-clinics")]
        public async Task<IActionResult> LoadAllClinics([FromBody] ClinicLoadParameters parameters)
        {
            return Ok(_clinicIntegration.LoadAllClinics(parameters));
        }

        [HttpPost("update-clinic")]
        public bool UpdateClinic([FromBody] ClinicSaveModel updateClinic)
        {
            return _clinicIntegration.UpdateClinic(updateClinic);
        }



    }
}
