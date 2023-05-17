namespace MedicApp.RelationshipTables
{
    public class Employee2WokringHours
    {
        public Guid Id { get; set; }
        public Guid Employee_RefID { get; set; }
        public Guid WorkingHours_RefID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Employee2WokringHours()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
        }
    }    
}
