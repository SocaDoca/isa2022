﻿namespace MedicApp.Models.RequestModels
{
    public class ClinicRatingRequest
    {
        public Guid ClinicId { get; set; }
        public Guid PatientId { get; set; }
        public string Rating { get; set; }
    }
}
