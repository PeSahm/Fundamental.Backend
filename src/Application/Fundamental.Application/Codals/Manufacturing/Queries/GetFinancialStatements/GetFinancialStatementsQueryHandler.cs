using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

public class GetFinancialStatementsQueryHandler : IRequestHandler<GetFinancialStatementsRequest,
    Response<Paginated<GetFinancialStatementsResultItem>>>
{
    private readonly IFinancialStatementsReadRepository _statementRepository;

    public GetFinancialStatementsQueryHandler(IFinancialStatementsReadRepository statementRepository)
    {
        _statementRepository = statementRepository;
    }

    public async Task<Response<Paginated<GetFinancialStatementsResultItem>>> Handle(
        GetFinancialStatementsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await _statementRepository.GetFinancialStatementsAsync(request, cancellationToken);
    }
}