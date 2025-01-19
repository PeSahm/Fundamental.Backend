using Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface INonOperationIncomesRepository
{
    Task<Response<Paginated<GetNonOperationIncomesResultItem>>> GetNonOperationIncomesAsync(
        GetNonOperationIncomesRequest request,
        CancellationToken cancellationToken
    );
}