using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class FinancialStatementConfiguration : EntityTypeConfigurationBase<FinancialStatement>
{
    protected override void ExtraConfigure(EntityTypeBuilder<FinancialStatement> builder)
    {
        builder.ToTable("financial-statement", "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol-id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType("BIGINT")
            .IsRequired();

        builder.Property(x => x.Currency)
            .Currency();

        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("fiscal-year")
                    .HasColumnType("SMALLINT")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("year-end-month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("report-month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.SaleMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("sale-month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.Property(x => x.LastClosePrice)
            .HasPrecision(36, 10);

        builder.Property(x => x.MarketCap)
            .HasPrecision(36, 10);

        builder.Property(x => x.MarketValue)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.OperationalIncome,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.OtherOperationalIncome,
            amount => amount.UseSignedCodalMoney()
        );


        builder.ComplexProperty(
            x => x.SpringOperationIncome,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.SummerOperationIncome,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.FallOperationIncome,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.WinterOperationIncome,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.GrossProfitOrLoss,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.OperationalProfitOrLoss,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.NoneOperationalProfit,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.Costs,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.NetProfitOrLoss,
            amount => amount.UseSignedCodalMoney()
        );

        builder.ComplexProperty(
            x => x.Sale,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.SaleBeforeThisMonth,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.SaleLastYearSamePeriod,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.TotalSale,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.SaleAverageExcludeThisPeriod,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.SaleAverageLastYearSamePeriod,
            amount => amount.UseCodalMoney()
        );

        builder.Property(x => x.ThisPeriodSaleRatio)
            .HasPrecision(36, 10);

        builder.Property(x => x.ThisPeriodSaleRatioWithLastYear)
            .HasPrecision(36, 10);

        builder.Property(x => x.GrossMargin)
            .HasPrecision(36, 10);

        builder.Property(x => x.OperationalMargin)
            .HasPrecision(36, 10);

        builder.Property(x => x.NetMargin)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.ForecastSale,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.ForecastOperationalProfit,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.ForecastNoneOperationalProfit,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.ForecastTotalProfit,
            amount => amount.UseCodalMoney()
        );

        builder.Property(x => x.TargetMarketValue)
            .HasPrecision(36, 10);

        builder.Property(x => x.TargetPrice)
            .HasPrecision(36, 10);

        builder.Property(x => x.OptimalBuyPrice)
            .HasPrecision(36, 10);

        builder.Property(x => x.Pe)
            .HasPrecision(36, 10);

        builder.Property(x => x.Ps)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.Assets,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.OwnersEquity,
            amount => amount.UseCodalMoney()
        );

        builder.Property(x => x.OwnersEquityRatio)
            .HasPrecision(36, 10);

        builder.Property(x => x.Pa)
            .HasPrecision(36, 10);

        builder.Property(x => x.Pb)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.Receivables,
            amount => amount.UseCodalMoney()
        );

        builder.Property(x => x.ReceivableRatio)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.LastYearNetProfitOrLoss,
            amount => amount.UseSignedCodalMoney()
        );

        builder.Property(x => x.NetProfitGrowthRatio)
            .HasPrecision(36, 10);

        builder.Property(x => x.Peg)
            .HasPrecision(36, 10);

        builder.ComplexProperty(
            x => x.DpsLastYear,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.DpsTwoYearsAgo,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.DpsRatioLastYear,
            amount => amount.UseCodalMoney()
        );

        builder.ComplexProperty(
            x => x.DpsRatioTwoYearsAgo,
            amount => amount.UseCodalMoney()
        );

        builder.Ignore(x => x.NonOperationIncomeAndExpenses);

        builder.Ignore(x => x.InvestmentsProfits);

        builder.Property(x => x.ConcurrencyToken)
            .IsRowVersion();
    }
}