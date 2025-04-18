using System.ComponentModel;

namespace Fundamental.Domain.Codals.Manufacturing.Enums;

public enum BalanceSheetCategory
{
    [Description("دارایی")]
    Assets = 1,

    [Description("بدهی")]
    Liability = 2
}