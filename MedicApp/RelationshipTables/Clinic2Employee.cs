﻿namespace MedicApp.RelationshipTables
{
    public class Clinic2Employee
    {
        public Guid Id { get; set; }
        public Guid Clinic_RefID { get; set; }
        public Guid Employee_RefID { get; set; }
        public bool IsDeleted { get; set; }


        public Clinic2Employee()
        {
            Id = Guid.NewGuid();
        }
    }
}
