using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class BalanceSheet : BaseEntity<Guid>
{
    public BalanceSheet(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        ushort row,
        ushort codalRow,
        BalanceSheetCategory codalCategory,
        string? description,
        SignedCodalMoney value,
        bool isAudited,
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
        FixedBalanceSheetReportMonth(yearEndMonth, reportMonth);
        Row = row;
        CodalCategory = codalCategory;
        CodalRow = codalRow;
        Description = description;
        Value = value;
        IsAudited = isAudited;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected BalanceSheet()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    public string Uri { get; private set; }

    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    public StatementMonth YearEndMonth { get; private set; }

    public StatementMonth ReportMonth { get; private set; }

    public ushort Row { get; private set; }

    public ushort CodalRow { get; private set; }

    public BalanceSheetCategory CodalCategory { get; set; }

    public string? Description { get; private set; }

    public SignedCodalMoney Value { get; private set; }

    public bool IsAudited { get; private set; }

    public FinancialStatement? FinancialStatement { get; set; }

    public void Update(
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        ushort row,
        string description,
        SignedCodalMoney value,
        bool isAudited,
        DateTime updatedAt
    )
    {
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        Row = row;
        Description = description;
        Value = value;
        IsAudited = isAudited;
        UpdatedAt = updatedAt;
    }

    /// <summary>
    /// Adjusts ReportMonth and FiscalYear when CODAL reports the statement as the first
    /// day of the month following the fiscal year-end (instead of the correct last day).
    /// This method mutates the entity by assigning `ReportMonth` and `YearEndMonth`.
    /// </summary>
    /// <example>
    /// For fiscal year-end 12/30, CODAL may return 1403-01-01. This method will set
    /// `ReportMonth` to 12 and decrement `FiscalYear` so the intended period becomes 1402-12-30.
    /// </example>
    /// <param name="yearEndMonth">Fiscal year-end month as a <see cref="StatementMonth"/>.</param>
    /// <param name="reportMonth">Reported statement month as a <see cref="StatementMonth"/>.</param>
    private void FixedBalanceSheetReportMonth(StatementMonth yearEndMonth, StatementMonth reportMonth)
    {
        if (reportMonth == (yearEndMonth == 12 ? 1 : yearEndMonth + 1))
        {
            ReportMonth = new StatementMonth(yearEndMonth);
            FiscalYear = new FiscalYear(FiscalYear - 1);
        }
    }
}