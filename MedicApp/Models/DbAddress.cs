namespace MedicApp.Models
{
    public class DbAddress
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool? IsDeleted { get; set; }

        public DbAddress()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }


    public class AddressBasicInfo
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; } 
        
    }
}
