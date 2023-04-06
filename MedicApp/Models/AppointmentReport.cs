using static MedicApp.Models.WorkItem;

namespace MedicApp.Models
{
    public class AppointmentReport
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<WorkItem> Equipment { get; set; }
        public bool IsDeleted { get; set; }

        public AppointmentReport()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Equipment = new List<WorkItem>();
        }
    }


    public class ReportSaveModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<WorkItemInfo> Equipment { get; set; }
    }

}
