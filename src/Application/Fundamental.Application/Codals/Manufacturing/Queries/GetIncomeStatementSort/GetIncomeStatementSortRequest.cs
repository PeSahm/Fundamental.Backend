using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;

[HandlerCode(HandlerCode.GetIncomeStatementSortList)]
public sealed record GetIncomeStatementSortRequest : IRequest<Response<List<GetIncomeStatementSortResultDto>>>;