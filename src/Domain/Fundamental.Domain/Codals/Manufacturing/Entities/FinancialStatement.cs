using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class FinancialStatement : BaseEntity<Guid>
{
    public FinancialStatement(
        Guid id,
        Symbol symbol,
        IsoCurrency currency,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        CreatedAt = createdAt;
        Currency = currency;
        UpdatedAt = createdAt;
    }

    protected FinancialStatement()
    {
    }

    public Symbol Symbol { get; private set; }

    public decimal LastClosePrice { get; private set; }

    public DateOnly LastClosePriceDate { get; private set; }

    public decimal MarketCap { get; private set; }

    public decimal MarketValue { get; private set; }

    /// <summary>
    /// سال مالی
    /// </summary>
    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    /// <summary>
    /// ماه آخر سال مالی
    /// </summary>
    public StatementMonth YearEndMonth { get; private set; }

    /// <summary>
    /// مال گزارش صورت وضعیت مالی
    /// </summary>
    public StatementMonth ReportMonth { get; private set; }

    /// <summary>
    /// ماه گزارش فروش
    /// </summary>
    public StatementMonth SaleMonth { get; private set; }

    /// <summary>
    /// درآمد عملیاتی
    /// </summary>
    public SignedCodalMoney OperationalIncome { get; set; }

    /// <summary>
    /// درآمد عملیاتی ماه بهار
    /// </summary>

    public SignedCodalMoney SpringOperationIncome { get; set; }

    /// <summary>
    /// درآمد عملیاتی ماه تابستان
    /// </summary>

    public SignedCodalMoney SummerOperationIncome { get; set; }

    /// <summary>
    /// درآمد عملیاتی ماه پاییز
    /// </summary>
    public SignedCodalMoney FallOperationIncome { get; set; }

    /// <summary>
    /// درامد عملیاتی ماه زمستان
    /// </summary>
    public SignedCodalMoney WinterOperationIncome { get; set; }

    /// <summary>
    /// سود و زیان ناخالص
    /// </summary>
    public SignedCodalMoney GrossProfitOrLoss { get; set; }

    /// <summary>
    /// سود زیان عملیاتی
    /// </summary>
    public SignedCodalMoney OperationalProfitOrLoss { get; set; }

    /// <summary>
    /// سود سپردا بانکی
    /// </summary>
    public SignedCodalMoney BankInterest { get; set; }

    /// <summary>
    ///  شرکت های سرمایه پذیر
    /// </summary>
    public List<SubCompanyNetProfit> InvestmentsProfits { get; set; }

    /// <summary>
    /// سود سرمایه گذاری ها
    /// </summary>
    public CodalMoney InvestmentsProfit { get; set; }

    /// <summary>
    /// هزینه ها
    /// </summary>
    public CodalMoney Costs { get; private set; }

    /// <summary>
    /// سود و زیان خالص
    /// </summary>
    public SignedCodalMoney NetProfitOrLoss { get; set; }

    /// <summary>
    /// فروش ماه جاری
    /// </summary>
    public CodalMoney Sale { get; set; }

    /// <summary>
    ///  فروش اول سال مالی تا ماه قبل سال جاری
    /// </summary>
    public CodalMoney SaleBeforeThisMonth { get; set; }

    /// <summary>
    /// فروش مدت مشابه سال گذشته
    /// </summary>
    public CodalMoney SaleLastYearSamePeriod { get; set; }

    /// <summary>
    /// فروش کل تا این ماه
    /// </summary>
    public CodalMoney TotalSale { get; private set; }

    /// <summary>
    /// میانگین فروش از اول سال تا ماه قبل سال جاری
    /// </summary>
    public CodalMoney SaleAverageExcludeThisPeriod { get; set; }

    /// <summary>
    /// میانگین فروش مدت مشابه سال قبل
    /// </summary>
    public CodalMoney SaleAverageLastYearSamePeriod { get; set; }

    /// <summary>
    /// نسبت فروش آخرین ماه به میانگین سال جاری
    /// </summary>
    public decimal ThisPeriodSaleRatio { get; set; }

    /// <summary>
    /// نسبت فروش به مد ت مشابه سال قبل
    /// </summary>
    public decimal ThisPeriodSaleRatioWithLastYear { get; set; }

    /// <summary>
    /// /حاشیه سود ناخالص
    /// </summary>
    public decimal GrossMargin { get; set; }

    /// <summary>
    /// حاشیه سود عملیاتی
    /// </summary>
    public decimal OperationalMargin { get; set; }

    /// <summary>
    /// حاشیه سود خالص
    /// </summary>
    public decimal NetMargin { get; set; }

    /// <summary>
    /// پیش بینی فروش
    /// </summary>
    public CodalMoney ForecastSale { get; set; }

    /// <summary>
    /// پیش بینی سود عملیاتی
    /// </summary>
    public CodalMoney ForecastOperationalProfit { get; set; }

    /// <summary>
    /// پیش بینی سود غیر عملیاتی
    /// </summary>
    public CodalMoney ForecastNoneOperationalProfit { get; set; }

    /// <summary>
    /// پیشبینی سود کل
    /// </summary>
    public CodalMoney ForecastTotalProfit { get; set; }

    /// <summary>
    /// ارزش بازار هدف
    /// </summary>
    public decimal TargetMarketValue { get; set; }

    /// <summary>
    /// قیمت هدف
    /// </summary>
    public decimal TargetPrice { get; set; }

    /// <summary>
    /// قیمت خرید
    /// </summary>
    public decimal OptimalBuyPrice { get; set; }

    public decimal Pe { get; set; }

    public decimal Ps { get; set; }

    /// <summary>
    /// دارایی ها
    /// </summary>
    public CodalMoney Assets { get; set; }

    /// <summary>
    /// حقوق مالکانه
    /// </summary>
    public CodalMoney OwnersEquity { get; set; }

    /// <summary>
    /// نسبت حقوق مالکانه
    /// </summary>
    public decimal OwnersEquityRatio { get; set; }

    public decimal Pa { get; set; }

    public decimal Pb { get; set; }

    /// <summary>
    /// دريافتني‌هاي تجاري و ساير دريافتني‌ها
    /// </summary>
    public CodalMoney Receivables { get; set; }

    /// <summary>
    /// نسبت مطالبات
    /// </summary>
    public decimal ReceivableRatio { get; set; }

    /// <summary>
    /// سود خالص مدت مشابه سال قبل
    /// </summary>
    public SignedCodalMoney LastYearNetProfitOrLoss { get; set; }

    /// <summary>
    /// نسبت رشد سود خالص نسبت سال قبل
    /// </summary>
    public decimal NetProfitGrowthRatio { get; set; }

    public decimal Peg { get; set; }

    public CodalMoney DpsLastYear { get; set; }

    public CodalMoney DpsTwoYearsAgo { get; set; }

    public CodalMoney DpsRatioLastYear { get; set; }

    public CodalMoney DpsRatioTwoYearsAgo { get; set; }

    public FinancialStatement SetLastClosePrice(decimal lastClosePrice, DateOnly lastClosePriceDate)
    {
        LastClosePrice = lastClosePrice;
        LastClosePriceDate = lastClosePriceDate;
        return this;
    }

    public FinancialStatement SetMarketCap(decimal marketCap)
    {
        MarketCap = marketCap;
        return this;
    }

    public FinancialStatement SetIncomeStatement(
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        SignedCodalMoney operationalIncome,
        SignedCodalMoney grossProfitOrLoss,
        SignedCodalMoney operationalProfitOrLoss,
        SignedCodalMoney bankInterest,
        List<SubCompanyNetProfit> investmentsProfits,
        CodalMoney investmentsProfit,
        CodalMoney costs,
        SignedCodalMoney netProfitOrLoss
    )
    {
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        OperationalIncome = operationalIncome;
        GrossProfitOrLoss = grossProfitOrLoss;
        OperationalProfitOrLoss = operationalProfitOrLoss;
        BankInterest = bankInterest;
        InvestmentsProfits = investmentsProfits;
        InvestmentsProfit = investmentsProfit;
        Costs = costs;
        NetProfitOrLoss = netProfitOrLoss;
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
        return this;
    }

    public void SetSale(
        CodalMoney sale,
        StatementMonth saleMonth,
        List<CodalMoney> saleBeforeThisMonth,
        List<CodalMoney> saleLastYearSamePeriod
    )
    {
        Sale = sale;
        SaleMonth = saleMonth;
        SaleBeforeThisMonth = saleBeforeThisMonth.Sum(x => x.Value);
        SaleLastYearSamePeriod = saleLastYearSamePeriod.Sum(x => x.Value);
        SaleAverageExcludeThisPeriod = saleBeforeThisMonth.Average(x => x.Value);
        SaleAverageLastYearSamePeriod = saleLastYearSamePeriod.Average(x => x.Value);
    }

    private void Calculate()
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
        ThisPeriodSaleRatio = Sale.Value / SaleBeforeThisMonth;
        ThisPeriodSaleRatioWithLastYear = Sale.Value / SaleLastYearSamePeriod;
    }

    /// <summary>
    /// محاسبه ارزش بازار
    /// </summary>
    private void CalculateMarketValue()
    {
        MarketValue = LastClosePrice * MarketCap;
    }

    /// <summary>
    /// محاسبه حاشیه سود ناخالص
    /// </summary>
    private void CalculateGrossMargin()
    {
        GrossMargin = GrossProfitOrLoss / OperationalIncome;
    }

    /// <summary>
    /// محاسبه حاشیه سود عملیاتی
    /// </summary>
    private void CalculateOperationalMargin()
    {
        OperationalMargin = OperationalProfitOrLoss / OperationalIncome;
    }

    /// <summary>
    /// محاسبه حاشیه سود خالص
    /// </summary>
    private void CalculateNetMargin()
    {
        NetMargin = NetProfitOrLoss / OperationalIncome;
    }

    /// <summary>
    /// پیش بینی فروش سالانه
    /// </summary>
    private void CalculateForecastSale()
    {
        ForecastSale = TotalSale / ReportMonth.AdjustedMonth(YearEndMonth) * 12;
    }

    /// <summary>
    /// پیش بینی سود عملیاتی
    /// </summary>
    private void CalculateForecastOperationalProfit()
    {
        ForecastOperationalProfit = ForecastSale.Value * OperationalMargin;
    }

    /// <summary>
    /// پیش بینی سود غیر عملیاتی
    /// </summary>
    private void CalculateForecastNoneOperationalProfit()
    {
        ForecastNoneOperationalProfit = BankInterest / ReportMonth.AdjustedMonth(YearEndMonth) * 12;
    }

    /// <summary>
    /// پیش بینی سود کل
    /// </summary>
    private void CalculateForecastTotalProfit()
    {
        // TODO : here I should add the other none operational profits like investments profits
        ForecastTotalProfit = ForecastOperationalProfit + ForecastNoneOperationalProfit;
    }

    /// <summary>
    /// ارزش بازار هدف
    /// </summary>
    private void CalculateTargetMarketValue()
    {
        TargetMarketValue = ForecastTotalProfit * 7;
    }

    /// <summary>
    /// محاسبه قیمت هدف
    /// </summary>
    private void CalculateTargetPrice()
    {
        TargetPrice = TargetMarketValue / MarketCap;
    }

    /// <summary>
    /// قیمت خرید
    /// </summary>
    private void CalculateOptimalBuyPrice()
    {
        OptimalBuyPrice = (ForecastTotalProfit * 4.4M) / MarketCap;
    }

    private void CalculatePe()
    {
        Pe = MarketValue / ForecastTotalProfit;
    }

    private void CalculatePs()
    {
        Ps = MarketValue / ForecastSale;
    }

    /// <summary>
    /// نسبت حقوق مالکانه
    /// </summary>
    private void CalculateOwnersEquityRatio()
    {
        OwnersEquityRatio = OwnersEquity / Assets;
    }

    private void CalculatePa()
    {
        Pa = MarketValue / Assets;
    }

    private void CalculatePb()
    {
        Pb = MarketValue / OwnersEquity;
    }

    /// <summary>
    /// نسبت مطالبات
    /// </summary>
    private void CalculateReceivableRatio()
    {
        ReceivableRatio = Receivables / Assets;
    }

    /// <summary>
    ///سود خالص مدت مشابه سال قبل
    /// </summary>
    private void CalculateNetProfitGrowthRatio()
    {
        NetProfitGrowthRatio = NetProfitOrLoss / LastYearNetProfitOrLoss;
    }

    private Season GetSession(StatementMonth month)
    {
        return month.Month switch
        {
            3 => Season.Spring,
            6 => Season.Summer,
            9 => Season.Fall,
            _ => Season.Winter,
        };
    }

    private void SetSeasonOperationalIncome()
    {
        Season session = GetSession(ReportMonth);

        switch (session)
        {
            case Season.Spring:
                SpringOperationIncome = OperationalIncome;
                break;
            case Season.Summer:
                SummerOperationIncome = OperationalIncome;
                break;
            case Season.Fall:
                FallOperationIncome = OperationalIncome;
                break;
            case Season.Winter:
                WinterOperationIncome = OperationalIncome;
                break;
        }
    }
}

public sealed class SubCompanyNetProfit
{
}

public enum Season
{
    Spring = 1,
    Summer = 2,
    Fall = 3,
    Winter = 4
}