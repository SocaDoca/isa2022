namespace MedicApp.RelationshipTables
{
    public class Appointment2Clinic
    {
        public Guid Id { get;set; }
        public Guid Appointment_RefID { get;set; }
        public Guid Clinic_RefID { get;set; }
        public bool IsDeleted { get;set; }  


        public Appointment2Clinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
