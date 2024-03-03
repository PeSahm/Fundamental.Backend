using Fundamental.Domain.ExAreas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.ExAreas;

public sealed class FairConfiguration : EntityTypeConfigurationBase<Fair>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Fair> builder)
    {
        builder.ToTable("fair", "ex_areas");

        builder.Property(x => x.Json)
            .HasColumnType("jsonb");
    }
}