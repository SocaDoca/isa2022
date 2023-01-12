namespace MedicApp.Models
{
    public class WorkingHours
    {
        public Guid Id { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
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
        public int Start { get; set; }
        public int End { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
    }
}
