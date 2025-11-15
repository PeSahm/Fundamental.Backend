namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Row codes for financing detail section.
/// </summary>
public enum FinancingDetailRowCode
{
    /// <summary>
    /// Bank facilities received (current period).
    /// </summary>
    BankFacilitiesCurrent = 11,

    /// <summary>
    /// Facilities settled from beginning of fiscal year (current period).
    /// </summary>
    SettledFacilitiesCurrent = 13,

    /// <summary>
    /// Total sum (current period).
    /// </summary>
    TotalCurrent = 14,

    /// <summary>
    /// Transfer to assets (current period).
    /// </summary>
    TransferToAssetsCurrent = 15,

    /// <summary>
    /// Financial expense for period (current period).
    /// </summary>
    FinancialExpenseCurrent = 16,

    /// <summary>
    /// Bank facilities received (estimated period).
    /// </summary>
    BankFacilitiesEstimated = 56,

    /// <summary>
    /// Facilities settled from beginning of fiscal year (estimated period).
    /// </summary>
    SettledFacilitiesEstimated = 58,

    /// <summary>
    /// Total sum (estimated period).
    /// </summary>
    TotalEstimated = 59,

    /// <summary>
    /// Transfer to assets (estimated period).
    /// </summary>
    TransferToAssetsEstimated = 60,

    /// <summary>
    /// Financial expense for period (estimated period).
    /// </summary>
    FinancialExpenseEstimated = 61
}
