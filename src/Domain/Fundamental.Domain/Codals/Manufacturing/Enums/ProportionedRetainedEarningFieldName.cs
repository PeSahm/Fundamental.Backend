using System.ComponentModel;

namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Field names for proportioned retained earnings (سود انباشته تخصیص یافته).
/// </summary>
public enum ProportionedRetainedEarningFieldName
{
    /// <summary>
    /// سود (زیان) خالص.
    /// </summary>
    [Description("سود (زیان) خالص")]
    NetIncomeLoss,

    /// <summary>
    /// سود (زیان) انباشته ابتدای دوره.
    /// </summary>
    [Description("سود (زیان) انباشته ابتدای دوره")]
    BeginingRetainedEarnings,

    /// <summary>
    /// تعدیلات سنواتی.
    /// </summary>
    [Description("تعدیلات سنواتی")]
    AnnualAdjustment,

    /// <summary>
    /// سود (زیان) انباشته ابتدای دوره تعدیل شده.
    /// </summary>
    [Description("سود (زیان) انباشته ابتدای دوره تعدیل شده")]
    AdjustedBeginingRetainedEarnings,

    /// <summary>
    /// سود سهام مصوب (مجمع سال قبل).
    /// </summary>
    [Description("سود سهام مصوب (مجمع سال قبل)")]
    PreYearDevidedRetainedEarning,

    /// <summary>
    /// تغییرات سرمایه از محل سود (زیان) انباشته.
    /// </summary>
    [Description("تغییرات سرمایه از محل سود (زیان) انباشته")]
    TransferToCapital,

    /// <summary>
    /// سود انباشته ابتدای دوره تخصیص نیافته.
    /// </summary>
    [Description("سود انباشته ابتدای دوره تخصیص نیافته")]
    UnallocatedRetainedEarningsAtTheBeginningOfPeriod,

    /// <summary>
    /// انتقال از سایر اقلام حقوق صاحبان سهام.
    /// </summary>
    [Description("انتقال از سایر اقلام حقوق صاحبان سهام")]
    TransfersFromOtherEquityItems,

    /// <summary>
    /// سود قابل تخصیص.
    /// </summary>
    [Description("سود قابل تخصیص")]
    ProportionableRetainedEarnings,

    /// <summary>
    /// انتقال به اندوخته‌ قانونی.
    /// </summary>
    [Description("انتقال به اتدوخته قانونی")]
    LegalReserve,

    /// <summary>
    /// انتقال به سایر اندوخته ها.
    /// </summary>
    [Description("انتقال به سایر اندوخته ها")]
    ExtenseReserve,

    /// <summary>
    /// سود (زیان) انباشته پايان دوره.
    /// </summary>
    [Description("سود (زیان) انباشته پايان دوره")]
    EndingRetainedEarnings,

    /// <summary>
    /// سود سهام مصوب (مجمع سال جاری).
    /// </summary>
    [Description("سود سهام مصوب (مجمع سال جاری)")]
    DividedRetainedEarning,

    /// <summary>
    /// سود (زیان) انباشته پایان دوره (با لحاظ نمودن مصوبات مجمع).
    /// </summary>
    [Description("سود (زیان) انباشته پایان دوره (با لحاظ نمودن مصوبات مجمع)")]
    TotalEndingRetainedEarnings,

    /// <summary>
    /// سود (زیان) خالص هر سهم- ریال.
    /// </summary>
    [Description("سود (زیان) خالص هر سهم- ریال")]
    EarningsPerShareAfterTax,

    /// <summary>
    /// سود نقدی هر سهم (ریال).
    /// </summary>
    [Description("سود نقدی هر سهم (ریال)")]
    DividendPerShare,

    /// <summary>
    /// سرمایه.
    /// </summary>
    [Description("سرمایه")]
    ListedCapital,
}
