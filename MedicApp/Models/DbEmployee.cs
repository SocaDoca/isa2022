namespace MedicApp.Models
{
    public class DbEmployee
    {
        public Guid Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string JMBG { get; set; }
        public bool IsAdminCenter { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime StartWorkingHours { get; set; }
        public double WorkDuration { get; set; }
         
    }
}
