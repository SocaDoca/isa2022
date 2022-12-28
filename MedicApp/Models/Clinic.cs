namespace MedicApp.Models
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public float Rating { get; set; }
        public bool IsDeleted { get; set; }

        public Clinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }

    public class ClinicSaveModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public float Rating { get; set; }
       
    }

    public class ClinicList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public float Rating { get; set; }
    }

}
