using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Codals.Manufacturing.Events;

public record MonthlyActivityUpdated(
    string Isin,
    ushort ReportMonth,
    ushort FiscalYear,
    decimal SaleCurrentMonth,
    decimal SaleBeforeCurrentMonth,
    decimal SaleLastYear,
    ulong TraceNo
) : IEvent;