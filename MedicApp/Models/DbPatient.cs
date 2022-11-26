using MedicApp.Enums;

namespace MedicApp.Models
{
    public class DbPatient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string JMBG { get; set; }
        public LoyaltyLevel LoyaltyLevel { get; set; }
        public bool IsDeleted { get; set; }

        public DbPatient()
        {
            Id = Guid.NewGuid();
        }
        
        //public Guid PersonInfo_RefID { get; set; }
        //public AppointmentHistory AppointmentHistory { get; set; }
    }
}
