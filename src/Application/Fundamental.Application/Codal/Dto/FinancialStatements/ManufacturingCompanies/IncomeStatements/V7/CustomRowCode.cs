using System.ComponentModel;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.IncomeStatements.V7;

public static class CustomRowCode
{
    [Description("ساير هزينه‌ها")]
    public static int OtherExpenses => 8;

    [Description("سود(زيان) عملياتى")]
    public static int OperatingProfitLoss => 9;

    [Description("هزينه هاى مالى")]
    public static int FinancialExpenses => 10;

    [Description("ساير درآمدها و هزينه هاى غيرعملياتى")]
    public static int OtherNonOperatingIncomesAndExpenses => 52;

    [Description("سود(زيان) عمليات در حال تداوم قبل از ماليات")]
    public static int ProfitLossFromContinuingOperationsBeforeTax => 13;

    [Description("هزينه ماليات بر درآمد")]
    public static int IncomeTaxExpense => 50;

    [Description("سال جاري")]
    public static int CurrentYear => 45;

    [Description("سال‌هاي قبل")]
    public static int PreviousYears => 46;

    [Description("سود(زيان) خالص عمليات در حال تداوم")]
    public static int NetProfitLossFromContinuingOperations => 15;

    [Description("عمليات متوقف شده")]
    public static int DiscontinuedOperations => 51;

    [Description("سود (زيان) خالص عمليات متوقف شده")]
    public static int NetProfitLossFromDiscontinuedOperations => 21;

    [Description("سود(زيان) خالص")]
    public static int NetProfitLoss => 17;

    [Description("سود(زيان) پايه هر سهم")]
    public static int BasicEarningsLossPerShare => 18;

    [Description("عملياتي (ريال)")]
    public static int OperatingRial => 19;

    [Description("غيرعملياتي (ريال)")]
    public static int NonOperatingRial => 20;

    [Description("ناشي از عمليات در حال تداوم")]
    public static int ResultingFromContinuingOperations => 47;

    [Description("ناشي از عمليات متوقف شده")]
    public static int ResultingFromDiscontinuedOperations => 48;

    [Description("سود(زيان) پايه هر سهم")]
    public static int BasicProfitLossPerShare => 21;

    [Description("سود (زيان) خالص هر سهم – ريال")]
    public static int NetProfitLossPerShareRial => 41;

    [Description("سرمايه")]
    public static int Capital => 42;

    [Description("عمليات در حال تداوم")]
    public static int ContinuingOperations => 49;

    [Description("ارزش اسمی هر سهم")]
    public static int NominalValuePerShare => 43;

    [Description("درآمدهاي عملياتي")]
    public static int OperatingRevenues => 3;

    [Description("بهاى تمام شده درآمدهاي عملياتي")]
    public static int CostOfOperatingRevenues => 4;

    [Description("سود(زيان) ناخالص")]
    public static int GrossProfitLoss => 5;

    [Description("هزينه هاى فروش، ادارى و عمومى")]
    public static int SellingGeneralAndAdministrativeExpenses => 6;

    [Description("هزينه کاهش ارزش دريافتني ها (هزينه استثنايي)")]
    public static int ImpairmentOfReceivablesExpenseExceptional => 44;

    [Description("ساير درآمدها")]
    public static int OtherIncomes => 7;
}