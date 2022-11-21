namespace MedicApp.Models
{
    public class WorkingHours
    {
        public Guid Id { get; set; }    
        public DateTime WorkStart { get; set; }
        public double WorkDuration { get; set; }
        
    }
}
