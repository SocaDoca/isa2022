namespace MedicApp.Models
{
    public class Questionnaire
    {
        public Guid Id { get; set; }
        public Guid Patient_RefID { get; set; }
        public DateTime ExpireDate { get; set; }
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
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }

        public Questionnaire()
        {
            Id = Guid.NewGuid();
            Creation_TimeStamp = DateTime.Now;
            IsDeleted = false;
            IsValid = false;
        }

        public bool IsQuestionireSigned()
        {

            if (this.question1)
            {
                return false;
            }
            if (this.question2)
            {
                return false;
            }
            if (this.question3)
            {
                return false;
            }
            if (this.question4)
            {
                return false;
            }
            if (this.question5)
            {
                return false;
            }
            if (this.question6)
            {
                return false;
            }
            if (this.question7)
            {
                return false;
            }
            if (this.question8)
            {
                return false;
            }
            if (this.question9)
            {
                return false;
            }
            if (this.question10)
            {
                return false;
            }
            if (this.question11)
            {
                return false;
            }
            if (this.question12)
            {
                return false;
            }
            return true;

        }
    }

    public class SaveQuestionnaire
    {
        public Guid Id { get; set; }
        public Guid Patient_RefID { get; set; }
        public DateTime ExpireDate { get; set; }
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
        public bool IsValid { get; set; }
    }
}
