namespace MedicApp.Models
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public String? Name { get; set; }
        public String? Address { get; set; }
        public bool IsDeleted { get; set; }


        public Clinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
