namespace MedicApp.RelationshipTables
{
    public class WorkItem2Reports
    {
        public Guid Id { get; set; }
        public Guid WorkItem_RefID { get; set; }
        public Guid Report_RefID { get; set; }
        public bool IsDeleted { get; set; }
    }
}