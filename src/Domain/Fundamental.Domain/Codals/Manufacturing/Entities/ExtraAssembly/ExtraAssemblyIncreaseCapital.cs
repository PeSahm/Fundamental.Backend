using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات افزایش سرمایه.
/// </summary>
public sealed class ExtraAssemblyIncreaseCapital
{
    /// <summary>
    /// مطالبات و اوردۀ نقدی (میلیون ریال).
    /// </summary>
    public int? CashIncoming { get; init; }

    /// <summary>
    /// سود انباشته (میلیون ریال).
    /// </summary>
    public int? RetainedEarning { get; init; }

    /// <summary>
    /// اندوخته (میلیون ریال).
    /// </summary>
    public int? Reserves { get; init; }

    /// <summary>
    /// مازاد تجدید ارزیابی دارایی ها (میلیون ریال).
    /// </summary>
    public int? RevaluationSurplus { get; init; }

    /// <summary>
    /// صرف سهام (میلیون ریال).
    /// </summary>
    public int? SarfSaham { get; init; }

    /// <summary>
    /// موافقت / عدم موافقت.
    /// </summary>
    public bool IsAccept { get; init; }

    /// <summary>
    /// مبلغ افزایش سرمایه (میلیون ریال).
    /// </summary>
    public int? CapitalIncreaseValue { get; init; }

    /// <summary>
    /// درصد افزایش سرمایه.
    /// </summary>
    public double? IncreasePercent { get; init; }

    /// <summary>
    /// نحوۀ تصویب.
    /// </summary>
    public CapitalIncreaseApprovalType Type { get; init; }

    /// <summary>
    /// قیمت سهام جهت عرضه عمومی-ریال.
    /// </summary>
    public decimal? CashForceclosurePriorityStockPrice { get; init; }

    /// <summary>
    /// توضیحات در خصوص قیمت سهام جهت عرضه عمومی.
    /// </summary>
    public string? CashForceclosurePriorityStockDesc { get; init; }

    /// <summary>
    /// تعداد سهام قابل عرضه به عموم.
    /// </summary>
    public int? CashForceclosurePriorityAvalableStockCount { get; init; }

    /// <summary>
    /// تعداد سهام جایزه.
    /// </summary>
    public int? CashForceclosurePriorityPrizeStockCount { get; init; }

    /// <summary>
    /// آورده نقدی با سلب حق تقدم از سهامداران فعلی.
    /// </summary>
    public decimal? CashForceclosurePriority { get; init; }
}
