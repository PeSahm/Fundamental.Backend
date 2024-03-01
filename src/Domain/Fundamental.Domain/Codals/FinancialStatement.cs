using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals;

public class FinancialStatement : BaseEntity<Guid>
{
    public FinancialStatement(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        CodalMoney operatingIncome,
        SignedCodalMoney grossProfit,
        SignedCodalMoney operatingProfit,
        CodalMoney bankInterestIncome,
        SignedCodalMoney investmentIncome,
        CodalMoney netProfit,
        CodalMoney expense,
        CodalMoney asset,
        CodalMoney ownersEquity,
        CodalMoney receivables,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        OperatingIncome = operatingIncome;
        GrossProfit = grossProfit;
        OperatingProfit = operatingProfit;
        BankInterestIncome = bankInterestIncome;
        InvestmentIncome = investmentIncome;
        NetProfit = netProfit;
        Expense = expense;
        Asset = asset;
        OwnersEquity = ownersEquity;
        Receivables = receivables;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected FinancialStatement()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    public string Uri { get; private set; }

    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    public StatementMonth YearEndMonth { get; private set; }

    public StatementMonth ReportMonth { get; private set; }

    public CodalMoney OperatingIncome { get; private set; }

    public SignedCodalMoney GrossProfit { get; private set; }

    public SignedCodalMoney OperatingProfit { get; private set; }

    public CodalMoney BankInterestIncome { get; private set; }

    public SignedCodalMoney InvestmentIncome { get; private set; }

    public CodalMoney NetProfit { get; set; }

    public CodalMoney Expense { get; private set; }

    public CodalMoney Asset { get; private set; }

    public CodalMoney OwnersEquity { get; private set; }

    public CodalMoney Receivables { get; private set; }

    public void Update(
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        CodalMoney operatingIncome,
        SignedCodalMoney grossProfit,
        SignedCodalMoney operatingProfit,
        CodalMoney bankInterestIncome,
        SignedCodalMoney investmentIncome,
        CodalMoney netProfit,
        CodalMoney expense,
        CodalMoney asset,
        CodalMoney ownersEquity,
        CodalMoney receivables,
        DateTime updatedAt
    )
    {
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        OperatingIncome = operatingIncome;
        GrossProfit = grossProfit;
        OperatingProfit = operatingProfit;
        BankInterestIncome = bankInterestIncome;
        InvestmentIncome = investmentIncome;
        NetProfit = netProfit;
        Expense = expense;
        Asset = asset;
        OwnersEquity = ownersEquity;
        Receivables = receivables;
        UpdatedAt = updatedAt;
    }
}