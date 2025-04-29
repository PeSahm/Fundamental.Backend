using Fundamental.Application.Symbols.Queries.GetSymbols;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

public class GetFinancialStatementsResultDto
{
    public string Isin { get; set; }
    public string TseInsCode { get; set; }
    public string Name { get; set; }
    public ulong TraceNo { get; set; }
    public uint FiscalYear { get; set; }
    public ushort ReportMonth { get; set; }
    public decimal OperationalIncome { get; set; }
    public decimal GrossProfitOrLoss { get; set; }
    public decimal OperationalProfitOrLoss { get; set; }
    public decimal NetProfitOrLoss { get; set; }
    public decimal OtherOperationalIncome { get; set; }
    public decimal NoneOperationalProfit { get; set; }
    public decimal Sale { get; set; }
    public decimal SaleBeforeThisMonth { get; set; }
    public decimal Assets { get; set; }
    public decimal MarketValue { get; set; }
    public decimal OwnersEquity { get; set; }
    public decimal LastClosePrice { get; set; }
    public decimal MarketCap { get; set; }
    public decimal Receivables { get; set; }
    public decimal NetMargin { get; set; }
    public decimal Pe { get; set; }
    public decimal Ps { get; set; }
    public decimal Pa { get; set; }
    public decimal Pb { get; set; }
    public decimal ForecastSale { get; set; }
    public decimal ForecastTotalProfit { get; set; }
    public decimal OwnersEquityRatio { get; set; }
    public decimal ReceivableRatio { get; set; }
    public decimal ThisPeriodSaleRatio { get; set; }
    public decimal ThisPeriodSaleRatioWithLastYear { get; set; }
    public decimal SaleAverageExcludeThisPeriod { get; set; }
    public decimal SaleAverageLastYearSamePeriod { get; set; }

    public SymbolPriceInfo? PriceInfo { get; set; }
}