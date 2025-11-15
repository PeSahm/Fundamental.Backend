using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class BalanceSheetSortConfiguration : EntityTypeConfigurationBase<BalanceSheetSort>
{
    protected override void ExtraConfigure(EntityTypeBuilder<BalanceSheetSort> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(BalanceSheetSort)), "manufacturing");

        builder.Property(x => x.Order)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalRow)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.HasIndex(x => new { x.Order }).IsUnique();

        builder.HasIndex(x => new { x.Category, x.CodalRow }).IsUnique();
    }
}