namespace MedicApp.Models
{
    public class DbSessions
    {
        public Guid Id { get; set; }
        public string SessionToken { get; set; }
        public Guid Account_RefID { get;set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsDeleted { get; set; }

    }
}
