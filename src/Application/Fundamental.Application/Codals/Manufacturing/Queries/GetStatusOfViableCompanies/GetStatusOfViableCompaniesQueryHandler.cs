using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;

public sealed class GetStatusOfViableCompaniesQueryHandler(IStatusOfViableCompaniesRepository statusOfViableCompaniesRepository) :
    IRequestHandler<GetStatusOfViableCompaniesRequest, Response<Paginated<GetStatusOfViableCompaniesResultDto>>>
{
    public async Task<Response<Paginated<GetStatusOfViableCompaniesResultDto>>> Handle(
        GetStatusOfViableCompaniesRequest request,
        CancellationToken cancellationToken
    )
    {
        Response<Paginated<GetStatusOfViableCompaniesResultDto>> response =
            await statusOfViableCompaniesRepository.GetStatusOfViableCompanies(request, cancellationToken);

        return response;
    }
}