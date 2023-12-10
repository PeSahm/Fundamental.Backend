using Fundamental.Domain.Statements.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class FinancialStatementConfiguration : EntityTypeConfigurationBase<FinancialStatement>
{
    protected override void ExtraConfigure(EntityTypeBuilder<FinancialStatement> builder)
    {
        builder.ToTable("FinancialStatements", "fs");

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

        builder.OwnsOne(
            x => x.OperatingIncome,
            amount =>
            {
                amount.Property(money => money.Value)
                    .HasColumnName("OperatingIncome")
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
                    .HasColumnName("GrossProfit")
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
                    .HasColumnName("OperatingProfit")
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
                    .HasColumnName("BankInterestIncome")
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
                    .HasColumnName("InvestmentIncome")
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
                    .HasColumnName("NetProfit")
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
                    .HasColumnName("Expense")
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
                    .HasColumnName("Asset")
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
                    .HasColumnName("OwnersEquity")
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
                    .HasColumnName("Receivables")
                    .HasColumnType("decimal")
                    .HasPrecision(36, 10)
                    .IsRequired();

                amount.Property(money => money.Currency)
                    .UseCurrencyColumn();
            });
    }
}