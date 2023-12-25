using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;

public sealed class GetBalanceSheetSortQueryHandler(IRepository<BalanceSheetSort> repository)
    : IRequestHandler<GetBalanceSheetSortRequest, Response<List<GetBalanceSheetSortResultDto>>>
{
    public async Task<Response<List<GetBalanceSheetSortResultDto>>> Handle(
        GetBalanceSheetSortRequest request,
        CancellationToken cancellationToken
    )
    {
        List<GetBalanceSheetSortResultDto> balanceSheetSorts =
            await repository.ListAsync(BalanceSheetSortSpec.GetValidSpecifications(), cancellationToken);

        return balanceSheetSorts;
    }
}