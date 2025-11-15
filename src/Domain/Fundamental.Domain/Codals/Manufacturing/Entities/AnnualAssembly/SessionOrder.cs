using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// دستور جلسه مجمع.
/// </summary>
public class SessionOrder
{
    /// <summary>
    /// نوع دستور جلسه.
    /// </summary>
    public SessionOrderType Type { get; set; }

    /// <summary>
    /// شرح دستور جلسه.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// نام فیلد.
    /// </summary>
    public string? FieldName { get; set; }
}
