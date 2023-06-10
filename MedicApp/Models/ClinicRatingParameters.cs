namespace MedicApp.Models
{
    public class ClinicRatingParameters
    {
        public Guid ClinicId { get; set; }
        public Guid PatientId { get; set; }
        public double Rating { get; set; }
    }
}
