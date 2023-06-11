namespace MedicApp.Models.RequestModels
{
    public class SaveAppointmentRequest
    {
        public Guid appointmentId { get; set; }
        public ReportSaveModel report { get; set; }
    }

    public class ReserveAppointmentRequest
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
    }
}
