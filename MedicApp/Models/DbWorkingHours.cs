namespace MedicApp.Models
{
    public class DbWorkingHours
    {
        public Guid Id { get; set; }    
        public DateTime WorkStart { get; set; }
        public double WorkDuration { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        
    }
}
