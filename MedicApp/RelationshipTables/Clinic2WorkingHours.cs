namespace MedicApp.RelationshipTables
{
    public class Clinic2WorkingHours
    {
        public Guid Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public Guid WorkingHours_RefID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
