namespace MedicApp.Models
{
    public class DbAppointment
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Start { get; set; }
        public int Duration{ get; set; }
        public bool IsReserved { get; set; }        
        
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public DbAppointment()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
