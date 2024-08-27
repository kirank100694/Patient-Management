using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Models;

namespace PatientManagement.Data
{
    public class PatientContext : IdentityDbContext<IdentityUser>
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }

        public DbSet<PatientModel> Patients { get; set; }
    }
}
