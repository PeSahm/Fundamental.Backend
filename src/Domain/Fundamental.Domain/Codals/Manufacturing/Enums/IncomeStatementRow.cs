using System.ComponentModel;

namespace Fundamental.Domain.Codals.Manufacturing.Enums;

public static class IncomeStatementRow
{
    [Description("درآمد عملیاتی")]
    public static ushort Sales => 3;

    [Description("بهای تمام شده درآمدهای عملیاتی")]
    public static ushort CostOfGoodsSale => 4;

    [Description("سود(زیان) ناخالص")]
    public static ushort GrossProfitLoss => 5;

    [Description("هزینه های عمومی و اداری")]
    public static ushort GeneralAdministrativeExpense => 6;

    [Description("هزینه کاهش ارزش دریافتنی ها (هزینه استثنایی)")]
    public static ushort ExceptionalExpenses => 44;

    [Description("سایر درآمدها")]
    public static ushort OtherOperatingRevenue => 7;

    [Description("سایر هزینه ها")]
    public static ushort OtherOperatingExpense => 8;

    [Description("سود(زیان) عملیاتی")]
    public static ushort OperatingProfitLoss => 9;

    [Description("هزینه های مالی")]
    public static ushort FinanceExpense => 10;

    [Description("سایر درآمدها و هزینه های غیرعملیاتی")]
    public static ushort OtherNoneOperationalIncomeOrExpense => 52;

    [Description("سود(زیان) عملیات در حال تداوم قبل از مالیات")]
    public static ushort ContinuingOperatingProfit => 13;

    [Description("سال جاری")]
    public static ushort TaxForThisYear => 45;

    [Description("سال های قبل")]
    public static ushort TaxForPrevYear => 46;

    [Description("سود(زیان) خالص عملیات در حال تداوم")]
    public static ushort NetIncomeLoss => 15;

    [Description("سود (زیان) خالص عملیات متوقف شده")]
    public static ushort BeShareDiscontinuedOperations => 21;

    [Description("سود(زیان) خالص")]
    public static ushort ProfitLoss => 17;

    [Description("عملیاتی (ریال)")]
    public static ushort BeShareContinuingOperationOperating => 19;

    [Description("غیرعملیاتی (ریال)")]
    public static ushort BeShareContinuingOperationNonOperating => 20;

    [Description("ناشی از عملیات در حال تداوم")]
    public static ushort BeShareContinuingOperatingProfitPerShare => 47;

    [Description("ناشی از عملیات متوقف شده")]
    public static ushort BeShareDiscontinuedOperationsPerShare => 48;

    [Description("سود(زیان) پایه هر سهم")]
    public static ushort EarningsPerShareAfterTax => 21;

    public static ushort ListedCapital => 42;

    public static ushort ShareValue => 43;
}