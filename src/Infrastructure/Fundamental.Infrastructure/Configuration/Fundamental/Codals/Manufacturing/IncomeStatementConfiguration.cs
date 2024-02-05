using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class IncomeStatementConfiguration : EntityTypeConfigurationBase<IncomeStatement>
{
    protected override void ExtraConfigure(EntityTypeBuilder<IncomeStatement> builder)
    {
        builder.ToTable("income-statement", "manufacturing");
        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol-id")
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

        builder.ComplexProperty(
            x => x.Value,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.Property(x => x.Row).IsRequired();

        builder.Property(x => x.CodalRow).IsRequired();

        builder.Property(x => x.CodalCategory).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

        builder.Property(x => x.IsAudited).IsRequired();
    }
}