using Medication_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medication_Management_System.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Medication> medication { get; set; }
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Report> report { get; set; }
    }
}
