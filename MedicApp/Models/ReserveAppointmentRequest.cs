namespace MedicApp.Models
{
    public class ReserveAppointmentRequest
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
    }
}
