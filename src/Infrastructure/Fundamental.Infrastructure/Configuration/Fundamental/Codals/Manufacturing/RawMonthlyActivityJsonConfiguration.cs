using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class RawMonthlyActivityJsonConfiguration : EntityTypeConfigurationBase<RawMonthlyActivityJson>
{
    protected override void ExtraConfigure(EntityTypeBuilder<RawMonthlyActivityJson> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(RawMonthlyActivityJson)), "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.PublishDate)
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(x => x.Version)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.RawJson)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}