using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;

public sealed class
    GetNonOperationIncomesQueryHandler(INonOperationIncomesRepository nonOperationIncomesRepository)
    : IRequestHandler<GetNonOperationIncomesRequest, Response<Paginated<GetNonOperationIncomesResultItem>>>
{
    public async Task<Response<Paginated<GetNonOperationIncomesResultItem>>> Handle(
        GetNonOperationIncomesRequest request,
        CancellationToken cancellationToken
    )
    {
        return await nonOperationIncomesRepository.GetNonOperationIncomesAsync(request, cancellationToken);
    }
}