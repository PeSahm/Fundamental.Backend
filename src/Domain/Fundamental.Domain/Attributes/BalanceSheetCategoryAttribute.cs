using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class BalanceSheetCategoryAttribute(BalanceSheetCategory balanceSheetCategory) : Attribute
{
    public BalanceSheetCategory BalanceSheetCategory { get; } = balanceSheetCategory;
}