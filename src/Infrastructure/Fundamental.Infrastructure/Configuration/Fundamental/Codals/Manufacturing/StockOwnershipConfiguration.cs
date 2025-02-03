using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
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

        builder.Property(x => x.ReviewStatus).IsRequired()
            .HasColumnName("review_status")
            .HasDefaultValue(ReviewStatus.Pending)
            .HasSentinel(ReviewStatus.Pending);

        builder.Property(x => x.TraceNo)
            .HasColumnName("trace_no")
            .IsRequired(false);

        builder.Property(x => x.Url)
            .HasColumnName("url")
            .HasMaxLength(512)
            .IsUnicode()
            .IsRequired(false);
    }
}