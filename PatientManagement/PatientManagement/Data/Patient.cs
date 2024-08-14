﻿namespace PatientManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MedicalComments { get; set; }
        public bool AnyMedicationsTaking { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
