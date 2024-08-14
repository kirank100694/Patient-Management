using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PatientManagement.Data
{
    public class PatientContext :DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }

        public DbSet<PatientModel> Patients { get; set; }
    }
}
