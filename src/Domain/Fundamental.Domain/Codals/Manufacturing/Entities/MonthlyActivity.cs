using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class MonthlyActivity : BaseEntity<Guid>
{
    public MonthlyActivity(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        CodalMoney saleBeforeCurrentMonth,
        CodalMoney saleCurrentMonth,
        CodalMoney saleIncludeCurrentMonth,
        CodalMoney saleLastYear,
        bool hasSubCompanySale,
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
        SaleBeforeCurrentMonth = saleBeforeCurrentMonth;
        SaleCurrentMonth = saleCurrentMonth;
        SaleIncludeCurrentMonth = saleIncludeCurrentMonth;
        SaleLastYear = saleLastYear;
        HasSubCompanySale = hasSubCompanySale;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected MonthlyActivity()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    public string Uri { get; private set; }

    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    public StatementMonth YearEndMonth { get; private set; }

    public StatementMonth ReportMonth { get; private set; }

    public CodalMoney SaleBeforeCurrentMonth { get; private set; }

    public CodalMoney SaleCurrentMonth { get; private set; }

    public CodalMoney SaleIncludeCurrentMonth { get; private set; }

    public CodalMoney SaleLastYear { get; private set; }

    public bool HasSubCompanySale { get; private set; }

    public void Update(
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        CodalMoney saleBeforeCurrentMonth,
        CodalMoney saleCurrentMonth,
        CodalMoney saleIncludeCurrentMonth,
        CodalMoney saleLastYear,
        bool hasSubCompanySale,
        DateTime updatedAt
    )
    {
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        SaleBeforeCurrentMonth = saleBeforeCurrentMonth;
        SaleCurrentMonth = saleCurrentMonth;
        SaleIncludeCurrentMonth = saleIncludeCurrentMonth;
        SaleLastYear = saleLastYear;
        HasSubCompanySale = hasSubCompanySale;
        UpdatedAt = updatedAt;
    }
}