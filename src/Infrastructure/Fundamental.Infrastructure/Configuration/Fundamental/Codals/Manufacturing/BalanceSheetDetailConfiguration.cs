using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class BalanceSheetDetailConfiguration : EntityTypeConfigurationBase<BalanceSheetDetail>
{
    protected override void ExtraConfigure(EntityTypeBuilder<BalanceSheetDetail> builder)
    {
        builder.ToTable("balance-sheet-detail", "manufacturing");

        builder.HasOne(x => x.BalanceSheet)
            .WithMany(x => x.Details)
            .HasForeignKey("balance-sheet-id")
            .IsRequired();

        builder.Property(x => x.Row)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalRow)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalCategory)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.ComplexProperty(
            x => x.Value,
            amount =>
                amount.UseSignedCodalMoney()
        );
    }
}