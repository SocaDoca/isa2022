namespace MedicApp.Models
{
    public class DbWorkingHours
    {
        public Guid Id { get; set; }
        public DateTime WorkStart { get; set; }
        public int WorkDuration { get; set; }
        public WorkingDay WorkDay { get; set; }
        public bool IsDeleted { get; set; } 

        public DbWorkingHours()
        {
            Id = Guid.NewGuid();
        }        
    }

    public class WorkingHoursBasicInfo
    {
        public Guid Id { get; set; }
        public DateTime WorkStart { get; set; }
        public int Duration { get; set; }
        public WorkingDay WorkingDay { get; set; }
    }
    public enum WorkingDay
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7,
    }
}
