using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;

public sealed class GetBalanceSheetSortResultDto
{
    public required ushort Order { get; init; }
    public required ushort CodalRow { get; init; }

    public string Description => BalanceSheetSort.GetDescription(CodalRow, Category);
    public required BalanceSheetCategory Category { get; init; }

    public string? CategoryDescription => Category.GetDescription();
}