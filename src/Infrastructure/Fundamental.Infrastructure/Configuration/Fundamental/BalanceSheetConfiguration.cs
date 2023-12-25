using Fundamental.Domain.Statements.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class BalanceSheetConfiguration : EntityTypeConfigurationBase<BalanceSheet>
{
    protected override void ExtraConfigure(EntityTypeBuilder<BalanceSheet> builder)
    {
        builder.ToTable("BalanceSheet", "fs");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("SymbolId")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnName("TraceNo")
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasColumnName("Uri")
            .HasColumnType("VARCHAR(512)")
            .IsRequired();

        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("FiscalYear")
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
                    .HasColumnName("YearEndMonth")
                    .HasColumnType("TINYINT")
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("ReportMonth")
                    .HasColumnType("TINYINT")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.Value,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("Value")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.Property(x => x.Row)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalRow)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalCategory)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

        builder.Property(x => x.IsAudited).IsRequired();
    }
}