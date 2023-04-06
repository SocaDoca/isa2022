namespace MedicApp.RelationshipTables
{
    public class Complaint2Patient
    {
        public Guid Id { get; set; }
        public Guid Patient_RefId { get; set; }
        public Guid Complaint_RefId { get; set; }
        public bool IsDeleted { get; set; }

        public Complaint2Patient()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
