namespace MedicApp.RelationshipTables
{
    public class Appointment2Patient
    {
        public Guid Id { get; set; }
        public Guid Appointment_RefID { get; set; }
        public Guid Patient_RefID { get; set; }
        public bool IsDeleted { get; set; }

        public Appointment2Patient()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
