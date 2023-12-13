using System.ComponentModel;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.IncomeStatements.V7;

public enum CustomRowCode
{
    [Description("درآمد های عملیاتی")]
    Sales = 1,

    [Description("بهای تمام شده درآمد های عملیاتی")]
    CostOfGoodsSale = 2,

    [Description("سود(زیان) ناخالص")]
    GrossProfitLoss = 3,

    [Description("هزینه های فروش ، اداری و عمومی")]
    GeneralAdministrativeExpense = 4,

    [Description("هزینه کاهش ارزش دریافتنی ها (هزینه استثنایی)")]
    ExceptionalExpenses = 5,

    [Description("سایر درآمد ها")]
    OtherOperatingRevenue = 6,

    [Description("سایر هزینه ها")]
    OtherOperatingExpense = 7,

    [Description("سود(زیان) عملیاتی")]
    OperatingProfitLoss = 8,

    [Description("هزینه های مالی")]
    FinanceExpense = 9,

    [Description("سایر درآمدها و هزینه های غیر عملیاتی")]
    OtherNoneOperationalIncomeOrExpense = 10,

    [Description("سود(زيان) عمليات در حال تداوم قبل از ماليات")]
    ContinuingOperatingProfit = 11,

    [Description("مالیات سال جاری")]
    TaxForThisYear = 12,

    [Description("مالیات سال های قبل")]
    TaxForPrevYear = 13,

    [Description("سود(زيان) خالص عمليات در حال تداوم")]
    NetIncomeLoss = 14,

    [Description("سود (زيان) خالص عمليات متوقف شده")]
    BeShareDiscontinuedOperations = 15,

    [Description("سود(زيان) خالص")]
    ProfitLoss = 16,

    [Description("سود زیان عملیاتی هر سهم")]
    BeShareContinuingOperationOperating = 17,

    [Description("سود و زیان غیر عملیاتی هر سهم")]
    BeShareContinuingOperationNonOperating = 18,

    [Description("سود(زیان) ناشی از عملیات در حال تداوم")]
    BeShareContinuingOperatingProfitPerShare = 19,

    [Description("سود(زیان) ناشی از عملیات متوقف شده")]
    BeShareDiscontinuedOperationsPerShare = 20,

    [Description("سود(زيان) پايه هر سهم")]
    EarningsPerShareBeforeTax = 21,

    [Description("سود (زيان) خالص هر سهم – ريال")]
    EarningsPerShareAfterTax = 22,

    [Description("سرمايه")]
    ListedCapital = 23,

    ShareValue = 24
}