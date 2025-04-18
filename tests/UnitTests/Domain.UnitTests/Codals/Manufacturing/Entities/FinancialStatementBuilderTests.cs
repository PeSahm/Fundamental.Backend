using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Domain.UnitTests.Codals.Manufacturing.Entities;

public class FinancialStatementBuilderTests
{
    [Fact]
    public void Build_ShouldCreateFinancialStatementWithCorrectValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol symbol = Symbol.Empty;
        ulong traceNo = 123456;
        IsoCurrency currency = IsoCurrency.IRR;
        DateTime createdAt = DateTime.Now;
        decimal lastClosePrice = 1000m;
        DateOnly lastClosePriceDate = DateOnly.FromDateTime(DateTime.Now);
        decimal marketCap = 500000m;
        FiscalYear fiscalYear = new(2023);
        StatementMonth yearEndMonth = new(12);
        StatementMonth reportMonth = new(6);
        SignedCodalMoney operationalIncome = new(1000000m);
        SignedCodalMoney otherOperationalIncome = new(0m);
        SignedCodalMoney grossProfitOrLoss = new(500000m);
        SignedCodalMoney operationalProfitOrLoss = new(300000m);
        SignedCodalMoney noneOperationalProfit = new(200000m);
        SignedCodalMoney costs = new(100000m);
        SignedCodalMoney netProfitOrLoss = new(400000m);
        CodalMoney sale = new(6000000m);
        StatementMonth saleMonth = new(6);
        CodalMoney saleBeforeThisMonth = new(5000000m);
        CodalMoney saleLastYearSamePeriod = new(4000000m);
        CodalMoney expectedAvgSaleBeforeThisMonth = new CodalMoney(5000000m) / (saleMonth - 1);
        CodalMoney expectedAvggSaleLastYearSamePeriod = Math.Ceiling(new CodalMoney(4000000m) / saleMonth);
        CodalMoney assets = new(1000000m);
        CodalMoney ownersEquity = new(500000m);
        CodalMoney receivables = new(200000m);
        SignedCodalMoney lastYearNetProfit = new(300000m);

        // Act
        FinancialStatement financialStatement = new FinancialStatementBuilder()
            .SetId(id)
            .SetSymbol(symbol)
            .SetCurrency(currency)
            .SetTraceNo(traceNo)
            .SetFiscalYear(fiscalYear)
            .SetYearEndMonth(yearEndMonth)
            .SetCreatedAt(createdAt)
            .SetLastClosePrice(lastClosePrice, lastClosePriceDate)
            .SetMarketCap(marketCap)
            .SetIncomeStatement(
                reportMonth,
                operationalIncome,
                otherOperationalIncome,
                grossProfitOrLoss,
                operationalProfitOrLoss,
                noneOperationalProfit,
                costs,
                netProfitOrLoss)
            .SetSale(sale, saleMonth, saleBeforeThisMonth, saleLastYearSamePeriod)
            .SetFinancialPosition(assets, ownersEquity, receivables, lastYearNetProfit)
            .Build();

        // Assert
        Assert.Equal(id, financialStatement.Id);
        Assert.Equal(symbol, financialStatement.Symbol);
        Assert.Equal(currency, financialStatement.Currency);
        Assert.Equal(createdAt, financialStatement.CreatedAt);
        Assert.Equal(lastClosePrice, financialStatement.LastClosePrice);
        Assert.Equal(marketCap, financialStatement.MarketCap);
        Assert.Equal(fiscalYear, financialStatement.FiscalYear);
        Assert.Equal(yearEndMonth, financialStatement.YearEndMonth);
        Assert.Equal(reportMonth, financialStatement.ReportMonth);
        Assert.Equal(operationalIncome, financialStatement.OperationalIncome);
        Assert.Equal(grossProfitOrLoss, financialStatement.GrossProfitOrLoss);
        Assert.Equal(operationalProfitOrLoss, financialStatement.OperationalProfitOrLoss);
        Assert.Equal(noneOperationalProfit, financialStatement.NoneOperationalProfit);
        Assert.Equal(costs.Value, financialStatement.Costs.Value);
        Assert.Equal(netProfitOrLoss, financialStatement.NetProfitOrLoss);
        Assert.Equal(sale, financialStatement.Sale);
        Assert.Equal(saleMonth, financialStatement.SaleMonth);
        Assert.Equal(saleBeforeThisMonth.Value, financialStatement.SaleBeforeThisMonth.Value);
        Assert.Equal(saleLastYearSamePeriod.Value, financialStatement.SaleLastYearSamePeriod.Value);

        Assert.Equal(expectedAvgSaleBeforeThisMonth.Value, financialStatement.SaleAverageExcludeThisPeriod.Value);
        Assert.Equal(expectedAvggSaleLastYearSamePeriod.Value, financialStatement.SaleAverageLastYearSamePeriod.Value);

        Assert.Equal(assets, financialStatement.Assets);
        Assert.Equal(ownersEquity, financialStatement.OwnersEquity);
        Assert.Equal(receivables, financialStatement.Receivables);
        Assert.Equal(lastYearNetProfit, financialStatement.LastYearNetProfitOrLoss);
    }

    [Fact]
    public void Build_ShouldCreateFinancialStatementWithCorrectValues_InSimpleMode()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol symbol = Symbol.Empty;
        ulong traceNo = 123456;
        IsoCurrency currency = IsoCurrency.IRR;
        DateTime createdAt = DateTime.Now;
        decimal marketCap = 500000m;
        FiscalYear fiscalYear = new(2023);
        StatementMonth yearEndMonth = new(12);

        // Act
        FinancialStatement financialStatement = new FinancialStatementBuilder()
            .SetId(id)
            .SetSymbol(symbol)
            .SetCurrency(currency)
            .SetTraceNo(traceNo)
            .SetFiscalYear(fiscalYear)
            .SetYearEndMonth(yearEndMonth)
            .SetCreatedAt(createdAt)
            .SetMarketCap(marketCap)
            .Build();

        // Assert
        Assert.Equal(id, financialStatement.Id);
        Assert.Equal(symbol, financialStatement.Symbol);
        Assert.Equal(currency, financialStatement.Currency);
        Assert.Equal(createdAt, financialStatement.CreatedAt);
        Assert.Equal(marketCap, financialStatement.MarketCap);
        Assert.Equal(fiscalYear, financialStatement.FiscalYear);
        Assert.Equal(yearEndMonth, financialStatement.YearEndMonth);
    }
}