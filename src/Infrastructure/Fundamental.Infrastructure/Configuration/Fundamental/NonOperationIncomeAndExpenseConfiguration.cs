using Fundamental.Domain.Statements.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class NonOperationIncomeAndExpenseConfiguration : EntityTypeConfigurationBase<NonOperationIncomeAndExpense>
{
    protected override void ExtraConfigure(EntityTypeBuilder<NonOperationIncomeAndExpense> builder)
    {
        builder.ToTable("NonOperationIncomeAndExpense", "fs");

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

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.ComplexProperty(
            x => x.Value,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("Value")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.Property(x => x.IsAudited)
            .HasColumnName("IsAudited")
            .HasColumnType("BIT")
            .IsRequired();

        builder.Property(x => x.CurrentPeriod)
            .HasColumnName("CurrentPeriod")
            .HasColumnType("BIT")
            .IsRequired();

        builder.Property(x => x.PreviousPeriod)
            .HasColumnName("PreviousPeriod")
            .HasColumnType("BIT")
            .IsRequired();

        builder.Property(x => x.ForecastPeriod)
            .HasColumnName("ForecastPeriod")
            .HasColumnType("BIT")
            .IsRequired();
    }
}