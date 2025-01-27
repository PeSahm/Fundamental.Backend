using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;

[HandlerCode(HandlerCode.GetGetStatusOfViableCompanies)]
public record GetStatusOfViableCompaniesRequest(string? Isin, ReviewStatus? ReviewStatus) :
    PagingRequest,
    IRequest<Response<Paginated<GetStatusOfViableCompaniesResultDto>>>;