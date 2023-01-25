namespace MedicApp.Models
{
    public class WorkingHours
    {
        public Guid Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
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
        public string Start { get; set; }
        public string End { get; set; }
        public DayOfWeek Day { get; set; }
    }
    public class LoadWorkingHoursModel
    {
        public Guid Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
