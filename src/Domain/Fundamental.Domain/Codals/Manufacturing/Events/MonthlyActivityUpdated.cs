using Fundamental.BuildingBlock.Events;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Domain.Codals.Manufacturing.Events;

public record MonthlyActivityUpdated(
    string Isin,
    StatementMonth ReportMonth,
    FiscalYear FiscalYear,
    CodalMoney SaleCurrentMonth,
    CodalMoney SaleBeforeCurrentMonth,
    CodalMoney SaleLastYear
) : IEvent;