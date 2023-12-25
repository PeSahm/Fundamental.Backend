using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementById;

public sealed class
    GetFinancialStatementByIdQueryHandler : IRequestHandler<GetFinancialStatementByIdRequest, Response<GetFinancialStatementsResultItem>>
{
    private readonly IRepository<FinancialStatement> _statementRepository;

    public GetFinancialStatementByIdQueryHandler(IRepository<FinancialStatement> statementRepository)
    {
        _statementRepository = statementRepository;
    }

    public async Task<Response<GetFinancialStatementsResultItem>> Handle(
        GetFinancialStatementByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetFinancialStatementsResultItem? statement = await _statementRepository.FirstOrDefaultAsync(
            new FinancialStatementSpec()
                .WhereId(request.Id)
                .Select(),
            cancellationToken);

        if (statement is null)
        {
            return GetFinancialStatementByIdErrorCodes.FinancialStatementNotFound;
        }

        return statement;
    }
}