namespace MedicApp.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }


        public AccountType AccountType { get; set; } 
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }


    }

    public enum AccountType
    {
        Guest = 0,
        Admin = 1,
        Employee = 2,
        User = 3
    }
}
