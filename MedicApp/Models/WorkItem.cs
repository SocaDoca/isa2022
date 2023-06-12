namespace MedicApp.Models
{
    public class WorkItem
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int UsedInstances { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public WorkItem()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
        }

        public class WorkItemInfo
        {
            public String Name { get; set; }
            public int UsedInstances { get; set; }
        }
    }

}
