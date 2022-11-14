namespace MedicApp.Models
{
    public class DbClinic
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public int Capacity { get; set; }
        public DateTime? WorkingFrom { get; set; }
        public DateTime? WorkingTo { get; set; }
        public bool IsDeleted { get; set; }


        public DbClinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }

        public class SaveClinicModel
        {
            public Guid Id { get; set;}
            public string? Name { get; set; }
            public string? Address { get; set; }
            public int Capacity { get; set; }
            public string? Description { get; set; }
            public double? Rating { get; set; }
            public List<DbAppointment> Appointments { get; set; }
            public List<DbEmployee>Employees { get; set; }
        }
    }
}
