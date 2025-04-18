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
    private CodalMoney _assets = CodalMoney.Empty;
    private CodalMoney _costs = CodalMoney.Empty;
    private DateTime _createdAt;
    private IsoCurrency _currency;
    private FiscalYear _fiscalYear;
    private SignedCodalMoney _grossProfitOrLoss = SignedCodalMoney.Empty;
    private Guid _id;
    private decimal _lastClosePrice;
    private DateOnly _lastClosePriceDate = DateOnly.FromDateTime(DateTime.Now);
    private SignedCodalMoney _lastYearNetProfit = SignedCodalMoney.Empty;
    private decimal _marketCap;
    private SignedCodalMoney _netProfitOrLoss = SignedCodalMoney.Empty;
    private SignedCodalMoney _noneOperationalProfit = SignedCodalMoney.Empty;
    private SignedCodalMoney _operationalIncome = SignedCodalMoney.Empty;
    private SignedCodalMoney _operationalProfitOrLoss = SignedCodalMoney.Empty;
    private SignedCodalMoney _otherOperationalIncome = SignedCodalMoney.Empty;
    private CodalMoney _ownersEquity = CodalMoney.Empty;
    private CodalMoney _receivables = CodalMoney.Empty;
    private StatementMonth _reportMonth = StatementMonth.Empty;
    private CodalMoney _sale = CodalMoney.Empty;
    private CodalMoney _saleBeforeThisMonth = CodalMoney.Empty;
    private CodalMoney _saleLastYearSamePeriod = CodalMoney.Empty;
    private StatementMonth _saleMonth = StatementMonth.Empty;
    private Symbol _symbol;
    private ulong _traceNo;
    private StatementMonth _yearEndMonth = StatementMonth.Empty;

    public ISetSymbol SetId(Guid id)
    {
        _id = id;
        return this;
    }

    public ISetLastClosePrice SetCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public ISetTraceNo SetCurrency(IsoCurrency currency)
    {
        _currency = currency;
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

    public ISetYearEndMonth SetFiscalYear(FiscalYear fiscalYear)
    {
        _fiscalYear = fiscalYear;
        return this;
    }

    public ISetSale SetIncomeStatement(
        StatementMonth reportMonth,
        SignedCodalMoney operationalIncome,
        SignedCodalMoney otherOperationalIncome,
        SignedCodalMoney grossProfitOrLoss,
        SignedCodalMoney operationalProfitOrLoss,
        SignedCodalMoney noneOperationalProfit,
        SignedCodalMoney costs,
        SignedCodalMoney netProfitOrLoss
    )
    {
        _reportMonth = reportMonth;
        _operationalIncome = operationalIncome;
        _otherOperationalIncome = otherOperationalIncome;
        _grossProfitOrLoss = grossProfitOrLoss;
        _operationalProfitOrLoss = operationalProfitOrLoss;
        _noneOperationalProfit = noneOperationalProfit;
        _costs = costs.Value > 0 ? new CodalMoney(costs.Value) : CodalMoney.Empty;
        _netProfitOrLoss = netProfitOrLoss;
        return this;
    }

    public ISetMarketCap SetLastClosePrice(decimal lastClosePrice, DateOnly lastClosePriceDate)
    {
        _lastClosePrice = lastClosePrice;
        _lastClosePriceDate = lastClosePriceDate;
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
                    _otherOperationalIncome,
                    _grossProfitOrLoss,
                    _operationalProfitOrLoss,
                    _noneOperationalProfit,
                    _costs,
                    _netProfitOrLoss)
                .SetFinancialPosition(_assets, _ownersEquity, _receivables, _lastYearNetProfit);

        financialStatement.SetSale(_sale, _saleMonth, _saleBeforeThisMonth, _saleLastYearSamePeriod);

        return financialStatement;
    }

    public ISetIncomeStatement SetMarketCap(decimal marketCap)
    {
        _marketCap = marketCap;
        return this;
    }

    public ISetFinancialPosition SetSale(
        CodalMoney sale,
        StatementMonth saleMonth,
        CodalMoney saleBeforeThisMonth,
        CodalMoney saleLastYearSamePeriod
    )
    {
        _sale = sale;
        _saleMonth = saleMonth;
        _saleBeforeThisMonth = saleBeforeThisMonth;
        _saleLastYearSamePeriod = saleLastYearSamePeriod;
        return this;
    }

    public ISetCurrency SetSymbol(Symbol symbol)
    {
        _symbol = symbol;
        return this;
    }

    public ISetFiscalYear SetTraceNo(ulong traceNo)
    {
        _traceNo = traceNo;
        return this;
    }

    public ISetCreatedAt SetYearEndMonth(StatementMonth yearEndMonth)
    {
        _yearEndMonth = yearEndMonth;
        return this;
    }
}