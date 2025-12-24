using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainPractice.Investment
{
    internal sealed class InvestmentConfig : IEntityTypeConfiguration<InvestmentData>
    {
        public void Configure(EntityTypeBuilder<InvestmentData> builder)
        {
            builder.ToTable("Investments");

            builder.Property(i => i.Id)
                .ValueGeneratedNever();

            builder.Property(i => i.Title)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();

            builder.Property(i => i.Value)
                .HasColumnType("decimal(18,0)");

            builder
                .HasMany(i => i.Coverages)
                .WithMany(c => c.Investments)
                .UsingEntity(cf=>cf.ToTable("InvestmentCoverages"));
        }
    }
}
