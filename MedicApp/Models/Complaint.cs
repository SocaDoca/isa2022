namespace MedicApp.Models
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }


        public Complaint()
        {
            Id = Guid.NewGuid();
            Status = false;

        }
    }
}
