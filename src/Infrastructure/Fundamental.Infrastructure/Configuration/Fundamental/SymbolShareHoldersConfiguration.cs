using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public sealed class SymbolShareHoldersConfiguration : EntityTypeConfigurationBase<SymbolShareHolder>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolShareHolder> builder)
    {
        builder.ToTable(
            NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(SymbolShareHolder)),
            "shd");

        builder.Property(x => x.ShareHolderName)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.SharePercentage)
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.ReviewStatus)
            .IsRequired();

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.HasOne(x => x.ShareHolderSymbol)
            .WithMany()
            .HasForeignKey("share_holder_symbol_id")
            .IsRequired(false);
    }
}