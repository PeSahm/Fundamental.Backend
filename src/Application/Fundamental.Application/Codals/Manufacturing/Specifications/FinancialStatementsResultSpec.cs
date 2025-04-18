using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class FinancialStatementsResultSpec : Specification<FinancialStatement, GetFinancialStatementsResultDto>
{
    public FinancialStatementsResultSpec()
    {
        Query.Select(x => new GetFinancialStatementsResultDto
        {
            Isin = x.Symbol.Isin,
            Name = x.Symbol.Name,
            TraceNo = x.TraceNo,
            FiscalYear = x.FiscalYear,
            ReportMonth = x.ReportMonth,
            OperationalIncome = x.OperationalIncome,
            GrossProfitOrLoss = x.GrossProfitOrLoss,
            OperationalProfitOrLoss = x.OperationalProfitOrLoss,
            NetProfitOrLoss = x.NetProfitOrLoss,
            OtherOperationalIncome = x.OtherOperationalIncome,
            NoneOperationalProfit = x.NoneOperationalProfit,
            Sale = x.Sale,
            SaleBeforeThisMonth = x.SaleBeforeThisMonth,
            Assets = x.Assets,
            MarketValue = x.MarketValue,
            OwnersEquity = x.OwnersEquity,
            LastClosePrice = x.LastClosePrice,
            MarketCap = x.MarketCap,
            Pa = x.Pa,
            Pe = x.Pe,
            Receivables = x.Receivables,
            NetMargin = x.NetMargin,
            Pb = x.Pb,
            Ps = x.Ps,
            ForecastSale = x.ForecastSale,
            ReceivableRatio = x.ReceivableRatio,
            ForecastTotalProfit = x.ForecastTotalProfit,
            OwnersEquityRatio = x.OwnersEquityRatio,
            ThisPeriodSaleRatio = x.ThisPeriodSaleRatio,
            SaleAverageExcludeThisPeriod = x.SaleAverageExcludeThisPeriod,
            SaleAverageLastYearSamePeriod = x.SaleAverageLastYearSamePeriod,
            ThisPeriodSaleRatioWithLastYear = x.ThisPeriodSaleRatioWithLastYear
        });
    }

    public FinancialStatementsResultSpec OrderByLastRecord()
    {
        Query.OrderByDescending(x => x.TraceNo)
            .ThenByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month);
        return this;
    }
}