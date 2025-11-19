namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات مجمع بعدی - اعلام تنفس.
/// </summary>
public sealed class NextSessionInfo
{
    /// <summary>
    /// توضیحات اعلام تنفس.
    /// </summary>
    public string? BreakDesc { get; set; }

    /// <summary>
    /// ساعت برگزاری مجمع بعدی.
    /// </summary>
    public string? Hour { get; set; }

    /// <summary>
    /// تاریخ برگزاری مجمع بعدی.
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// روز برگزاری مجمع بعدی.
    /// </summary>
    public string? Day { get; set; }

    /// <summary>
    /// محل برگزاری مجمع بعدی.
    /// </summary>
    public string? Location { get; set; }
}
