using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Column(TypeName = "nVarchar(30)")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "nVarchar(10)")]
        public string Gender { get; set; }

        [Column(TypeName = "nVarchar(13)")]
        public string ContactNumber { get; set; }
        public double Weight { get; set; }

        public double Height { get; set; }

        [Column(TypeName = "nVarchar(30)")]
        public string Email { get; set; }

        [Column(TypeName = "nVarchar(200)")]
        public string Address { get; set; }

        [Column(TypeName = "nVarchar(500)")]
        public string MedicalComments { get; set; }

        public bool AnyMedicationsTaking { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
