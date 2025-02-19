using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;

public interface IFinancialStatementBuilder
{
    ISetSymbol SetId(Guid id);
}

public interface ISetSymbol
{
    ISetCurrency SetSymbol(Symbol symbol);
}

public interface ISetCurrency
{
    ISetTraceNo SetCurrency(IsoCurrency currency);
}

public interface ISetTraceNo
{
    ISetFiscalYear SetTraceNo(ulong traceNo);
}

public interface ISetFiscalYear
{
    ISetYearEndMonth SetFiscalYear(FiscalYear fiscalYear);
}

public interface ISetYearEndMonth
{
    ISetCreatedAt SetYearEndMonth(StatementMonth yearEndMonth);
}

public interface ISetCreatedAt
{
    ISetLastClosePrice SetCreatedAt(DateTime createdAt);
}

public interface ISetLastClosePrice
{
    ISetMarketCap SetLastClosePrice(decimal lastClosePrice, DateOnly lastClosePriceDate);
    FinancialStatement Build();
}

public interface ISetMarketCap
{
    ISetIncomeStatement SetMarketCap(decimal marketCap);
    FinancialStatement Build();
}

public interface ISetIncomeStatement
{
    ISetSale SetIncomeStatement(
        StatementMonth reportMonth,
        SignedCodalMoney operationalIncome,
        SignedCodalMoney grossProfitOrLoss,
        SignedCodalMoney operationalProfitOrLoss,
        SignedCodalMoney noneOperationalProfit,
        CodalMoney costs,
        SignedCodalMoney netProfitOrLoss
    );

    FinancialStatement Build();
}

public interface ISetSale
{
    ISetFinancialPosition SetSale(
        CodalMoney sale,
        StatementMonth saleMonth,
        List<CodalMoney> saleBeforeThisMonth,
        List<CodalMoney> saleLastYearSamePeriod
    );

    FinancialStatement Build();
}

public interface ISetFinancialPosition
{
    IBuild SetFinancialPosition(
        CodalMoney assets,
        CodalMoney ownersEquity,
        CodalMoney receivables,
        SignedCodalMoney lastYearNetProfit
    );

    FinancialStatement Build();
}

public interface IBuild
{
    FinancialStatement Build();
}