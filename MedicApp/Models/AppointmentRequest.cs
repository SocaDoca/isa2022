namespace MedicApp.Models
{
    public class SaveAppointmentRequest
    {
        public Guid appointmentId { get; set; }
        public ReportSaveModel report { get; set; }
    }
}
