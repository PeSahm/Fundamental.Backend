using System.ComponentModel;
using Fundamental.Domain.Attributes;

namespace Fundamental.Domain.Codals.Manufacturing.Enums;

public static class IncomeStatementRow
{
    [Description("درآمد عملیاتی")]
    [IncomeStatementRow(1)]
    public static ushort Sales => 3;

    [Description("بهای تمام شده درآمدهای عملیاتی")]
    [IncomeStatementRow(2)]

    public static ushort CostOfGoodsSale => 4;

    [Description("سود(زیان) ناخالص")]
    [IncomeStatementRow(3)]

    public static ushort GrossProfitLoss => 5;

    [Description("هزینه های عمومی و اداری")]
    [IncomeStatementRow(4)]
    public static ushort GeneralAdministrativeExpense => 6;

    [Description("هزینه کاهش ارزش دریافتنی ها (هزینه استثنایی)")]
    [IncomeStatementRow(5)]
    public static ushort ExceptionalExpenses => 44;

    [Description("سایر درآمدها")]
    [IncomeStatementRow(6)]

    public static ushort OtherOperatingRevenue => 7;

    [Description("سایر هزینه ها")]
    [IncomeStatementRow(7)]
    public static ushort OtherOperatingExpense => 8;

    [Description("سود(زیان) عملیاتی")]
    [IncomeStatementRow(8)]
    public static ushort OperatingProfitLoss => 9;

    [Description("هزینه های مالی")]
    [IncomeStatementRow(9)]

    public static ushort FinanceExpense => 10;

    [Description("سایر درآمدها و هزینه های غیرعملیاتی")]
    [IncomeStatementRow(10)]
    public static ushort OtherNoneOperationalIncomeOrExpense => 52;

    [Description("سود(زیان) عملیات در حال تداوم قبل از مالیات")]
    [IncomeStatementRow(11)]
    public static ushort ContinuingOperatingProfit => 13;

    [Description("سال جاری")]
    [IncomeStatementRow(12)]

    public static ushort TaxForThisYear => 45;

    [Description("سال های قبل")]
    [IncomeStatementRow(13)]

    public static ushort TaxForPrevYear => 46;

    [Description("سود(زیان) خالص عملیات در حال تداوم")]
    [IncomeStatementRow(14)]

    public static ushort NetIncomeLoss => 15;

    [Description("سود (زیان) خالص عملیات متوقف شده")]
    [IncomeStatementRow(15)]

    public static ushort BeShareDiscontinuedOperations => 21;

    [Description("سود(زیان) خالص")]
    [IncomeStatementRow(16)]

    public static ushort ProfitLoss => 17;

    [Description("عملیاتی (ریال)")]
    [IncomeStatementRow(17)]

    public static ushort BeShareContinuingOperationOperating => 19;

    [Description("غیرعملیاتی (ریال)")]
    [IncomeStatementRow(18)]

    public static ushort BeShareContinuingOperationNonOperating => 20;

    [Description("ناشی از عملیات در حال تداوم")]
    [IncomeStatementRow(19)]

    public static ushort BeShareContinuingOperatingProfitPerShare => 47;

    [Description("ناشی از عملیات متوقف شده")]
    [IncomeStatementRow(20)]

    public static ushort BeShareDiscontinuedOperationsPerShare => 48;

    [Description("سود(زیان) پایه هر سهم")]
    [IncomeStatementRow(21)]

    public static ushort EarningsPerShareBeforeTax => 21;

    [Description("سود (زیان) خالص هر سهم – ریال")]
    [IncomeStatementRow(22)]

    public static ushort EarningsPerShareAfterTax => 21;

    [Description("سرمایه")]
    [IncomeStatementRow(23)]

    public static ushort ListedCapital => 42;

    public static ushort ShareValue => 43;
}