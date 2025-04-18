using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class FinancialStatement : BaseEntity<Guid>
{
    internal FinancialStatement(
        Guid id,
        Symbol symbol,
        IsoCurrency currency,
        ulong traceNo,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        CreatedAt = createdAt.ToUniversalTime();
        Currency = currency;
        TraceNo = traceNo;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        UpdatedAt = createdAt.ToUniversalTime();
    }

    protected FinancialStatement()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    /// <summary>
    ///     سال مالی.
    /// </summary>
    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    /// <summary>
    ///     ماه آخر سال مالی.
    /// </summary>
    public StatementMonth YearEndMonth { get; } = StatementMonth.Empty;

    public decimal LastClosePrice { get; private set; }

    // public DateOnly LastClosePriceDate { get; private set; }

    public decimal MarketCap { get; private set; }

    public decimal MarketValue { get; private set; }

    /// <summary>
    ///     مال گزارش صورت وضعیت مالی.
    /// </summary>
    public StatementMonth ReportMonth { get; private set; } = StatementMonth.Empty;

    /// <summary>
    ///     ماه گزارش فروش.
    /// </summary>
    public StatementMonth SaleMonth { get; private set; } = StatementMonth.Empty;

    /// <summary>
    ///     درآمد عملیاتی.
    /// </summary>
    public SignedCodalMoney OperationalIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     سایر درآمد های عملیاتی.
    /// </summary>
    public SignedCodalMoney OtherOperationalIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     درآمد عملیاتی ماه بهار.
    /// </summary>
    public SignedCodalMoney SpringOperationIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     درآمد عملیاتی ماه تابستان.
    /// </summary>
    public SignedCodalMoney SummerOperationIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     درآمد عملیاتی ماه پاییز.
    /// </summary>
    public SignedCodalMoney FallOperationIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     درامد عملیاتی ماه زمستان.
    /// </summary>
    public SignedCodalMoney WinterOperationIncome { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     سود و زیان ناخالص.
    /// </summary>
    public SignedCodalMoney GrossProfitOrLoss { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     سود زیان عملیاتی.
    /// </summary>
    public SignedCodalMoney OperationalProfitOrLoss { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     سود سپردا بانکی.
    /// </summary>
    public List<NonOperationIncomeAndExpense> NonOperationIncomeAndExpenses { get; } = new();

    /// <summary>
    ///     شرکت های سرمایه پذیر.
    /// </summary>
    public List<StockOwnership> InvestmentsProfits { get; private set; } = new();

    /// <summary>
    ///     درآمدهای غیر عملیاتی.
    /// </summary>
    public SignedCodalMoney NoneOperationalProfit { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     هزینه ها.
    /// </summary>
    public CodalMoney Costs { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     سود و زیان خالص.
    /// </summary>
    public SignedCodalMoney NetProfitOrLoss { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     فروش ماه جاری.
    /// </summary>
    public CodalMoney Sale { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     فروش اول سال مالی تا ماه قبل سال جاری.
    /// </summary>
    public CodalMoney SaleBeforeThisMonth { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     فروش مدت مشابه سال گذشته.
    /// </summary>
    public CodalMoney SaleLastYearSamePeriod { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     فروش کل تا این ماه.
    /// </summary>
    public CodalMoney TotalSale { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     میانگین فروش از اول سال تا ماه قبل سال جاری.
    /// </summary>
    public CodalMoney SaleAverageExcludeThisPeriod { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     میانگین فروش مدت مشابه سال قبل.
    /// </summary>
    public CodalMoney SaleAverageLastYearSamePeriod { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     نسبت فروش آخرین ماه به میانگین سال جاری.
    /// </summary>
    public decimal ThisPeriodSaleRatio { get; private set; }

    /// <summary>
    ///     نسبت فروش به مد ت مشابه سال قبل.
    /// </summary>
    public decimal ThisPeriodSaleRatioWithLastYear { get; private set; }

    /// <summary>
    ///     /حاشیه سود ناخالص.
    /// </summary>
    public decimal GrossMargin { get; private set; }

    /// <summary>
    ///     حاشیه سود عملیاتی.
    /// </summary>
    public decimal OperationalMargin { get; private set; }

    /// <summary>
    ///     حاشیه سود خالص.
    /// </summary>
    public decimal NetMargin { get; private set; }

    /// <summary>
    ///     پیش بینی فروش.
    /// </summary>
    public CodalMoney ForecastSale { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     پیش بینی سود عملیاتی.
    /// </summary>
    public CodalMoney ForecastOperationalProfit { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     پیش بینی سود غیر عملیاتی.
    /// </summary>
    public CodalMoney ForecastNoneOperationalProfit { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     پیشبینی سود کل.
    /// </summary>
    public CodalMoney ForecastTotalProfit { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     ارزش بازار هدف.
    /// </summary>
    public decimal TargetMarketValue { get; private set; }

    /// <summary>
    ///     قیمت هدف.
    /// </summary>
    public decimal TargetPrice { get; private set; }

    /// <summary>
    ///     قیمت خرید.
    /// </summary>
    public decimal OptimalBuyPrice { get; private set; }

    public decimal Pe { get; private set; }

    public decimal Ps { get; private set; }

    /// <summary>
    ///     دارایی ها.
    /// </summary>
    public CodalMoney Assets { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     حقوق مالکانه.
    /// </summary>
    public CodalMoney OwnersEquity { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     نسبت حقوق مالکانه.
    /// </summary>
    public decimal OwnersEquityRatio { get; private set; }

    public decimal Pa { get; private set; }

    public decimal Pb { get; private set; }

    /// <summary>
    ///     دريافتني‌هاي تجاري و ساير دريافتني‌ها.
    /// </summary>
    public CodalMoney Receivables { get; private set; } = CodalMoney.Empty;

    /// <summary>
    ///     نسبت مطالبات.
    /// </summary>
    public decimal ReceivableRatio { get; private set; }

    /// <summary>
    ///     سود خالص مدت مشابه سال قبل.
    /// </summary>
    public SignedCodalMoney LastYearNetProfitOrLoss { get; private set; } = SignedCodalMoney.Empty;

    /// <summary>
    ///     نسبت رشد سود خالص نسبت سال قبل.
    /// </summary>
    public decimal NetProfitGrowthRatio { get; private set; }

    public decimal Peg { get; private set; }

    public CodalMoney DpsLastYear { get; private set; } = CodalMoney.Empty;

    public CodalMoney DpsTwoYearsAgo { get; private set; } = CodalMoney.Empty;

    public CodalMoney DpsRatioLastYear { get; private set; } = CodalMoney.Empty;

    public CodalMoney DpsRatioTwoYearsAgo { get; private set; } = CodalMoney.Empty;

    public uint ConcurrencyToken { get; private set; }

    public FinancialStatement SetLastClosePrice(decimal lastClosePrice, DateOnly lastClosePriceDate)
    {
        LastClosePrice = lastClosePrice;
        Calculate();
        return this;
    }

    public FinancialStatement SetMarketCap(decimal marketCap)
    {
        MarketCap = marketCap;
        Calculate();

        return this;
    }

    public FinancialStatement SetIncomeStatement(
        StatementMonth reportMonth,
        SignedCodalMoney operationalIncome,
        SignedCodalMoney otherOperationalIncome,
        SignedCodalMoney grossProfitOrLoss,
        SignedCodalMoney operationalProfitOrLoss,
        SignedCodalMoney noneOperationalProfit,
        CodalMoney costs,
        SignedCodalMoney netProfitOrLoss
    )
    {
        ReportMonth = reportMonth;
        OperationalIncome = operationalIncome;
        GrossProfitOrLoss = grossProfitOrLoss;
        OtherOperationalIncome = otherOperationalIncome;
        OperationalProfitOrLoss = operationalProfitOrLoss;
        NoneOperationalProfit = noneOperationalProfit.Value > 0 ? noneOperationalProfit : 0;
        Costs = costs;
        NetProfitOrLoss = netProfitOrLoss;
        Calculate();

        return this;
    }

    public FinancialStatement SetFinancialPosition(
        CodalMoney assets,
        CodalMoney ownersEquity,
        CodalMoney receivables,
        SignedCodalMoney lastYearNetProfit
    )
    {
        Assets = assets;
        OwnersEquity = ownersEquity;
        Receivables = receivables;
        LastYearNetProfitOrLoss = lastYearNetProfit;
        Calculate();

        return this;
    }

    public FinancialStatement SetSale(
        CodalMoney sale,
        StatementMonth saleMonth,
        CodalMoney saleBeforeThisMonth,
        CodalMoney saleLastYearSamePeriod
    )
    {
        Sale = sale;
        SaleMonth = saleMonth;
        SaleBeforeThisMonth = saleBeforeThisMonth;
        SaleLastYearSamePeriod = saleLastYearSamePeriod;
        SaleAverageExcludeThisPeriod = saleMonth.AdjustedMonth(YearEndMonth) == 1
            ? saleMonth
            : Math.Ceiling(saleBeforeThisMonth / (saleMonth.AdjustedMonth(YearEndMonth) - 1));
        SaleAverageLastYearSamePeriod = saleMonth.AdjustedMonth(YearEndMonth) == 1
            ? saleMonth
            : Math.Ceiling(saleLastYearSamePeriod / saleMonth.AdjustedMonth(YearEndMonth));

        TotalSale = SaleBeforeThisMonth + Sale;
        Calculate();
        return this;
    }

    public void Calculate()
    {
        CalculateMarketValue();
        CalculateGrossMargin();
        CalculateOperationalMargin();
        CalculateNetMargin();
        CalculateForecastSale();
        CalculateForecastOperationalProfit();
        CalculateForecastNoneOperationalProfit();
        CalculateForecastTotalProfit();
        CalculateTargetMarketValue();
        CalculateTargetPrice();
        CalculateOptimalBuyPrice();

        CalculatePe();
        CalculatePs();
        CalculateOwnersEquityRatio();
        CalculatePa();
        CalculatePb();
        CalculateReceivableRatio();
        CalculateNetProfitGrowthRatio();
        SetSeasonOperationalIncome();
        CalculateSaleRatio();
    }

    private void CalculateSaleRatio()
    {
        ThisPeriodSaleRatio = Math.Round((SaleBeforeThisMonth == 0 ? 0 : (Sale.Value / SaleAverageExcludeThisPeriod) - 1) * 100, 2);
        ThisPeriodSaleRatioWithLastYear =
            Math.Round((SaleLastYearSamePeriod == 0 ? 0 : (Sale.Value / SaleAverageLastYearSamePeriod) - 1) * 100, 2);
    }

    /// <summary>
    ///     محاسبه ارزش بازار.
    /// </summary>
    private void CalculateMarketValue()
    {
        MarketValue = LastClosePrice * MarketCap;
    }

    /// <summary>
    ///     محاسبه حاشیه سود ناخالص.
    /// </summary>
    private void CalculateGrossMargin()
    {
        GrossMargin = OperationalIncome == 0 ? 0 : Math.Round(GrossProfitOrLoss / OperationalIncome, 2);
    }

    /// <summary>
    ///     محاسبه حاشیه سود عملیاتی.
    /// </summary>
    private void CalculateOperationalMargin()
    {
        OperationalMargin = OperationalIncome == 0 ? 0 : Math.Round(OperationalProfitOrLoss / OperationalIncome, 2);
    }

    /// <summary>
    ///     محاسبه حاشیه سود خالص.
    /// </summary>
    private void CalculateNetMargin()
    {
        NetMargin = OperationalIncome == 0
            ? 0
            : Math.Round((NetProfitOrLoss.Value - OtherOperationalIncome.Value - NoneOperationalProfit.Value) / OperationalIncome, 4);
    }

    /// <summary>
    ///     پیش بینی فروش سالانه.
    /// </summary>
    private void CalculateForecastSale()
    {
        ForecastSale = ReportMonth.IsEmptyStatementMonth() ? 0 : Math.Round(TotalSale / SaleMonth.AdjustedMonth(YearEndMonth) * 12);
    }

    /// <summary>
    ///     پیش بینی سود عملیاتی.
    /// </summary>
    private void CalculateForecastOperationalProfit()
    {
        if (NetMargin < 0)
        {
            return;
        }

        ForecastOperationalProfit = Math.Round(ForecastSale.Value * NetMargin);
    }

    /// <summary>
    ///     پیش بینی سود غیر عملیاتی.
    /// </summary>
    private void CalculateForecastNoneOperationalProfit()
    {
        if (!NonOperationIncomeAndExpenses.Any())
        {
            if (NoneOperationalProfit.Value < 0)
            {
                return;
            }

            ForecastNoneOperationalProfit = ReportMonth.IsEmptyStatementMonth()
                ? 0
                : Math.Round(NoneOperationalProfit / ReportMonth.AdjustedMonth(YearEndMonth) * 12);
            return;
        }

        decimal profit = NonOperationIncomeAndExpenses.Where(x => x.Tags.Any())
            .Sum(x => x.Value.Value);

        ForecastNoneOperationalProfit = ReportMonth.IsEmptyStatementMonth() ? 0 : profit / ReportMonth.AdjustedMonth(YearEndMonth) * 12;
    }

    /// <summary>
    ///     پیش بینی سود کل.
    /// </summary>
    private void CalculateForecastTotalProfit()
    {
        // TODO : here I should add the other none operational profits like investments profits
        ForecastTotalProfit = ForecastOperationalProfit + ForecastNoneOperationalProfit;
    }

    /// <summary>
    ///     ارزش بازار هدف.
    /// </summary>
    private void CalculateTargetMarketValue()
    {
        TargetMarketValue = ForecastTotalProfit.RealValue * 7;
    }

    /// <summary>
    ///     محاسبه قیمت هدف.
    /// </summary>
    private void CalculateTargetPrice()
    {
        TargetPrice = MarketCap == 0 ? 0 : Math.Truncate(TargetMarketValue / MarketCap);
    }

    /// <summary>
    ///     قیمت خرید.
    /// </summary>
    private void CalculateOptimalBuyPrice()
    {
        OptimalBuyPrice = MarketCap == 0 ? 0 : Math.Truncate(ForecastTotalProfit.RealValue * 4.4M / MarketCap);
    }

    private void CalculatePe()
    {
        Pe = ForecastTotalProfit == 0 ? 0 : Math.Round(MarketValue / ForecastTotalProfit.RealValue, 2);
    }

    private void CalculatePs()
    {
        Ps = ForecastSale == 0 ? 0 : Math.Round(MarketValue / ForecastSale.RealValue, 2);
    }

    /// <summary>
    ///     نسبت حقوق مالکانه.
    /// </summary>
    private void CalculateOwnersEquityRatio()
    {
        OwnersEquityRatio = Assets == 0 ? 0 : Math.Round(OwnersEquity / Assets * 100, 2);
    }

    private void CalculatePa()
    {
        Pa = Assets == 0 ? 0 : Math.Round(MarketValue / Assets.RealValue, 2);
    }

    private void CalculatePb()
    {
        Pb = OwnersEquity == 0 ? 0 : Math.Round(MarketValue / OwnersEquity.RealValue, 2);
    }

    /// <summary>
    ///     نسبت مطالبات.
    /// </summary>
    private void CalculateReceivableRatio()
    {
        ReceivableRatio = Assets == 0 ? 0 : Math.Round(Receivables / Assets * 100, 2);
    }

    /// <summary>
    ///     سود خالص مدت مشابه سال قبل.
    /// </summary>
    private void CalculateNetProfitGrowthRatio()
    {
        NetProfitGrowthRatio = LastYearNetProfitOrLoss == 0 ? 0 : Math.Round(NetProfitOrLoss / LastYearNetProfitOrLoss * 100);
    }

    private static Season GetSession(StatementMonth month)
    {
        return month.Month switch
        {
            3 => Season.SPRING,
            6 => Season.SUMMER,
            9 => Season.FALL,
            _ => Season.WINTER
        };
    }

    private void SetSeasonOperationalIncome()
    {
        Season session = GetSession(ReportMonth);

        switch (session)
        {
            case Season.SPRING:
                SpringOperationIncome = OperationalIncome;
                break;
            case Season.SUMMER:
                SummerOperationIncome = OperationalIncome;
                break;
            case Season.FALL:
                FallOperationIncome = OperationalIncome;
                break;
            case Season.WINTER:
                WinterOperationIncome = OperationalIncome;
                break;
        }
    }
}