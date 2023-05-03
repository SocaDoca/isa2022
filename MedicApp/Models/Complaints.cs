﻿namespace MedicApp.Models
{
    public class Complaints
    {
        public Guid Id { get; set; }
        public bool IsForClinic { get; set; }
        public bool IsForEmployee { get; set; }
        public Guid? IsForClinic_Clinic_RefId { get; set; }
        public Guid? IsForEmployee_User_RefId { get; set; }
        public string UserInput { get; set; }
        public string Answer { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ComplaintBy_User_RefId { get; set; }
        public Complaints()
        {
            Id = Guid.NewGuid();
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

}
