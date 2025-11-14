using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;

[HandlerCode(HandlerCode.AddIncomeStatement)]
public sealed record AddIncomeStatementRequest(
    string Isin,
    ulong TraceNo,
    string Uri,
    ushort FiscalYear,
    ushort YearEndMonth,
    ushort ReportMonth,
    bool IsAudited,
    DateTime PublishDate,
    List<AddIncomeStatementItem> Items
) : IRequest<Response>;