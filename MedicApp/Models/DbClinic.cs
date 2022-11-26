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
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<DbWorkingHours> WorkHours { get; set; }
        public bool IsDeleted { get; set; }


        public DbClinic()
        {
            Id = Guid.NewGuid();
            
        }

        public class ClinicSaveModel
        {
            public Guid Id { get; set;}
            public string? Name { get; set; }   
            public int Capacity { get; set; }
            public AddressBasicInfo Address { get; set; }
            public string Description { get; set; }
            public double Rating { get;set; }
            public string Email { get; set; }
            public EmployeeBasicModel AdminCenter { get; set; }
            public List<EmployeeBasicModel> Employees { get; set; }
            public List<DbWorkingHours> WorkingHours { get; set; }
        }
    }
}
