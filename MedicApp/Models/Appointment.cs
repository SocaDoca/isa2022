﻿using MedicApp.Enums;

namespace MedicApp.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; } //in minutes rounded
        public bool IsDeleted { get; set; }
        public Guid? Clinic_RefID { get; set; }
        public Guid? Patient_RefID { get; set; }
        public Guid? ResponsiblePerson_RefID { get; set; } //employee or nurse who is taking blood from donor
        public bool IsCanceled { get; set; }
        public bool IsFinished { get; set; }

        public Appointment()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Duration = 15;
            Title = "Blood appointment";            
        }

        
    }
    public class AppointmentSaveModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public UserLoadModel ResponsiblePerson { get; set; }
        public UserLoadModel Patient { get; set; }
        public Clinic Clinic { get; set; } // maybe only send gid?
        public bool IsCanceled { get; set; }
        public bool IsFinished { get; set; }
    }

    public class AppointmentLoadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public UserLoadModel ResponsiblePerson { get; set; }
        public UserLoadModel Patient { get; set; }
        public ClinicLoadModel Clinic { get; set; }

    }
}
