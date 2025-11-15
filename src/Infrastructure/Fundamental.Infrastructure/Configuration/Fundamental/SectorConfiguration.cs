using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SectorConfiguration : EntityTypeConfigurationBase<Sector>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Sector> builder)
    {
        builder.ToTable("sector", "shd");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name);

        builder.HasMany(x => x.Symbols)
            .WithOne(x => x.Sector)
            .OnDelete(DeleteBehavior.NoAction);
    }
}