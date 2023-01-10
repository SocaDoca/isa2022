namespace MedicApp.Models
{
    public class Questionnaire
    {
        public Guid Id { get; set; }
        public bool question1 { get; set; }
        public bool question2 { get; set; }
        public bool question3 { get; set; }
        public bool question4 { get; set; }
        public bool question5 { get; set; }
        public bool question6 { get; set; }
        public bool question7 { get; set; }
        public bool question8 { get; set; }
        public bool question9 { get; set; }
        public bool question10 { get; set; }
        public bool question11 { get; set; }
        public bool question12 { get; set; }

        public bool IsDeleted { get; set; }

        public Questionnaire()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }        
        
    } 
}
