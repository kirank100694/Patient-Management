using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Data
{
    public class PatientCreateDate
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Phone]
        public string ContactNumber { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [Range(0, double.MaxValue)]
        public double Height { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(500)]
        public string MedicalComments { get; set; }

        public bool AnyMedicationsTaking { get; set; }
    }
}
