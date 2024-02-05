using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public sealed class SymbolShareHoldersConfiguration : EntityTypeConfigurationBase<SymbolShareHolder>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolShareHolder> builder)
    {
        builder.ToTable(
            "symbol-share-holders",
            "shd");

        builder.Property(x => x.ShareHolderName)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.SharePercentage)
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.ShareHolderSource)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.ReviewStatus)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol-id")
            .IsRequired();

        builder.HasOne(x => x.ShareHolderSymbol)
            .WithMany()
            .HasForeignKey("share-holder-symbol-id")
            .IsRequired(false);
    }
}