using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class MonthlyActivityConfiguration : EntityTypeConfigurationBase<MonthlyActivity>
{
    protected override void ExtraConfigure(EntityTypeBuilder<MonthlyActivity> builder)
    {
        builder.ToTable("monthly-activity", "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasMaxLength(512)
            .IsUnicode(false)
            .IsRequired();

        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("fiscal_year")
                    .HasColumnType("SMALLINT")
                    .IsRequired();
            });

        builder.Property(x => x.Currency)
            .Currency();

        builder.ComplexProperty(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("year_end_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("report_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.SaleBeforeCurrentMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("sale_before_current_month")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.ComplexProperty(
            x => x.SaleCurrentMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("sale_current_month")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.ComplexProperty(
            x => x.SaleIncludeCurrentMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("sale_include_current_month")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.ComplexProperty(
            x => x.SaleLastYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("sale_last_year")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.Property(x => x.HasSubCompanySale)
            .IsRequired();
    }
}