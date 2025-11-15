using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;

[HandlerCode(HandlerCode.GetBalanceSheetDetails)]
public sealed record GetBalanceSheetDetailsRequest(ulong TraceNo, ushort FiscalYear, ushort ReportMonth)
    : IRequest<Response<List<GetBalanceSheetDetailResultDto>>>;