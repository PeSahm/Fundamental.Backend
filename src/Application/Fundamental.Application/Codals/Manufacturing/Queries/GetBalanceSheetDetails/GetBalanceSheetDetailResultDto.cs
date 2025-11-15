using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;

public sealed class GetBalanceSheetDetailResultDto
{
    public ushort Order { get; init; }
    public ushort CodalRow { get; init; }

    public string Description => BalanceSheetSort.GetDescription(CodalRow, Category);

    public required BalanceSheetCategory Category { get; init; }

    public string? CategoryDescription => Category.GetDescription();

    public decimal Value { get; init; }
}