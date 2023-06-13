using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Models;
using MedicApp.Models.RequestModels;
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
        public bool ReserveAppointment(ReserveAppointmentRequest parameters)
        {
             return _appointmentIntegration.ReserveAppointment(parameters);
        }
        [HttpPost("reserve-predefiend-appointment")]
        public List<LoadPredefiendAppointment> LoadPredefiendAppointments(Guid clinicId)
        {
             return _appointmentIntegration.LoadPredefiendAppointments(clinicId);
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

        [HttpPost("cancel-appointment-by-user")]
        public bool CancelAppointmentByUser(Guid appointmenetId)
        {
             return _appointmentIntegration.CancelAppointmentByUser(appointmenetId);           
        }
        [HttpPost("cancel-appointment-by-admin")]
        public bool CancelAppointmentByAdmin(Guid appointmenetId)
        {
            return _appointmentIntegration.CancelAppointmentByAdmin(appointmenetId);           
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
