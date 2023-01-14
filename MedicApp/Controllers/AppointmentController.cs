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
        public Appointment SaveAppointment(AppointmentSaveModel appotinmentSave)
        {
            return _appointmentIntegration.SaveAppointment(appotinmentSave);
        }
    }
}
