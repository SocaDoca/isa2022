using MedicApp.Enums;

namespace MedicApp.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; } //in minutes
        public bool IsDeleted { get; set; }
        public AppointmentStatus Status { get; set; }

        public Appointment()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Duration = 20;
            Status = AppointmentStatus.Planned;
        }
    }



    public class AppointmentByPatientSaveModel
    {

        // We use this model only when Patient is logged on
        public Guid? Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public string? Title { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public Guid Patient_RefID{ get; set; } 
        public User? Doctor{ get; set; }
        
    }
    public class PredefinedAppointmentByAdmin
    {
        public Guid? Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public string? Title { get; set; }
        public int Duration { get;set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfWantedAppointments { get; set; }
        
    }

    public class AppointmentLoadModel
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public User? Patient { get; set; }
        public User? Doctor { get; set; }
    }
}
