namespace MedicApp.Models
{
    public class SaveComplaintsRequest
    {
        public ComplaintSaveModel Complaint { get; set; }
        public Guid PatientId { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? EmployeeId { get; set; }
    }

    public class AnswerCompaintRequest
    {
        public Guid ComplaintId { get; set; }
        public string Answer { get; set; }
    }
}
