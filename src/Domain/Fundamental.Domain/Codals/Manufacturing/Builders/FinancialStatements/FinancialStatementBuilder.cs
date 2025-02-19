using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;

public class FinancialStatementBuilder :
    IFinancialStatementBuilder, ISetSymbol, ISetCurrency, ISetCreatedAt, ISetLastClosePrice, ISetTraceNo, ISetFiscalYear, ISetYearEndMonth,
    ISetMarketCap, ISetIncomeStatement, ISetSale, ISetFinancialPosition, IBuild
{
    private Guid _id;
    private Symbol _symbol;
    private IsoCurrency _currency;
    private DateTime _createdAt;
    private ulong _traceNo;
    private FiscalYear _fiscalYear;
    private StatementMonth _yearEndMonth;
    private decimal _lastClosePrice;
    private DateOnly _lastClosePriceDate;
    private decimal _marketCap;
    private StatementMonth _reportMonth;
    private SignedCodalMoney _operationalIncome;
    private SignedCodalMoney _grossProfitOrLoss;
    private SignedCodalMoney _operationalProfitOrLoss;
    private SignedCodalMoney _noneOperationalProfit;
    private CodalMoney _costs;
    private SignedCodalMoney _netProfitOrLoss;
    private CodalMoney _sale;
    private StatementMonth _saleMonth;
    private List<CodalMoney> _saleBeforeThisMonth;
    private List<CodalMoney> _saleLastYearSamePeriod;
    private CodalMoney _assets;
    private CodalMoney _ownersEquity;
    private CodalMoney _receivables;
    private SignedCodalMoney _lastYearNetProfit;

    public ISetSymbol SetId(Guid id)
    {
        _id = id;
        return this;
    }

    public ISetCurrency SetSymbol(Symbol symbol)
    {
        _symbol = symbol;
        return this;
    }

    public ISetTraceNo SetCurrency(IsoCurrency currency)
    {
        _currency = currency;
        return this;
    }

    public ISetFiscalYear SetTraceNo(ulong traceNo)
    {
        _traceNo = traceNo;
        return this;
    }

    public ISetYearEndMonth SetFiscalYear(FiscalYear fiscalYear)
    {
        _fiscalYear = fiscalYear;
        return this;
    }

    public ISetCreatedAt SetYearEndMonth(StatementMonth yearEndMonth)
    {
        _yearEndMonth = yearEndMonth;
        return this;
    }

    public ISetLastClosePrice SetCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public ISetMarketCap SetLastClosePrice(decimal lastClosePrice, DateOnly lastClosePriceDate)
    {
        _lastClosePrice = lastClosePrice;
        _lastClosePriceDate = lastClosePriceDate;
        return this;
    }

    public ISetIncomeStatement SetMarketCap(decimal marketCap)
    {
        _marketCap = marketCap;
        return this;
    }

    public ISetSale SetIncomeStatement(
        StatementMonth reportMonth,
        SignedCodalMoney operationalIncome,
        SignedCodalMoney grossProfitOrLoss,
        SignedCodalMoney operationalProfitOrLoss,
        SignedCodalMoney noneOperationalProfit,
        CodalMoney costs,
        SignedCodalMoney netProfitOrLoss
    )
    {
        _reportMonth = reportMonth;
        _operationalIncome = operationalIncome;
        _grossProfitOrLoss = grossProfitOrLoss;
        _operationalProfitOrLoss = operationalProfitOrLoss;
        _noneOperationalProfit = noneOperationalProfit;
        _costs = costs;
        _netProfitOrLoss = netProfitOrLoss;
        return this;
    }

    public ISetFinancialPosition SetSale(
        CodalMoney sale,
        StatementMonth saleMonth,
        List<CodalMoney> saleBeforeThisMonth,
        List<CodalMoney> saleLastYearSamePeriod
    )
    {
        _sale = sale;
        _saleMonth = saleMonth;
        _saleBeforeThisMonth = saleBeforeThisMonth;
        _saleLastYearSamePeriod = saleLastYearSamePeriod;
        return this;
    }

    public IBuild SetFinancialPosition(
        CodalMoney assets,
        CodalMoney ownersEquity,
        CodalMoney receivables,
        SignedCodalMoney lastYearNetProfit
    )
    {
        _assets = assets;
        _ownersEquity = ownersEquity;
        _receivables = receivables;
        _lastYearNetProfit = lastYearNetProfit;
        return this;
    }

    public FinancialStatement Build()
    {
        FinancialStatement financialStatement =
            new FinancialStatement(_id, _symbol, _currency, _traceNo, _fiscalYear, _yearEndMonth, _createdAt)
                .SetLastClosePrice(_lastClosePrice, _lastClosePriceDate)
                .SetMarketCap(_marketCap)
                .SetIncomeStatement(
                    _reportMonth,
                    _operationalIncome,
                    _grossProfitOrLoss,
                    _operationalProfitOrLoss,
                    _noneOperationalProfit,
                    _costs,
                    _netProfitOrLoss)
                .SetFinancialPosition(_assets, _ownersEquity, _receivables, _lastYearNetProfit);

        financialStatement.SetSale(_sale, _saleMonth, _saleBeforeThisMonth, _saleLastYearSamePeriod);

        return financialStatement;
    }
}