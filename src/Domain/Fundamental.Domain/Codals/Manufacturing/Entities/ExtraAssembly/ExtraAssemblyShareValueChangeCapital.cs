namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات تغییر ارزش اسمی.
/// </summary>
public sealed class ExtraAssemblyShareValueChangeCapital
{
    /// <summary>
    /// موافقت / عدم موافقت.
    /// </summary>
    public bool IsAccept { get; init; }

    /// <summary>
    /// تعداد سهام.
    /// </summary>
    public long? NewShareCount { get; init; }

    /// <summary>
    /// ارزش اسمی هر سهم.
    /// </summary>
    public int? NewShareValue { get; init; }
}
