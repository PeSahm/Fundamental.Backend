using Fundamental.Domain.Codals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals;

public class RawCodalJsonConfiguration : EntityTypeConfigurationBase<RawCodalJson>
{
    protected override void ExtraConfigure(EntityTypeBuilder<RawCodalJson> builder)
    {
        builder.ToTable(
            NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(RawCodalJson)),
            "codals");

        // TraceNo is the unique identifier from CODAL - create unique index
        builder.Property(x => x.TraceNo)
            .HasColumnType("numeric(20,0)")
            .IsRequired();

        builder.HasIndex(x => x.TraceNo)
            .IsUnique();

        builder.Property(x => x.PublishDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.ReportingType)
            .IsRequired();

        builder.Property(x => x.StatementLetterType)
            .IsRequired();

        builder.Property(x => x.HtmlUrl)
            .HasMaxLength(1024)
            .IsRequired(false);

        builder.Property(x => x.PublisherId)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Isin)
            .HasMaxLength(12)
            .IsRequired(false);

        builder.Property(x => x.RawJson)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}
