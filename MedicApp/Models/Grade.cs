namespace MedicApp.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public int Value { get; set; } = 0;
        public Guid Patient_RefId { get; set; }
        public Guid Clinic_RefId { get; set; }
        public bool IsDeleted { get; set; }

        public Grade()
        {
            Id =Guid.NewGuid();
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
