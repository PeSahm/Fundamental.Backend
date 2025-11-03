using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class CanonicalMonthlyActivityConfiguration : EntityTypeConfigurationBase<CanonicalMonthlyActivity>
{
    protected override void ExtraConfigure(EntityTypeBuilder<CanonicalMonthlyActivity> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(CanonicalMonthlyActivity)), "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.Version)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.PublishDate)
            .HasColumnName("publish_date")
            .HasColumnType("timestamp without time zone");

        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("fiscal_year")
                    .HasColumnType("SMALLINT")
                    .IsRequired();
            });

        builder.OwnsMany(
            x => x.BuyRawMaterialItems,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("buy_raw_material_items");
            });

        builder.OwnsMany(
            x => x.ProductionAndSalesItems,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("production_and_sales_items");
            });

        builder.OwnsMany(
            x => x.EnergyItems,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("energy_items");
            });

        builder.OwnsMany(
            x => x.CurrencyExchangeItems,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("currency_exchange_items");
            });

        builder.OwnsMany(
            x => x.Descriptions,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("descriptions");
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
    }
}