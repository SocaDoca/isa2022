namespace MedicApp.Models
{
    public class AppointmentReport
    {
        public Guid Id { get; set; }    
        public string Description { get; set; }
        public string Equipment { get;set; }
        public bool IsDeleted { get; set; }

        public AppointmentReport() 
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
