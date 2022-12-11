namespace MedicApp.Utils
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public bool IsDeleted { get; set; } 
        public Roles()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
