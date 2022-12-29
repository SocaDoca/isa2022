namespace MedicApp.Models
{
    

    public class Questionnaire
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public bool IsDeleted { get; set; }



        public Questionnaire()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }        
        
    }
}
