using Microsoft.EntityFrameworkCore;
using PatientManagement.Models;
using System.Collections.Generic;

namespace PatientManagement.Data
{
    public class PatientContext :DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
