using System.ComponentModel;

namespace Fundamental.Domain.Codals.Enums;

public enum BalanceSheetCategory
{
    [Description("دارایی")]
    Assets = 1,

    [Description("بدهی")]
    Liability = 2,
}