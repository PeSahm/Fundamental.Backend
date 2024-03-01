using Fundamental.Domain.Codals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals;

public class FinancialStatementConfiguration : EntityTypeConfigurationBase<FinancialStatement>
{
    protected override void ExtraConfigure(EntityTypeBuilder<FinancialStatement> builder)
    {
        builder.ToTable("financial-statement", "fs");

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

        builder.ComplexProperty(
            x => x.OperatingIncome,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.GrossProfit,
            navigationBuilder => navigationBuilder.UseSignedCodalMoney());

        builder.ComplexProperty(
            x => x.OperatingProfit,
            navigationBuilder => navigationBuilder.UseSignedCodalMoney());

        builder.ComplexProperty(
            x => x.BankInterestIncome,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.InvestmentIncome,
            navigationBuilder => navigationBuilder.UseSignedCodalMoney());

        builder.ComplexProperty(
            x => x.NetProfit,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.Expense,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.Asset,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.OwnersEquity,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.Receivables,
            navigationBuilder => navigationBuilder.UseCodalMoney());
    }
}