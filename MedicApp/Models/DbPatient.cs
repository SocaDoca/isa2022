using MedicApp.Enums;

namespace MedicApp.Models
{
    public class DbPatient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? JMBG { get; set; }
        public Gender? Gender { get; set; }
        //public LoyaltyLevel LoyaltyLevel { get; set; }
        public bool IsDeleted { get; set; }
        public string? Job { get; set; }

        public DbPatient()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
        
        
        
    }


}
