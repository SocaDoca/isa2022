using MedicApp.Enums;

namespace MedicApp.Models
{
    public class DbEmployee
    {
        public Guid Id { get; set; }
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? JMBG { get; set; }
        public bool IsAdminCenter { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public Gender Gender { get; set; }
        public Guid WorkingHours_RefId { get; set; }
        public double? WorkDuration { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; } 

        public DbEmployee()
        {
            Id = Guid.NewGuid();
            JMBG = String.Empty;
            IsAdminCenter = false;
            
        }
         
    }

    public class EmployeeBasicModel
    {
        public Guid Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsAdmin { get; set; }
    }
}
