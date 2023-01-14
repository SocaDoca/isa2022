namespace MedicApp.RelationshipTables
{
    public class Patient2Questionnaire
    {
        public Guid Id { get; set; }
        public Guid Patient_RefId { get; set; }
        public Guid Questionnaire_RefId { get; set; }
        public bool IsDeleted { get; set; } 


        public Patient2Questionnaire()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
