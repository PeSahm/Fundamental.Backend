namespace Fundamental.Application.Statements.Queries.GetMonthlyActivities;

public record GetMonthlyActivitiesResultItem(
    Guid Id,
    string Isin,
    string Symbol,
    string Title,
    string Uri,
    ushort FiscalYear,
    ushort YearEndMonth,
    ushort ReportMonth,
    decimal SaleBeforeCurrentMonth,
    decimal SaleCurrentMonth,
    decimal SaleIncludeCurrentMonth,
    decimal SaleLastYear,
    bool HasSubCompanySale,
    ulong TraceNo
);