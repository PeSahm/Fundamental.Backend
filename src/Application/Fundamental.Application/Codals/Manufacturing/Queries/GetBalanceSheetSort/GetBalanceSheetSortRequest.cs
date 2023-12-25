using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;

[HandlerCode(HandlerCode.GetBalanceSheetSortList)]
public sealed record GetBalanceSheetSortRequest : IRequest<Response<List<GetBalanceSheetSortResultDto>>>;