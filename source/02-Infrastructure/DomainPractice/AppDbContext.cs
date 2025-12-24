using DomainPractice.Coverage;
using DomainPractice.Investment;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DomainPractice
{
    internal sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<InvestmentData> Investments { get; set; }
        public DbSet<CoverageData> Coverages { get; set; }
    }
}
