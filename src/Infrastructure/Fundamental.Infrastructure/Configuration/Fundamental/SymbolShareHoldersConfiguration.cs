using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public sealed class SymbolShareHoldersConfiguration : EntityTypeConfigurationBase<SymbolShareHolder>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolShareHolder> builder)
    {
        builder.ToTable("SymbolShareHolder",
            "shd",
            t => t.IsTemporal(cfg =>
            {
                cfg.UseHistoryTable("SymbolShareHolderHistory", "shd");
            }));

        builder.Property(x => x.ShareHolderName)
            .HasColumnType("nvarchar(512)")
            .IsRequired();

        builder.Property(x => x.SharePercentage)
            .HasPrecision(18, 5)
            .HasColumnName("SharePercentage")
            .IsRequired();

        builder.Property(x => x.ShareHolderSource)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.ReviewStatus)
            .HasColumnType("SMALLINT")
            .HasColumnName("ReviewStatus")
            .IsRequired();

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("SymbolId")
            .IsRequired();

        builder.HasOne(x => x.ShareHolderSymbol)
            .WithMany()
            .HasForeignKey("ShareHolderSymbolId")
            .IsRequired(false);
    }
}