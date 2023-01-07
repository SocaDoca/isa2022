namespace MedicApp.RelationshipTables
{
    public class Appointment2Doctor
    {
        public Guid Id { get; set; }
        public Guid Appointment_RefID { get; set; }
        public Guid Doctor_RefID { get; set; }
        public bool IsDeleted { get; set; } 

        public Appointment2Doctor()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
