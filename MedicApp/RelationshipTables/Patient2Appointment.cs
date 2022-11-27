namespace MedicApp.RelationshipTables
{
    public class Patient2Appointment
    {
        public Guid Id { get; set; }
        public Guid Patient_RefID { get; set; }
        public Guid Appointment_RefID { get; set; }
        public bool IsDeleted { get; set; }
    }

}
