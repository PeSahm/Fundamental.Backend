using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;

[HandlerCode(HandlerCode.GetAnnualAssemblys)]
public sealed record GetAnnualAssemblysRequest(
    string? Isin,
    int? FiscalYear,
    int? YearEndMonth
) : PagingRequest, IRequest<Response<Paginated<GetAnnualAssemblyListItem>>>;
