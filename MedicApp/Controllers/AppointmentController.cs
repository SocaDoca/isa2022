using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentIntegration _appointmentIntegration;


        public AppointmentController(IAppointmentIntegration appointmentIntegration)
        {
            _appointmentIntegration = appointmentIntegration;
        }

        [HttpPost("save-appointment")]
        public bool SaveAppointment([FromBody] SaveAppointmentRequest parameters )
        {
            return _appointmentIntegration.SaveAppointmentReport(parameters);
        }
        
        [HttpPost("create-predefiend-appointment")]
        public List<Appointment> CreatePredefiendAppointment(SavePredefiendAppointment appotinmentSave)
        {
            return _appointmentIntegration.CreatePredefiendAppointments(appotinmentSave);
        }
        [HttpPost("reserve-predefiend-appointment")]
        public void ReserveAppointment(Guid appotinmentId, Guid patientId)
        {
             _appointmentIntegration.ReserveAppointment(appotinmentId,patientId);
        }
        [HttpPost("start-predefiend-appointment")]
        public void StartAppointment(Guid appotinmentId)
        {
             _appointmentIntegration.StartAppointmnet(appotinmentId);
        }
        
        [HttpPost("finish-predefiend-appointment")]
        public void FinishAppointment(Guid appotinmentId)
        {
             _appointmentIntegration.FinishAppointment(appotinmentId);
        }

        [HttpPost("cancel-appointment")]
        public void CancelAppointment(Guid appointmenetId)
        {
             _appointmentIntegration.CancelAppointment(appointmenetId);           
        }

        [HttpPost("get-appointment")]
        public AppointmentLoadModel GetAppointmentById([FromBody]Guid Id)
        {
            return _appointmentIntegration.LoadAppointmentById(Id);
        } 
        [HttpPost("patient/appointmets")]
        public List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId)
        {
            return _appointmentIntegration.LoadAllAppointmentsByPatientId(patientId);
        }

        [HttpGet("clinic/appointmets")]
        public List<AppointmentLoadModel> LoadAllAppointmentsByClinicId(Guid clinicId)
        {
            return _appointmentIntegration.LoadAllAppointmnetsByClinicId(clinicId);
        }
        [HttpGet("load-reserved-appointments")]
        public List<AppointmentLoadModel> LoadAllReservedAppointmnets()
        {
            return _appointmentIntegration.LoadAllReservedAppointmnets();
        }
    }
}
