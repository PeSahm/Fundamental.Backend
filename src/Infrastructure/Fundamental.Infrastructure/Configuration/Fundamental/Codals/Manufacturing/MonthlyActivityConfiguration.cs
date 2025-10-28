using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class MonthlyActivityConfiguration : EntityTypeConfigurationBase<MonthlyActivity>
{
    protected override void ExtraConfigure(EntityTypeBuilder<MonthlyActivity> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(MonthlyActivity)), "manufacturing");

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
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.SaleCurrentMonth,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.SaleIncludeCurrentMonth,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.SaleLastYear,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.Property(x => x.HasSubCompanySale)
            .IsRequired();

        builder.OwnsMany(
            x => x.ExtraSalesInfos,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("extra_sales_infos");
            });
    }
}