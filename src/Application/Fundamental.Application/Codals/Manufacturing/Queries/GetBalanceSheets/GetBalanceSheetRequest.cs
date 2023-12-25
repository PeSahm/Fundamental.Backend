using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;

[HandlerCode(HandlerCode.GetBalanceSheet)]
public sealed record GetBalanceSheetRequest(List<string> IsinList, ulong? TraceNo, ushort? FiscalYear, ushort? ReportMonth) :
    PagingRequest, IRequest<Response<Paginated<GetBalanceSheetResultDto>>>;