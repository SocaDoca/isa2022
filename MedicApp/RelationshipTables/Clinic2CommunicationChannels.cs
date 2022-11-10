namespace MedicApp.RelationshipTables
{
    public class Clinic2CommunicationChannels
    {
        public Guid Id { get; set; }    
        public Guid Clinic_RefID { get; set; }
        public Guid CommunicationChannel_RefID { get; set; }
        public bool IsDeleted { get; set; }


        public Clinic2CommunicationChannels()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
