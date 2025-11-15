using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

/// <summary>
/// EF Core configuration for CanonicalInterpretativeReportSummaryPage5 entity.
/// Configures table mapping, owned collections as JSONB, and value object conversions.
/// </summary>
public class CanonicalInterpretativeReportSummaryPage5Configuration : EntityTypeConfigurationBase<CanonicalInterpretativeReportSummaryPage5>
{
    protected override void ExtraConfigure(EntityTypeBuilder<CanonicalInterpretativeReportSummaryPage5> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(CanonicalInterpretativeReportSummaryPage5)), "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasMaxLength(500)
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

        // JSONB owned collections
        builder.OwnsMany(
            x => x.OtherOperatingIncomes,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("other_operating_incomes");
            });

        builder.OwnsMany(
            x => x.OtherNonOperatingExpenses,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("other_non_operating_expenses");
            });

        builder.OwnsMany(
            x => x.FinancingDetails,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("financing_details");
            });

        builder.OwnsMany(
            x => x.FinancingDetailsEstimated,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("financing_details_estimated");
            });

        builder.OwnsMany(
            x => x.InvestmentIncomes,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("investment_incomes");
            });

        builder.OwnsMany(
            x => x.MiscellaneousExpenses,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("miscellaneous_expenses");
            });

        builder.OwnsMany(
            x => x.Descriptions,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("descriptions");
            });
    }
}
