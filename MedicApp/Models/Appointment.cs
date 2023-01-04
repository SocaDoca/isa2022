namespace MedicApp.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? Patient_RefID { get; set; }
        public Guid? Doctor_RefID { get; set; }

        public Appointment()
        {
            Id = Guid.NewGuid();
        }
    }

    public class AppointmentStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GID { get; set; }
    }


    public class AppointmentSaveModel
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? Patient_RefID { get; set; }
        public Guid? Doctor_RefID { get; set; }
        
    }
}
