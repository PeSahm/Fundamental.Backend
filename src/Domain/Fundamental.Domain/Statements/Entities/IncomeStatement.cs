using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Statements.Entities;

public class IncomeStatement : BaseEntity<Guid>
{
    public IncomeStatement(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        int row,
        int codalRow,
        int codalCategory,
        string? description,
        CodalMoney value,
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
        Row = row;
        CodalCategory = codalCategory;
        CodalRow = codalRow;
        Description = description;
        Value = value;
        IsAudited = isAudited;
        CreatedAt = createdAt;
    }

    protected IncomeStatement()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    public string Uri { get; private set; }

    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    public StatementMonth YearEndMonth { get; private set; }

    public StatementMonth ReportMonth { get; private set; }

    public int Row { get; private set; }

    public int CodalRow { get; private set; }

    public int CodalCategory { get; set; }
    public string? Description { get; private set; }

    public SignedMoney Value { get; private set; }

    public bool IsAudited { get; private set; }

    public void Update(
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        int row,
        string description,
        CodalMoney value,
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