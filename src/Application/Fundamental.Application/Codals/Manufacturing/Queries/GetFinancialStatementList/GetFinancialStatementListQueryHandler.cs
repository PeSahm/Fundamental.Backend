using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementList;

public sealed class GetFinancialStatementListQueryHandler(IFinancialStatementReadRepository repository)
    : IRequestHandler<GetFinancialStatementListRequest, Response<Paginated<GetFinancialStatementsResultDto>>>
{
    public async Task<Response<Paginated<GetFinancialStatementsResultDto>>> Handle(
        GetFinancialStatementListRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetLastFinancialStatementList(request, cancellationToken);
    }
}