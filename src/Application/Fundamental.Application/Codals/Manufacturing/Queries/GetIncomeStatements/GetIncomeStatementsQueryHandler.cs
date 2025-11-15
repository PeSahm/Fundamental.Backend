using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;

public sealed class GetIncomeStatementsQueryHandler(IIncomeStatementsReadRepository incomeStatementsReadRepository)
    : IRequestHandler<GetIncomeStatementsRequest, Response<Paginated<GetIncomeStatementsResultDto>>>
{
    public async Task<Response<Paginated<GetIncomeStatementsResultDto>>> Handle(
        GetIncomeStatementsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await incomeStatementsReadRepository.GetIncomeStatements(request, cancellationToken);
    }
}