using MedicApp.Utils;

namespace MedicApp.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Role Role;
        //public LoyaltyLevel LoyaltyLevel { get; set; }
   
        
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

    }

    public enum Role
    {
        Guest = 0,
        User = 1,
        AdminCenter = 2,
        SysAdmin = 3
    }
}
