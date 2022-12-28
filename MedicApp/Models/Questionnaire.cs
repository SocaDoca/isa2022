namespace MedicApp.Models
{
    

    public class Questionnaire
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }



        public Questionnaire()
        {
            Id = Guid.NewGuid();
        }        
        
    }
}
