using System.ComponentModel.DataAnnotations;

namespace PatientManagement
{
    public class PatientModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
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

        [Required]
        public bool AnyMedicationsTaking { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
