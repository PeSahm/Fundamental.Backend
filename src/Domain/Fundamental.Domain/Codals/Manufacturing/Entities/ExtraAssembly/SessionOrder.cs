using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// دستور جلسه مجمع عمومی فوق‌العاده
/// </summary>
public sealed class SessionOrder
{
    /// <summary>
    /// نوع دستور جلسه
    /// </summary>
    public SessionOrderType Type { get; init; }

    /// <summary>
    /// عنوان فارسی دستورجلسه
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// عنوان انگلیسی دستورجلسه
    /// </summary>
    public string? FieldName { get; init; }
}
