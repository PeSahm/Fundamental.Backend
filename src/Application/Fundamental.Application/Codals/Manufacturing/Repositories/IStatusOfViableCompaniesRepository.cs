using Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IStatusOfViableCompaniesRepository
{
    Task<Response<Paginated<GetStatusOfViableCompaniesResultDto>>> GetStatusOfViableCompanies(
        GetStatusOfViableCompaniesRequest request,
        CancellationToken cancellationToken
    );
}