namespace MedicApp.Models
{
    public class Questionnaire
    {
        public Guid Id { get; set; }

        //Patient 
        public string PatienetName { get; set; }
        public string JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get;set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; } 
        public string Job { get; set; }
        public int NumberOfGivenBlood { get; set; }
        public string PatientSignature { get;set;}


        //Employee
        //Registration 
        public BloodType BloodType { get; set; }
        public string DoctorAttention { get; set; }
        public string ResponsiblePerson { get; set; }

        public bool IsDeleted { get; set; }



        public Questionnaire()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }        
        
    }


    public enum BloodType
    {
        A = 1,
        B = 2,
        AB = 3,
        O = 4,
    } 
}
