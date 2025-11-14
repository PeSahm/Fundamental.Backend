using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;

public sealed class GetIncomeStatementSortResultDto
{
    public required ushort Order { get; init; }
    public required ushort CodalRow { get; init; }

    public string Description => IncomeStatementSort.GetDescription(CodalRow, Order);
}