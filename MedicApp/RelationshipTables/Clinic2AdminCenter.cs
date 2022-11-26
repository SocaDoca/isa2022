namespace MedicApp.RelationshipTables
{
    public class Clinic2AdminCenter
    {
        public Guid Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public Guid AdminCenter_RefID { get; set; }
        public bool IsDeleted { get; set; }
        
        public Clinic2AdminCenter()
        {
            Id = Guid.NewGuid();
        }
    }
}
