using MedicApp.Enums;

namespace MedicApp.Models
{
    public class AppointmentHistory
    {
        public Guid Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public bool IsStartedAppointment { get; set; }    
        public bool IsFinishedAppointment { get; set; }    
        public Guid ChangedByUser_RefID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeCreated { get; set; }

        public AppointmentHistory()
        {
            Id = Guid.NewGuid();
            TimeCreated = DateTime.Now;
            IsDeleted = false;
        }
    }
}
