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
        public List<ClinicList> LoadAllClinics([FromBody] ClinicLoadParameters parameters)
        {
            return _clinicIntegration.LoadAllClinics(parameters);
        }

        [HttpPost("update-clinic")]
        public bool UpdateClinic([FromBody] ClinicSaveModel updateClinic)
        {
            return _clinicIntegration.UpdateClinic(updateClinic);
        }

        [HttpPost("get-clinic-by-id")]
        public ClinicLoadModel GetClinicById(Guid Id)
        {
            return _clinicIntegration.GetClinicById(Id);
        }

        [HttpPost("save-complaint")]
        public Complaint SaveComplaint(Complaint complaint)
        {
            return _clinicIntegration.SaveComplaint(complaint);
        }

        [HttpGet("load-all-complaints")]
        public List<Complaint> LoadAllComplaints()
        {
            return _clinicIntegration.LoadAllComplaints();
        }

    }
}
