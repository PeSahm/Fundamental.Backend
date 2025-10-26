using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;

[HandlerCode(HandlerCode.AddBalanceSheet)]
public sealed record AddBalanceSheetRequest(
    string Isin,
    ulong TraceNo,
    string Uri,
    ushort FiscalYear,
    ushort YearEndMonth,
    ushort ReportMonth,
    bool IsAudited,
    DateTime PublishDate,
    List<AddBalanceSheetItem> Items
) : IRequest<Response>;