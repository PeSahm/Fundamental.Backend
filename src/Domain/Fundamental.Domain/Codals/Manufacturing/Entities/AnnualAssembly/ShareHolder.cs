namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// اطلاعات سهامدار.
/// </summary>
public class ShareHolder
{
    /// <summary>
    /// سریال سهامدار.
    /// </summary>
    public int ShareHolderSerial { get; set; }

    /// <summary>
    /// نام سهامدار.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// تعداد سهام.
    /// </summary>
    public long ShareCount { get; set; }

    /// <summary>
    /// درصد مالکیت.
    /// </summary>
    public decimal? SharePercent { get; set; }
}
