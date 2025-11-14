using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementList;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IFinancialStatementReadRepository
{
    Task<FinancialStatement?> GetLastFinancialStatement(string isin, DateOnly date, CancellationToken cancellationToken = default);

    Task<FinancialStatement?> GetLastFinancialStatement(
        string isin,
        FiscalYear year,
        StatementMonth month,
        CancellationToken cancellationToken = default
    );

    Task<Response<Paginated<GetFinancialStatementsResultDto>>> GetLastFinancialStatementList(
        GetFinancialStatementListRequest request,
        CancellationToken cancellationToken = default
    );
}