using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class NonOperationIncomeAndExpenseConfiguration : EntityTypeConfigurationBase<NonOperationIncomeAndExpense>
{
    protected override void ExtraConfigure(EntityTypeBuilder<NonOperationIncomeAndExpense> builder)
    {
        builder.ToTable("non-operation-income-expense", "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasColumnName("Uri")
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

        builder.OwnsOne(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("year_end_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("report_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(512);

        builder.ComplexProperty(
            x => x.Value,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                navigationBuilder.Property(x => x.Currency)
                    .UseCurrencyColumn();
            });

        builder.Property(x => x.IsAudited)
            .IsRequired();

        builder.Property(x => x.CurrentPeriod)
            .IsRequired();

        builder.Property(x => x.PreviousPeriod)
            .IsRequired();

        builder.Property(x => x.ForecastPeriod)
            .IsRequired();

        builder.Property(x => x.YearlyForecastPeriod)
            .IsRequired()
            .HasDefaultValueSql("false");
    }
}