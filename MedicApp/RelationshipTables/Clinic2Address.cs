namespace MedicApp.RelationshipTables
{
    public class Clinic2Address
    {
        public Guid Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public Guid Address_RefID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
