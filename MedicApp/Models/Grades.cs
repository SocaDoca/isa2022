namespace MedicApp.Models
{
    public class Grades
    {
        public Guid Id { get; set; }
        public int Value { get; set; } = 0;
        public Guid Patient_RefId { get; set; }
        public Guid Clinic_RefId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Grades()
        {
            Id =Guid.NewGuid();
            Creation_TimeStamp = DateTime.Now;
            IsDeleted = false;
        }
    }

    public class SaveGrade
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Guid Patient_RefId { get; set; }
        public Guid Clinic_RefId { get; set; }
    }
}
