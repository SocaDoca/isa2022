namespace MedicApp.RelationshipTables
{
    public class Clinic2Laboratory
    {
        public Guid Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public Guid Laboratory_RefID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
