using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddMonthlyActivity;

[HandlerCode(HandlerCode.AddMonthlyActivity)]
public record AddMonthlyActivityRequest(
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