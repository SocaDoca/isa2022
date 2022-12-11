namespace MedicApp.RelationshipTables
{
    public class Account2Clinic
    {
        public Guid Id { get; set; }
        public Guid Account_RefID { get; set; }
        public Guid Clinic_RefID { get; set; }
        public bool IsDeleted { get; set; }
        public Account2Clinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }


}
