using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Statements.Entities;

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
        Money operatingIncome,
        SignedMoney grossProfit,
        SignedMoney operatingProfit,
        Money bankInterestIncome,
        SignedMoney investmentIncome,
        Money netProfit,
        Money expense,
        Money asset,
        Money ownersEquity,
        Money receivables,
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

    public Money OperatingIncome { get; private set; }

    public SignedMoney GrossProfit { get; private set; }

    public SignedMoney OperatingProfit { get; private set; }

    public Money BankInterestIncome { get; private set; }

    public SignedMoney InvestmentIncome { get; private set; }

    public Money NetProfit { get; set; }

    public Money Expense { get; private set; }

    public Money Asset { get; private set; }

    public Money OwnersEquity { get; private set; }

    public Money Receivables { get; private set; }

    public void Update(
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        CodalMoney operatingIncome,
        CodalMoney grossProfit,
        CodalMoney operatingProfit,
        CodalMoney bankInterestIncome,
        CodalMoney investmentIncome,
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