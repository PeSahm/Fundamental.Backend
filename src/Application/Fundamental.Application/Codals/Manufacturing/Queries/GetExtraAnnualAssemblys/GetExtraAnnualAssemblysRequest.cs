using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;

[HandlerCode(HandlerCode.GetExtraAnnualAssemblys)]
public sealed record GetExtraAnnualAssemblysRequest(
    string? Isin,
    int? FiscalYear,
    int? YearEndMonth
) : PagingRequest, IRequest<Response<Paginated<GetExtraAnnualAssemblyListItem>>>;
