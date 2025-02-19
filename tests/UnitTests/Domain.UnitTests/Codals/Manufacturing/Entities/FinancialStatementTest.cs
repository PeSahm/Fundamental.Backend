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
    [InlineData(6, 9, 9)]
    [InlineData(1, 12, 1)]
    [InlineData(12, 1, 11)]
    [InlineData(3, 6, 9)]
    [InlineData(11, 2, 9)]
    public void AdjustedMonth_ShouldReturnCorrectMonth(int reportMonth, int yearEndMonth, int expectedAdjustedMonth)
    {
        // Arrange
        StatementMonth month = new StatementMonth(reportMonth);

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
    public void Build_ShouldCalculateSale(FinancialStatementTestCase testCase)
    {
        // Arrange
        FinancialStatement financialStatement = CreateFinancialStatement(testCase);

        // Act & Assert
        Assert.Equal(testCase.ExpectedSaleAverageExcludeThisPeriod, financialStatement.SaleAverageExcludeThisPeriod);
    }

    // Strongly-typed test data provider
    public static IEnumerable<object[]> MarketValueTestCases()
    {
        yield return new object[]
        {
            new FinancialStatementTestCase
            {
                LastClosePrice = 18_740m,
                MarketCap = 12_000_000_000m,
                ExpectedMarketValue = 224_880_000_000_000,
                OperationalIncome = new SignedCodalMoney(89_882_304),
                GrossProfitOrLoss = new SignedCodalMoney(38_473_162),
                OperationalProfitOrLoss = new SignedCodalMoney(36_793_662),
                NoneOperationalProfit = new SignedCodalMoney(1_185_834),
                Costs = new CodalMoney(472_602),
                NetProfitOrLoss = new SignedCodalMoney(37_506_894),
                Sale = new CodalMoney(8_353_110),
                SaleMonth = new StatementMonth(7),
                SaleBeforeThisMonth = new List<CodalMoney> { new CodalMoney(89_882_304) },
                SaleLastYearSamePeriod = new List<CodalMoney> { new CodalMoney(73_517_290) },
                Assets = new CodalMoney(174_236_766),
                OwnersEquity = new CodalMoney(64_398_016),
                Receivables = new CodalMoney(70_615_376),
                LastYearNetProfit = new SignedCodalMoney(18_437_222),
                ExpectedSpringOperationalIncome = new SignedCodalMoney(0),
                ExpectedSummerOperationalIncome = new SignedCodalMoney(89_882_304),
                ExpectedFallOperationalIncome = new SignedCodalMoney(0),
                ExpectedWinterOperationalIncome = new SignedCodalMoney(0),
                ExpectedSaleAverageExcludeThisPeriod = new CodalMoney(14980384)
            }
        };
    }

    public class FinancialStatementTestCase
    {
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
        public List<CodalMoney> SaleBeforeThisMonth { get; set; }
        public List<CodalMoney> SaleLastYearSamePeriod { get; set; }
        public CodalMoney Assets { get; set; }
        public CodalMoney OwnersEquity { get; set; }
        public CodalMoney Receivables { get; set; }
        public SignedCodalMoney LastYearNetProfit { get; set; }
        public SignedCodalMoney ExpectedSpringOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedSummerOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedFallOperationalIncome { get; set; }
        public SignedCodalMoney ExpectedWinterOperationalIncome { get; set; }
        public CodalMoney ExpectedSaleAverageExcludeThisPeriod { get; set; }
    }

    private FinancialStatement CreateFinancialStatement(FinancialStatementTestCase testCase)
    {
        Guid id = Guid.NewGuid();
        Symbol symbol = Symbol.Empty;
        ulong traceNo = 123_456;
        IsoCurrency currency = IsoCurrency.IRR;
        DateTime createdAt = DateTime.Now;
        DateOnly lastClosePriceDate = DateOnly.FromDateTime(DateTime.Now);
        FiscalYear fiscalYear = new FiscalYear(1_403);
        StatementMonth yearEndMonth = new StatementMonth(12);
        StatementMonth reportMonth = new StatementMonth(6);

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
                testCase.GrossProfitOrLoss,
                testCase.OperationalProfitOrLoss,
                testCase.NoneOperationalProfit,
                testCase.Costs,
                testCase.NetProfitOrLoss)
            .SetSale(testCase.Sale, testCase.SaleMonth, testCase.SaleBeforeThisMonth, testCase.SaleLastYearSamePeriod)
            .SetFinancialPosition(testCase.Assets, testCase.OwnersEquity, testCase.Receivables, testCase.LastYearNetProfit)
            .Build();
    }
}