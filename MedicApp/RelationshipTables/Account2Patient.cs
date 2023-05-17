namespace MedicApp.RelationshipTables
{
    public class Account2Patient
    {
        public Guid Id { get; set; }
        public Guid Account_RefID { get; set; }
        public Guid Patient_RefID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Account2Patient()
        {
            Id = Guid.NewGuid();
            IsDeleted= false;
            Creation_TimeStamp = DateTime.Now;
        }
    }
}
