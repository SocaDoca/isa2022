namespace MedicApp.Models
{
    public class CommunicationChannel
    {
        public Guid Id { get; set; }
        public CommunicationMessageType Type { get; set; }
        public string Value { get; set; }  
    }

    public enum CommunicationMessageType
    {
        Mobile = 1,
        Email = 2,
        Fax = 3
    }
}