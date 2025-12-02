namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات کاهش سرمایه.
/// </summary>
public sealed class ExtraAssemblyDecreaseCapital
{
    /// <summary>
    /// مبلغ کاهش سرمایه.
    /// </summary>
    public decimal? CapitalDecreaseValue { get; init; }

    /// <summary>
    /// درصد کاهش سرمایه.
    /// </summary>
    public decimal? DecreasePercent { get; init; }

    /// <summary>
    /// موافقت / عدم موافقت.
    /// </summary>
    public bool IsAccept { get; init; }

    /// <summary>
    /// مبلغ (میلیون ریال).
    /// </summary>
    public long? NewCapital { get; init; }

    /// <summary>
    /// تعداد سهام.
    /// </summary>
    public long? NewShareCount { get; init; }

    /// <summary>
    /// ارزش اسمی هر سهم.
    /// </summary>
    public int? NewShareValue { get; init; }
}