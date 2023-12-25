using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Attributes;

public class BalanceSheetCategoryAttribute(BalanceSheetCategory balanceSheetCategory) : Attribute
{
    public BalanceSheetCategory BalanceSheetCategory { get; } = balanceSheetCategory;
}