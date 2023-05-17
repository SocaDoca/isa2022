namespace MedicApp.Models
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime Creation_TimeStamp { get; set; }
        public Roles()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
        }
    }


    public class SaveRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class LoadRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
