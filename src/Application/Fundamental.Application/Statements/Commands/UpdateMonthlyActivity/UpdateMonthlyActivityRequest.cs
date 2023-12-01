using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Commands.UpdateMonthlyActivity;

[HandlerCode(HandlerCode.UpdateMonthlyActivity)]
public record UpdateMonthlyActivityRequest(
    Guid Id,
    string Isin,
    ulong TraceNo,
    string Uri,
    ushort FiscalYear,
    ushort YearEndMonth,
    ushort ReportMonth,
    decimal SaleBeforeCurrentMonth,
    decimal SaleCurrentMonth,
    decimal SaleIncludeCurrentMonth,
    decimal SaleLastYear,
    bool HasSubCompanySale
) : IRequest<Response>;