namespace MedicApp.RelationshipTables
{
    public class Employee2WokringHours
    {
        public Guid Id { get; set; }
        public Guid Employee_RefID { get; set; }
        public Guid WorkingHours_RefID { get; set; }
        public bool IsDeleted { get; set; }  
        public Employee2WokringHours()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }    
}
