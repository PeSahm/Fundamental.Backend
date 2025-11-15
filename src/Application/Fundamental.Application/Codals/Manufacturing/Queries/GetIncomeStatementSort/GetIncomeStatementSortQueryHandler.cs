using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;

public sealed class GetIncomeStatementSortQueryHandler(IRepository repository)
    : IRequestHandler<GetIncomeStatementSortRequest, Response<List<GetIncomeStatementSortResultDto>>>
{
    public async Task<Response<List<GetIncomeStatementSortResultDto>>> Handle(
        GetIncomeStatementSortRequest request,
        CancellationToken cancellationToken
    )
    {
        List<GetIncomeStatementSortResultDto> rows =
            await repository.ListAsync(IncomeStatementSortSpec.GetValidSpecifications(), cancellationToken);

        return rows;
    }
}