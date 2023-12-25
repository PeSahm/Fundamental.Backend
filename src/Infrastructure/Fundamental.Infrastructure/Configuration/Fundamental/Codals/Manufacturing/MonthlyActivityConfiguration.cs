using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class MonthlyActivityConfiguration : EntityTypeConfigurationBase<MonthlyActivity>
{
    protected override void ExtraConfigure(EntityTypeBuilder<MonthlyActivity> builder)
    {
        builder.ToTable("MonthlyActivities", "fs");

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

        builder.ComplexProperty(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("YearEndMonth")
                    .HasColumnType("TINYINT")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("ReportMonth")
                    .HasColumnType("TINYINT")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.SaleBeforeCurrentMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("SaleBeforeCurrentMonth")
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
                    .HasColumnName("SaleCurrentMonth")
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
                    .HasColumnName("SaleIncludeCurrentMonth")
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
                    .HasColumnName("SaleLastYear")
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