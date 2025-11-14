using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IIncomeStatementsReadRepository
{
    Task<Paginated<GetIncomeStatementsResultDto>> GetIncomeStatements(
        GetIncomeStatementsRequest request,
        CancellationToken cancellationToken = default
    );

    Task<SignedCodalMoney?> GetLastIncomeStatement(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth statementMonth,
        ushort incomeStatementRow,
        CancellationToken cancellationToken = default
    );
    Task<(FiscalYear Year, StatementMonth Month, ulong TraceNo)> GetLatestStatement(string isin, CancellationToken cancellationToken = default);
}