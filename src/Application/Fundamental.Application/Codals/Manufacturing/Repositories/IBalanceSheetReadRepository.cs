using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IBalanceSheetReadRepository
{
    Task<Paginated<GetBalanceSheetResultDto>> GetBalanceSheet(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken = default
    );

    Task<List<SimpleBalanceSheet>> GetLastBalanceSheetDetails(
        CancellationToken cancellationToken = default
    );

    Task<SignedCodalMoney?> GetLastBalanceSheetItem(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth statementMonth,
        BalanceSheetCategory category,
        ushort balanceSheetRow,
        CancellationToken cancellationToken = default
    );
}