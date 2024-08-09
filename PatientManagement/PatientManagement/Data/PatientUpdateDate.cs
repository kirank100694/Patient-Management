using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Data
{
    public class PatientUpdateDate
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Phone]
        public string ContactNumber { get; set; }

        [Range(0, double.MaxValue)]
        public double? Weight { get; set; }

        [Range(0, double.MaxValue)]
        public double? Height { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(500)]
        public string MedicalComments { get; set; }

        public bool? AnyMedicationsTaking { get; set; }

    }
}
