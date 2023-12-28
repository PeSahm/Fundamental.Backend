using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementDetails;

[HandlerCode(HandlerCode.GetIncomeStatementDetails)]
public sealed record GetIncomeStatementDetailsRequest(ulong TraceNo, ushort FiscalYear, ushort ReportMonth)
    : IRequest<Response<List<GetIncomeStatementDetailsResultDto>>>;