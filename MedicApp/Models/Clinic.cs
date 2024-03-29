﻿namespace MedicApp.Models
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WorksFrom { get; set; }
        public DateTime WorksTo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Creation_TimeStamp { get; set; }

        public Clinic()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Creation_TimeStamp = DateTime.Now;
            Rating = 0;
        }
    }

    public class ClinicSaveModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime WorksFrom { get; set; }
        public DateTime WorksTo { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public double Rating { get; set; }
       
    }
    public class ClinicLoadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WorksFrom { get; set; }
        public DateTime WorksTo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public double Rating { get; set; }
        public List<Appointment> Appointments { get; set; }            

       
    }

    public class ClinicBasicInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WorksFrom { get; set; }
        public DateTime WorksTo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }

    public class ClinicLoadParameters
    {
        public string? SearchCriteria { get; set; }
        public ClinicFilterData? ClinicFilterData { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public bool OrderAsc { get; set; }
    }

    public class ClinicFilterData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class ClinicList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime WorksFrom { get; set; }
        public DateTime WorksTo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public double Rating { get; set; }
        public List<AppotinmentInClinics> Appointments { get; set; } = new List<AppotinmentInClinics>();
    }


    public class ClinicDropdownModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
