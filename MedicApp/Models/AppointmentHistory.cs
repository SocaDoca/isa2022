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
        public DateTime Creation_TimeStamp { get; set; }

        public AppointmentHistory()
        {
            Id = Guid.NewGuid();
            Creation_TimeStamp = DateTime.Now;
            IsDeleted = false;
        }
    }

    public class AppointmentListHistory
    {
        public Guid Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid ClinicId { get; set; }
        public DateTime TimeFinished { get; set; }
        public string ClinicName { get; set; }
        public string ReportDescription { get; set; }
    }
}
