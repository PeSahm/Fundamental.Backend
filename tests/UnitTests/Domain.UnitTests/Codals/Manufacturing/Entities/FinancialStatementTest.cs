using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using JetBrains.Annotations;

namespace Domain.UnitTests.Codals.Manufacturing.Entities;

[TestSubject(typeof(FinancialStatement))]
public class FinancialStatementTest
{
    [Theory]
    [InlineData([6, 9, 9])]
    [InlineData([1, 12, 1])]
    [InlineData([12, 1, 11])]
    [InlineData([3, 6, 9])]
    [InlineData([11, 2, 9])]
    public void AdjustedMonth_ShouldReturnCorrectMonth(int reportMonth, int yearEndMonth, int expectedAdjustedMonth)
    {
        // Arrange
        StatementMonth month = new(reportMonth);

        StatementMonth adjustedMonth = month.AdjustedMonth(yearEndMonth);

        // Assert
        Assert.Equal(expectedAdjustedMonth, adjustedMonth.Month);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateMarketValue(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedMarketValue, financialStatement.MarketValue);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateQuarterlyIncome(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedSpringOperationalIncome, financialStatement.SpringOperationIncome);
        Assert.Equal(testCase.ExpectedSummerOperationalIncome, financialStatement.SummerOperationIncome);
        Assert.Equal(testCase.ExpectedFallOperationalIncome, financialStatement.FallOperationIncome);
        Assert.Equal(testCase.ExpectedWinterOperationalIncome, financialStatement.WinterOperationIncome);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateSaleAverageExcludeThisPeriod(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedSaleAverageExcludeThisPeriod, financialStatement.SaleAverageExcludeThisPeriod);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateSaleAverageLastYearSamePeriod(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedSaleAverageLastYearSamePeriod, financialStatement.SaleAverageLastYearSamePeriod);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateThisPeriodSaleRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedThisPeriodSaleRatio, financialStatement.ThisPeriodSaleRatio);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateThisPeriodSaleRatioWithLastYear(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(
            testCase.ExpectedThisPeriodSaleRatioWithLastYear,
            financialStatement.ThisPeriodSaleRatioWithLastYear);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateGrossMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedGrossMargin, financialStatement.GrossMargin);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOperationalMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedOperationalMargin, financialStatement.OperationalMargin);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateNetMargin(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedNetMargin, financialStatement.NetMargin);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastSale(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedForecastSale, financialStatement.ForecastSale);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastOperationalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedForecastOperationalProfit, financialStatement.ForecastOperationalProfit);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastForecastTotalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedForecastTotalProfit, financialStatement.ForecastTotalProfit);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateForecastNoneOperationalProfit(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedForecastNoneOperationalProfit, financialStatement.ForecastNoneOperationalProfit);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateTargetMarketCap(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedTargetMarketValue, financialStatement.TargetMarketValue);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateTargetPrice(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedTargetPrice, financialStatement.TargetPrice);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOptimalBuyPrice(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedTargetOptimalBuyPrice, financialStatement.OptimalBuyPrice);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePe(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedPe, financialStatement.Pe);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePs(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedPs, financialStatement.Ps);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateOwnersEquityRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedOwnersEquityRatio, financialStatement.OwnersEquityRatio);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePa(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedPa, financialStatement.Pa);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculatePb(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedPb, financialStatement.Pb);
    }

    [Theory]
    [MemberData(nameof(MarketValueTestCases))]
    public void Build_ShouldCalculateReceivableRatio(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedReceivableRatio, financialStatement.ReceivableRatio);
    }

    // Strongly-typed test data provider
    public static IEnumerable<object[]> MarketValueTestCases()
    {
        yield return new object[]
        {
            new FinancialStatementTestCase
            {
                FiscalYear = new FiscalYear(1_403),
                YearEndMonth = new StatementMonth(12),
                ReportMonth = new StatementMonth(9),
                LastClosePrice = 18_740m,
                MarketCap = 12_000_000_000m,
                ExpectedMarketValue = 224_880_000_000_000,
                OperationalIncome = new SignedCodalMoney(143_128_914),
                GrossProfitOrLoss = new SignedCodalMoney(41_964_779),
                OperationalProfitOrLoss = new SignedCodalMoney(38_670_368),
                NoneOperationalProfit = new SignedCodalMoney(189_406),
                Costs = new CodalMoney(1_272_585),
                NetProfitOrLoss = new SignedCodalMoney(50_074_345),
                Sale = new CodalMoney(16_024_842),
                SaleMonth = new StatementMonth(10),
                SaleBeforeThisMonth = new CodalMoney(143_128_914),
                SaleLastYearSamePeriod = new CodalMoney(153_164_667),
                Assets = new CodalMoney(178_921_105),
                OwnersEquity = new CodalMoney(67_709_573),
                Receivables = new CodalMoney(65_615_475),
                LastYearNetProfit = new SignedCodalMoney(50_078_518),
                ExpectedSpringOperationalIncome = new SignedCodalMoney(0),
                ExpectedSummerOperationalIncome = new SignedCodalMoney(0),
                ExpectedFallOperationalIncome = new SignedCodalMoney(143_128_914),
                ExpectedWinterOperationalIncome = new SignedCodalMoney(0),
                ExpectedSaleAverageExcludeThisPeriod = new CodalMoney(15_903_213),
                ExpectedSaleAverageLastYearSamePeriod = new CodalMoney(15_316_467),
                ExpectedThisPeriodSaleRatio = 100.76m,
                ExpectedThisPeriodSaleRatioWithLastYear = 104.62m,
                ExpectedGrossMargin = 0.29m,
                ExpectedOperationalMargin = 0.27m,
                ExpectedNetMargin = 0.3499m,
                ExpectedForecastSale = new CodalMoney(190_984_507),
                ExpectedForecastOperationalProfit = new CodalMoney(51565817),
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
                SaleFiscalYear = 1404,
                SaleTraceNo = 112233
            }
        };
    }

    private static FinancialStatement CreateFinancialStatement(FinancialStatementTestCase testCase)
    {
        Guid id = Guid.NewGuid();
        Symbol symbol = Symbol.Empty;
        ulong traceNo = 123_456;
        IsoCurrency currency = IsoCurrency.IRR;
        DateTime createdAt = DateTime.Now;
        DateOnly lastClosePriceDate = DateOnly.FromDateTime(DateTime.Now);
        FiscalYear fiscalYear = testCase.FiscalYear;
        StatementMonth yearEndMonth = testCase.YearEndMonth;
        StatementMonth reportMonth = testCase.ReportMonth;

        return new FinancialStatementBuilder()
            .SetId(id)
            .SetSymbol(symbol)
            .SetCurrency(currency)
            .SetTraceNo(traceNo)
            .SetFiscalYear(fiscalYear)
            .SetYearEndMonth(yearEndMonth)
            .SetCreatedAt(createdAt)
            .SetLastClosePrice(testCase.LastClosePrice, lastClosePriceDate)
            .SetMarketCap(testCase.MarketCap)
            .SetIncomeStatement(
                reportMonth,
                testCase.OperationalIncome,
                SignedCodalMoney.Empty,
                testCase.GrossProfitOrLoss,
                testCase.OperationalProfitOrLoss,
                testCase.NoneOperationalProfit,
                testCase.Costs.Value,
                testCase.NetProfitOrLoss)
            .SetSale(
                testCase.Sale,
                testCase.SaleMonth,
                testCase.SaleTraceNo,
                testCase.SaleFiscalYear,
                testCase.SaleBeforeThisMonth,
                testCase.SaleLastYearSamePeriod)
            .SetFinancialPosition(
                testCase.Assets,
                testCase.OwnersEquity,
                testCase.Receivables,
                testCase.LastYearNetProfit)
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
        public ulong SaleTraceNo { get; set; }

        public FiscalYear SaleFiscalYear { get; set; }

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