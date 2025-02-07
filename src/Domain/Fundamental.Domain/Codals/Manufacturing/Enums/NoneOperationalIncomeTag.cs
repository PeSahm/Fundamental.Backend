using System.ComponentModel;

namespace Fundamental.Domain.Codals.Manufacturing.Enums;

public enum NoneOperationalIncomeTag
{
    [Description("درآمد بانکی و سپرده های کوتاه مدت تکرار پذیر")]
    BankInterestIncome = 1,
    [Description("درآمد سهام و سود سهام")]
    StockDividendIncome = 2,
    [Description("سایر درآمدهای غیر عملیاتی تکرار پذیر")]
    OtherRenewableIncome = 3,
}