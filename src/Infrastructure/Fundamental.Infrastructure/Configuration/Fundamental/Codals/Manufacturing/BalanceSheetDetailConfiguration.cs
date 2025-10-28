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

        // Indexes for performance
        builder.HasIndex("balance-sheet-id")
            .HasDatabaseName("ix_balance_sheet_detail_balance_sheet_id");

        builder.HasIndex(x => x.CodalRow)
            .HasDatabaseName("ix_balance_sheet_detail_codal_row");

        builder.HasIndex(x => x.CodalCategory)
            .HasDatabaseName("ix_balance_sheet_detail_codal_category");

        // Composite index for ordered queries within a balance sheet
        builder.HasIndex("balance-sheet-id", nameof(BalanceSheetDetail.Row))
            .HasDatabaseName("ix_balance_sheet_detail_balance_sheet_id_row");

        // Composite index for filtering by category and row
        builder.HasIndex(x => new { x.CodalCategory, x.CodalRow })
            .HasDatabaseName("ix_balance_sheet_detail_category_row");
    }
}