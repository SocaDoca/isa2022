namespace MedicApp.Models
{
    public class WorkItem
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int UsedInstances { get; set; }
        public bool IsDeleted { get; set; }

        public WorkItem()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }

        public class WorkItemInfo
        {
            public String Name { get; set; }
            public int UsedInstances { get; set; }
        }
    }

}
