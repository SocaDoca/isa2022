﻿using MedicApp.Database;
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
        public bool SaveAppointment(Guid appointmentId, ReportSaveModel report)
        {
            return _appointmentIntegration.SaveAppointment(appointmentId, report);
        }
        
        [HttpPost("create-predefiend-appointment")]
        public List<Appointment> CreatePredefiendAppointment(SavePredefiendAppointment appotinmentSave)
        {
            return _appointmentIntegration.CreatePredefiendAppointments(appotinmentSave);
        }
        [HttpPost("reserve-predefiend-appointment")]
        public bool ReserveAppointment(Guid appotinmentId, Guid patientId)
        {
            return _appointmentIntegration.ReserveAppointment(appotinmentId,patientId);
        }
        [HttpPost("start-predefiend-appointment")]
        public bool StartAppointment(Guid appotinmentId)
        {
            return _appointmentIntegration.StartAppointmnet(appotinmentId);
        }
        
        [HttpPost("finish-predefiend-appointment")]
        public bool FinishAppointment(Guid appotinmentId)
        {
            return _appointmentIntegration.FinishAppointment(appotinmentId);
        }

        [HttpPost("cancel-appointment")]
        public bool CancelAppointment(Guid appointmenetId)
        {
            return _appointmentIntegration.CancelAppointment(appointmenetId);           
        }

        [HttpGet("get-appointment")]
        public AppointmentLoadModel GetAppointmentById(Guid Id)
        {
            return _appointmentIntegration.LoadAppointmentById(Id);
        } 
        [HttpGet("patient/appointmets")]
        public List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId)
        {
            return _appointmentIntegration.LoadAllAppointmentsByPatientId(patientId);
        }

        [HttpGet("clinic/appointmets")]
        public List<AppointmentLoadModel> LoadAllAppointmentsByClinicId(Guid clinicId)
        {
            return _appointmentIntegration.LoadAllAppointmnetsByClinicId(clinicId);
        }

    }
}
