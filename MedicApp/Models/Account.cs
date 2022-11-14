namespace MedicApp.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public LoyaltyLevel LoyaltyLevel { get; set; }
   
        
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }

    public enum LoyaltyLevel
    {
        Silver = 0,
        Gold = 1,
        Platinum = 2,
        Diamond = 3,
    }

    public static class Role
    {
        public const string Administrator = "Administrator";
        public const string User = "User";
        public const string Guest = "Guest";
        public const string Employee = "Employee";
    }
}
