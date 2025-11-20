namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات ترکیب سهامداران
/// </summary>
public sealed class ShareHolder
{
    /// <summary>
    /// سریال سهامدار
    /// </summary>
    public int? ShareHolderSerial { get; init; }

    /// <summary>
    /// نام سهامدار
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// تعداد سهام
    /// </summary>
    public long? ShareCount { get; init; }

    /// <summary>
    /// درصد مالکیت
    /// </summary>
    public double? SharePercent { get; init; }
}
