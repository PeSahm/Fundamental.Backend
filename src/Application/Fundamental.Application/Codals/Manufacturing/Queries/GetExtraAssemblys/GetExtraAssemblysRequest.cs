using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;

[HandlerCode(HandlerCode.GetExtraAssemblys)]
public sealed record GetExtraAssemblysRequest(
    string? Isin,
    int? FiscalYear,
    int? YearEndMonth
) : PagingRequest, IRequest<Response<Paginated<GetExtraAssemblyListItem>>>;
