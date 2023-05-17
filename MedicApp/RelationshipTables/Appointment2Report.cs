namespace MedicApp.RelationshipTables
{
    public class Appointment2Report
    {
        public Guid Id { get; set; }
        public Guid Appointment_RefID { get; set; }
        public Guid ReportId { get; set; }  
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }
        public Appointment2Report()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
        }
    }
}
