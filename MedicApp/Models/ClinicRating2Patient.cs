namespace MedicApp.Models
{
    public class ClinicRating2Patient
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; }
        public Guid PatientId { get; set; }
        public double Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_Timestamp { get; set; }
    }
}
