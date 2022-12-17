using MedicApp.Enums;

namespace MedicApp.Models
{
    public class DbAccount
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }   
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPasswordConfirmed { get; set; }

        public DbAccount()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            IsActive = false;
            IsPasswordConfirmed = false;
        }

        
    }

    public class AccountDto
    {
        public string Username { get; set;} = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }

    }

}
