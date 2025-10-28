using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class BalanceSheetConfiguration : EntityTypeConfigurationBase<BalanceSheet>
{
    protected override void ExtraConfigure(EntityTypeBuilder<BalanceSheet> builder)
    {
        builder.ToTable("balance-sheet", "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol-id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasMaxLength(512)
            .IsUnicode()
            .IsRequired();

        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("fiscal-year")
                    .HasColumnType("SMALLINT")
                    .IsRequired();
            });

        builder.Property(x => x.Currency)
            .Currency();

        builder.OwnsOne(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("year-end-month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("report-month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.Property(x => x.IsAudited).IsRequired();

        builder.Property(x => x.PublishDate)
            .HasColumnName("publish-date")
            .IsRequired();

        builder.HasOne(x => x.FinancialStatement)
            .WithOne()
            .HasForeignKey<BalanceSheet>("financial-statement-id")
            .IsRequired(false);

        // Indexes for performance
        builder.HasIndex("symbol-id")
            .HasDatabaseName("ix_balance_sheet_symbol_id");

        builder.HasIndex(x => x.TraceNo)
            .HasDatabaseName("ix_balance_sheet_trace_no");

        builder.HasIndex(x => x.PublishDate)
            .HasDatabaseName("ix_balance_sheet_publish_date");
    }
}