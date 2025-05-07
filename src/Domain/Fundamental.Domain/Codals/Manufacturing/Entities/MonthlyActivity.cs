using Fundamental.BuildingBlock;
using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class MonthlyActivity : BaseEntity<Guid>
{
    private bool ExtraSalesInfosApplied { get; set; } = false;

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
        List<SymbolExtensions.SalesInfo> extraSalesInfos,
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
        ExtraSalesInfos = extraSalesInfos;
        CreatedAt = createdAt.ToUniversalTime();
        UpdatedAt = createdAt.ToUniversalTime();

        AddDomainEvent(
            new MonthlyActivityUpdated(
                Symbol.Isin,
                ReportMonth,
                FiscalYear,
                SaleCurrentMonth.Value,
                SaleBeforeCurrentMonth.Value,
                SaleLastYear.Value,
                traceNo
            ),
            EventsAddress.MonthlyActivity.MONTHLY_ACTIVITY_UPDATE
        );
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

    public void ApplyExtraSale()
    {
        if (ExtraSalesInfosApplied)
        {
            return;
        }

        if (ExtraSalesInfos.Count == 0)
        {
            return;
        }

        SaleCurrentMonth += ExtraSalesInfos.Sum(x => x.MonthlySales);
        SaleIncludeCurrentMonth += ExtraSalesInfos.Sum(x => x.CumulativeSales);
        SaleBeforeCurrentMonth = SaleBeforeCurrentMonth + ExtraSalesInfos.Sum(x => x.CumulativeSales)
                                 - ExtraSalesInfos.Sum(x => x.MonthlySales);
        ExtraSalesInfosApplied = true;
    }

    public List<SymbolExtensions.SalesInfo> ExtraSalesInfos { get; private set; } = new();

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
        List<SymbolExtensions.SalesInfo> extraSalesInfos,
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
        ExtraSalesInfos = extraSalesInfos;
        UpdatedAt = updatedAt.ToUniversalTime();

        AddDomainEvent(
            new MonthlyActivityUpdated(
                Symbol.Isin,
                ReportMonth,
                FiscalYear,
                SaleCurrentMonth.Value,
                SaleBeforeCurrentMonth.Value,
                SaleLastYear.Value,
                traceNo
            ),
            EventsAddress.MonthlyActivity.MONTHLY_ACTIVITY_UPDATE
        );
    }
}