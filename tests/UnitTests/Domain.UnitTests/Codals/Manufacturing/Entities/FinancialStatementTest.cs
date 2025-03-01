using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using JetBrains.Annotations;

namespace Domain.UnitTests.Codals.Manufacturing.Entities;

[TestSubject(subject: typeof(FinancialStatement))]
public class FinancialStatementTest
{
    [Theory]
    [InlineData(data: [6, 9, 9])]
    [InlineData(data: [1, 12, 1])]
    [InlineData(data: [12, 1, 11])]
    [InlineData(data: [3, 6, 9])]
    [InlineData(data: [11, 2, 9])]
    public void AdjustedMonth_ShouldReturnCorrectMonth(int reportMonth, int yearEndMonth, int expectedAdjustedMonth)
    {
        // Arrange
        StatementMonth month = new StatementMonth(month: reportMonth);

        StatementMonth adjustedMonth = month.AdjustedMonth(yearEndMonth: yearEndMonth);

        // Assert
        Assert.Equal(expected: expectedAdjustedMonth, actual: adjustedMonth.Month);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateMarketValue(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedMarketValue, actual: financialStatement.MarketValue);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateQuarterlyIncome(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedSpringOperationalIncome, actual: financialStatement.SpringOperationIncome);
        Assert.Equal(expected: testCase.ExpectedSummerOperationalIncome, actual: financialStatement.SummerOperationIncome);
        Assert.Equal(expected: testCase.ExpectedFallOperationalIncome, actual: financialStatement.FallOperationIncome);
        Assert.Equal(expected: testCase.ExpectedWinterOperationalIncome, actual: financialStatement.WinterOperationIncome);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateSaleAverageExcludeThisPeriod(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedSaleAverageExcludeThisPeriod, actual: financialStatement.SaleAverageExcludeThisPeriod);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateSaleAverageLastYearSamePeriod(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedSaleAverageLastYearSamePeriod, actual: financialStatement.SaleAverageLastYearSamePeriod);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateThisPeriodSaleRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedThisPeriodSaleRatio, actual: financialStatement.ThisPeriodSaleRatio);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateThisPeriodSaleRatioWithLastYear(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(
            expected: testCase.ExpectedThisPeriodSaleRatioWithLastYear,
            actual: financialStatement.ThisPeriodSaleRatioWithLastYear);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateGrossMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedGrossMargin, actual: financialStatement.GrossMargin);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOperationalMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedOperationalMargin, actual: financialStatement.OperationalMargin);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateNetMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedNetMargin, actual: financialStatement.NetMargin);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastSale(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedForecastSale, actual: financialStatement.ForecastSale);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastOperationalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedForecastOperationalProfit, actual: financialStatement.ForecastOperationalProfit);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastForecastTotalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedForecastTotalProfit, actual: financialStatement.ForecastTotalProfit);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastNoneOperationalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedForecastNoneOperationalProfit, actual: financialStatement.ForecastNoneOperationalProfit);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateTargetMarketCap(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedTargetMarketValue, actual: financialStatement.TargetMarketValue);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateTargetPrice(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedTargetPrice, actual: financialStatement.TargetPrice);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOptimalBuyPrice(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedTargetOptimalBuyPrice, actual: financialStatement.OptimalBuyPrice);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePe(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedPe, actual: financialStatement.Pe);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePs(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedPs, actual: financialStatement.Ps);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOwnersEquityRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedOwnersEquityRatio, actual: financialStatement.OwnersEquityRatio);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePa(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedPa, actual: financialStatement.Pa);
    }
    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePb(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedPb, actual: financialStatement.Pb);
    }

    [Theory]
    [MemberData(memberName: nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateReceivableRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase: testCase);

        // Act & Assert
        Assert.Equal(expected: testCase.ExpectedReceivableRatio, actual: financialStatement.ReceivableRatio);
    }

    // Strongly-typed test data provider
    public static IEnumerable<object[]> MarketValueTestCases()
    {
        yield return new object[]
        {
            new FinancialStatementTestCase
            {
                FiscalYear = new FiscalYear(year: 1_403),
                YearEndMonth = new StatementMonth(month: 12),
                ReportMonth = new StatementMonth(month: 9),
                LastClosePrice = 18_740m,
                MarketCap = 12_000_000_000m,
                ExpectedMarketValue = 224_880_000_000_000,
                OperationalIncome = new SignedCodalMoney(amount: 143_128_914),
                GrossProfitOrLoss = new SignedCodalMoney(amount: 41_964_779),
                OperationalProfitOrLoss = new SignedCodalMoney(amount: 38_670_368),
                NoneOperationalProfit = new SignedCodalMoney(amount: 189_406),
                Costs = new CodalMoney(amount: 1_272_585),
                NetProfitOrLoss = new SignedCodalMoney(amount: 50_074_345),
                Sale = new CodalMoney(amount: 16_024_842),
                SaleMonth = new StatementMonth(month: 10),
                SaleBeforeThisMonth = new CodalMoney(amount: 143_128_914),
                SaleLastYearSamePeriod = new CodalMoney(amount: 153_164_667),
                Assets = new CodalMoney(amount: 178_921_105),
                OwnersEquity = new CodalMoney(amount: 67_709_573),
                Receivables = new CodalMoney(amount: 65_615_475),
                LastYearNetProfit = new SignedCodalMoney(amount: 50_078_518),
                ExpectedSpringOperationalIncome = new SignedCodalMoney(amount: 0),
                ExpectedSummerOperationalIncome = new SignedCodalMoney(amount: 0),
                ExpectedFallOperationalIncome = new SignedCodalMoney(amount: 143_128_914),
                ExpectedWinterOperationalIncome = new SignedCodalMoney(amount: 0),
                ExpectedSaleAverageExcludeThisPeriod = new CodalMoney(amount: 15_903_213),
                ExpectedSaleAverageLastYearSamePeriod = new CodalMoney(amount: 15_316_467),
                ExpectedThisPeriodSaleRatio = 100.76m,
                ExpectedThisPeriodSaleRatioWithLastYear = 104.62m,
                ExpectedGrossMargin = 0.29m,
                ExpectedOperationalMargin = 0.27m,
                ExpectedNetMargin = 0.3499m,
                ExpectedForecastSale = new CodalMoney(amount: 190_984_507),
                ExpectedForecastOperationalProfit = new CodalMoney(amount: 51565817),
                ExpectedForecastNoneOperationalProfit = new CodalMoney(252541),
                ExpectedForecastTotalProfit = new CodalMoney(51_818_358),
                ExpectedTargetMarketValue = 362_728_506_000_000,
                ExpectedTargetPrice = 30_227,
                ExpectedTargetOptimalBuyPrice = 19_000,
                ExpectedPe = 4.34m,
                ExpectedPs = 1.18m,
                ExpectedOwnersEquityRatio = 37.84m,
                ExpectedPa = 1.26m,
                ExpectedPb = 3.32m,
                ExpectedReceivableRatio = 36.67m,
            },
        };
    }

    private static FinancialStatement CreateFinancialStatement(FinancialStatementTestCase testCase)
    {
        Guid id = Guid.NewGuid();
        Symbol symbol = Symbol.Empty;
        ulong traceNo = 123_456;
        IsoCurrency currency = IsoCurrency.IRR;
        DateTime createdAt = DateTime.Now;
        DateOnly lastClosePriceDate = DateOnly.FromDateTime(dateTime: DateTime.Now);
        FiscalYear fiscalYear = testCase.FiscalYear;
        StatementMonth yearEndMonth = testCase.YearEndMonth;
        StatementMonth reportMonth = testCase.ReportMonth;

        return new FinancialStatementBuilder()
            .SetId(id: id)
            .SetSymbol(symbol: symbol)
            .SetCurrency(currency: currency)
            .SetTraceNo(traceNo: traceNo)
            .SetFiscalYear(fiscalYear: fiscalYear)
            .SetYearEndMonth(yearEndMonth: yearEndMonth)
            .SetCreatedAt(createdAt: createdAt)
            .SetLastClosePrice(lastClosePrice: testCase.LastClosePrice, lastClosePriceDate: lastClosePriceDate)
            .SetMarketCap(marketCap: testCase.MarketCap)
            .SetIncomeStatement(
                reportMonth: reportMonth,
                operationalIncome: testCase.OperationalIncome,
                grossProfitOrLoss: testCase.GrossProfitOrLoss,
                operationalProfitOrLoss: testCase.OperationalProfitOrLoss,
                noneOperationalProfit: testCase.NoneOperationalProfit,
                costs: testCase.Costs,
                netProfitOrLoss: testCase.NetProfitOrLoss)
            .SetSale(
                sale: testCase.Sale,
                saleMonth: testCase.SaleMonth,
                saleBeforeThisMonth: testCase.SaleBeforeThisMonth,
                saleLastYearSamePeriod: testCase.SaleLastYearSamePeriod)
            .SetFinancialPosition(
                assets: testCase.Assets,
                ownersEquity: testCase.OwnersEquity,
                receivables: testCase.Receivables,
                lastYearNetProfit: testCase.LastYearNetProfit)
            .Build();
    }

    public class FinancialStatementTestCase
    {
        public FiscalYear FiscalYear { get; set; }
        public StatementMonth YearEndMonth { get; set; }
        public StatementMonth ReportMonth { get; set; }
        public decimal LastClosePrice { get; set; }
        public decimal MarketCap { get; set; }
        public decimal ExpectedMarketValue { get; set; }
        public SignedCodalMoney OperationalIncome { get; set; }
        public SignedCodalMoney GrossProfitOrLoss { get; set; }
        public SignedCodalMoney OperationalProfitOrLoss { get; set; }
        public SignedCodalMoney NoneOperationalProfit { get; set; }
        public CodalMoney Costs { get; set; }
        public SignedCodalMoney NetProfitOrLoss { get; set; }
        public CodalMoney Sale { get; set; }
        public StatementMonth SaleMonth { get; set; }
        public CodalMoney SaleBeforeThisMonth { get; set; }
        public CodalMoney SaleLastYearSamePeriod { get; set; }
        public CodalMoney Assets { get; set; }
        public CodalMoney OwnersEquity { get; set; }
        public CodalMoney Receivables { get; set; }
        public SignedCodalMoney LastYearNetProfit { get; set; }
        public SignedCodalMoney ExpectedSpringOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedSummerOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedFallOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedWinterOperationalIncome { get; set; }
        public CodalMoney ExpectedSaleAverageExcludeThisPeriod { get; set; }
        public CodalMoney ExpectedSaleAverageLastYearSamePeriod { get; set; }
        public decimal ExpectedThisPeriodSaleRatio { get; set; }
        public decimal ExpectedThisPeriodSaleRatioWithLastYear { get; set; }
        public decimal ExpectedGrossMargin { get; set; }
        public decimal ExpectedOperationalMargin { get; set; }
        public decimal ExpectedNetMargin { get; set; }
        public CodalMoney ExpectedForecastOperationalProfit { get; set; }
        public CodalMoney ExpectedForecastSale { get; set; }
        public CodalMoney ExpectedForecastNoneOperationalProfit { get; set; }
        public CodalMoney ExpectedForecastTotalProfit { get; set; }
        public decimal ExpectedTargetMarketValue { get; set; }
        public decimal ExpectedTargetPrice { get; set; }
        public decimal ExpectedTargetOptimalBuyPrice { get; set; }
        public decimal ExpectedPe { get; set; }
        public decimal ExpectedPs { get; set; }
        public decimal ExpectedOwnersEquityRatio { get; set; }
        public decimal ExpectedPa { get; set; }
        public decimal ExpectedPb { get; set; }
        public decimal ExpectedReceivableRatio { get; set; }
    }
}