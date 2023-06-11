namespace MedicApp.Models
{
    public class Complaints
    {
        public Guid Id { get; set; }
        public bool IsForClinic { get; set; }
        public bool IsForEmployee { get; set; }
        public Guid? IsForClinic_Clinic_RefId { get; set; }
        public Guid? IsForEmployee_User_RefId { get; set; }
        public string? UserInput { get; set; } 
        public string? Answer { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ComplaintBy_User_RefId { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Complaints()
        {
            Id = Guid.NewGuid();
            Creation_TimeStamp = DateTime.Now;
            IsDeleted = false;
        }
    }

    public class ComplaintSaveModel
    {
        public Guid Id { get; set; }
        public bool IsForClinic { get; set; }
        public bool IsForEmployee { get; set; }
        public string UserInput { get; set; }
        public string Answer { get; set; }
        public bool IsAnswered { get; set; }
    }

    public class ComplaintListModel
    {
        public Guid Id { get; set; }
        public bool IsForClinic { get; set; }
        public bool IsForEmployee { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsAnswered { get; set; }
    }

}
