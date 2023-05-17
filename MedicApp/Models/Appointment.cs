using MedicApp.Enums;

namespace MedicApp.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } //in minutes rounded
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Guid? Clinic_RefID { get; set; }
        public Guid? Patient_RefID { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsStarted { get; set; } 
        public bool IsPredefiend { get; set; }
        public bool IsFinished { get; set; }
        public bool IsReserved { get; set; }        

        public Appointment()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
            IsPredefiend = false;
            IsFinished = false;
            IsStarted = false;
            IsReserved = false;
            IsCanceled = false;
            Duration = 15;
            Title = "Blood appointment";
        }
    }
    public class AppointmentSaveModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string? StartTime { get; set; }
        public Guid? Patient_RefID { get; set; }
        public Guid? Clinic_RefID { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsReserved { get; set; }
        public bool IsFinished { get; set; }
        public AppointmentReport? Report { get; set; }
    }

    public class AppointmentLoadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public UserBasicInfo Patient { get; set; }
        public ClinicBasicInfo Clinic { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsPredefiend { get; set; }
        public bool IsFinished { get; set; }
        public AppointmentReport? Report { get; set; }
    }

    public class AppotinmentInClinics
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public User Patient { get; set; }
    }

    public class LoadPredefiendAppointment
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class SavePredefiendAppointment
    {
        public string Time { get; set; }
        public DateTime? Date { get; set; }
        public int NumberOfAppointmentsInDay { get; set; }
        public int Duration { get; set; }
        public Guid Clinic_RefID { get; set; }

    }
}
