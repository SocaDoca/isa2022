namespace MedicApp.RelationshipTables
{
    public class WorkItem2Reports
    {
        public Guid Id { get; set; }
        public Guid WorkItem_RefID { get; set; }
        public Guid Report_RefID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }

        public WorkItem2Reports() {
            Id = Guid.NewGuid();
            Creation_TimeStamp = DateTime.Now;
            IsDeleted = false;

        }
    }
}