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

        builder.OwnsOne(
            x => x.OperatingIncome,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("operating_income")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.GrossProfit,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("gross_profit")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.OperatingProfit,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("operating_profit")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.BankInterestIncome,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("bank_interest_income")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.InvestmentIncome,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("investment_income")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.NetProfit,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("net_profit")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.Expense,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("expense")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.Asset,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("asset")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.OwnersEquity,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("owners_equity")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });

        builder.OwnsOne(
            x => x.Receivables,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("receivables")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });
    }
}