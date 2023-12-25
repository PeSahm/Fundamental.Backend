using Fundamental.Domain.Codals.Enums;

namespace Fundamental.Domain.Attributes;

public class BalanceSheetCategoryAttribute(BalanceSheetCategory balanceSheetCategory) : Attribute
{
    public BalanceSheetCategory BalanceSheetCategory { get; } = balanceSheetCategory;
}