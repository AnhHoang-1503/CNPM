using CNPM.Domain;
using CNPM.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<TemporaryPaper> Temporary_Paper { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<FamilyFeeMapping> Family_Fee_Mapping { get; set; }
    }
}
