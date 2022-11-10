namespace MedicApp.Models
{
    public class Laboratory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        public LaboratoryType Type { get; set; }
        public int Capacity { get; set; }

        public 
    }

    public enum LaboratoryType
    {
        Blood = 1,
        Test = 2
    }
}
