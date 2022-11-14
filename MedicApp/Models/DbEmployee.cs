namespace MedicApp.Models
{
    public class DbEmployee
    {
        public Guid Id { get; set; }
        public bool IsAdminCenter { get; set; }
        public Guid PersonInfo_RefID { get; set; }
    }
}
