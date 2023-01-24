namespace MedicApp.Models
{
    public class WorkingHours
    {
        public Guid Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsDeleted { get; set; }

        public WorkingHours()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;            
        }

    }

    public class SaveWorkingHoursModel
    {
        public Guid Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public DayOfWeek Day { get; set; }
    }
    public class LoadWorkingHoursModel
    {
        public Guid Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
