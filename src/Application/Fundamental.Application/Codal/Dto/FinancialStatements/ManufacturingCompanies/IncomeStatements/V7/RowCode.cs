namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.IncomeStatements.V7;

public enum RowCode
{
    Sales = 3,
    CostOfGoodsSale = 4,
    GrossProfitLoss = 5,
    GeneralAdministrativeExpense = 6,
    ExceptionalExpenses = 44,
    OtherOperatingRevenue = 7,
    OtherOperatingExpense = 8,
    OperatingProfitLoss = 9,
    FinanceExpense = 10,
    OtherNoneOperationalIncomeOrExpense = 52,

    /// <summary>
    /// سود(زیان) عملیات در حال تداوم قبل از مالیات.
    /// </summary>
    ContinuingOperatingProfit,
    TaxForThisYear = 45,
    TaxForPrevYear = 46,
    NetIncomeLoss = 15,

    /// <summary>
    /// سود (زیان) خالص عملیات متوقف شده.
    /// </summary>
    BeShareDiscontinuedOperations = 21,

    /// <summary>
    /// سود(زیان) خالص.
    /// </summary>
    ProfitLoss = 17,

    /// <summary>
    ///  سود زیان عملیاتی هر سهم.
    /// </summary>
    BeShareContinuingOperationOperating = 19,

    /// <summary>
    /// سود و زیان غیر عملیاتی هر سهم.
    /// </summary>
    BeShareContinuingOperationNonOperating = 20,

    /// <summary>
    /// ناشي از عمليات در حال تداوم.
    /// </summary>
    BeShareContinuingOperatingProfitPerShare = 47,

    /// <summary>
    /// ناشي از عمليات متوقف شده.
    /// </summary>
    BeShareDiscontinuedOperationsPerShare = 48,

    EarningsPerShareAfterTax = 41,

    ListedCapital = 42,
    ShareValue = 43
}