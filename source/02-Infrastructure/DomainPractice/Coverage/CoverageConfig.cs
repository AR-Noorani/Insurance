using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainPractice.Coverage
{
    internal sealed class CoverageConfig : IEntityTypeConfiguration<CoverageData>
    {
        public void Configure(EntityTypeBuilder<CoverageData> builder)
        {
            builder.ToTable("Coverages");

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Title)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();

            builder.Property(c => c.MinimumAllowedInvestment)
                .HasColumnType("decimal(18,0)");

            builder.Property(c => c.MaximumAllowedInvestment)
                .HasColumnType("decimal(18,0)");

            builder.Property(c => c.NetPercent)
                .HasColumnType("decimal(10,9)");
        }
    }
}
