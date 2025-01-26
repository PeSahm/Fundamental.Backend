using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class StockOwnershipConfiguration : EntityTypeConfigurationBase<StockOwnership>
{
    protected override void ExtraConfigure(EntityTypeBuilder<StockOwnership> builder)
    {
        builder.ToTable("stock_ownership", "manufacturing");

        builder.HasOne(x => x.ParentSymbol)
            .WithMany()
            .HasForeignKey("parent_symbol_id")
            .IsRequired();

        builder.Property(x => x.SubsidiarySymbolName)
            .HasColumnName("subsidiary_symbol_name")
            .HasMaxLength(512)
            .IsUnicode()
            .IsRequired();

        builder.Property(x => x.OwnershipPercentage)
            .HasColumnName("ownership_percentage")
            .HasPrecision(5, 2)
            .HasColumnType("decimal")
            .IsRequired();

        builder.ComplexProperty(
            x => x.CostPrice,
            navigationBuilder => navigationBuilder.UseSignedCodalMoney());

        builder.HasOne(x => x.SubsidiarySymbol)
            .WithMany()
            .HasForeignKey("subsidiary_symbol_id")
            .IsRequired(false);
    }
}