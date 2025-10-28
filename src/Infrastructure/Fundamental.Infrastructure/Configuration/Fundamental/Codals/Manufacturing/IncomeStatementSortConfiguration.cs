using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public sealed class IncomeStatementSortConfiguration : EntityTypeConfigurationBase<IncomeStatementSort>
{
    protected override void ExtraConfigure(EntityTypeBuilder<IncomeStatementSort> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(IncomeStatementSort)), "manufacturing");

        builder.Property(x => x.Order)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalRow)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(512)
            .IsRequired();

        builder.HasIndex(x => new { x.Order }).IsUnique();
    }
}