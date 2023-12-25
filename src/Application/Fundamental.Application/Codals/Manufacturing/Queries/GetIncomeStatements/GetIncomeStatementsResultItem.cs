using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;

public sealed class GetIncomeStatementsResultItem
{
    public ushort Order { get; init; }
    public ushort CodalRow { get; init; }

    public string Description => IncomeStatementSort.GetDescription(CodalRow, Order);

    public decimal Value { get; init; }
}