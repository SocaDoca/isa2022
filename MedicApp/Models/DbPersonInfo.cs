namespace MedicApp.Models
{
    public class DbPersonInfo
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string JMBG { get; set; }
        public string Address { get; set; }




        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

            

    }
}
