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
        ReportMonth = GetFixedBalanceSheetReportMonth(yearEndMonth, reportMonth);
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

    /// <summary>
    /// Returns a corrected ReportMonth for a balance sheet when CODAL reports the month
    /// incorrectly shifted to the month following the fiscal year-end month.
    /// This method is static and returns the corrected ReportMonth without mutating state.
    /// </summary>
    /// <param name="yearEndMonth">Fiscal year-end month (1\-12).</param>
    /// <param name="reportMonth">Reported statement month (1\-12).</param>
    /// <remarks>See CODAL reference for the reported issue: https://codal.ir/Reports/Decision.aspx?LetterSerial=TFcAsVKrYbEQ2EPMwz9qSg%3d%3d&amp;rt=0&amp;let=6&amp;ct=0&amp;ft=-1&amp;sheetId=0 .</remarks>
    public static StatementMonth GetFixedBalanceSheetReportMonth(StatementMonth yearEndMonth, StatementMonth reportMonth)
    {
        if (reportMonth == (yearEndMonth == 12 ? 1 : yearEndMonth + 1))
        {
            return yearEndMonth;
        }

        return reportMonth;
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
}