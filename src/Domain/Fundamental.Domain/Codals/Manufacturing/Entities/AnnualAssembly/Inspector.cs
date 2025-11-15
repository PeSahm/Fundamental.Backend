using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// حسابرس یا بازرس قانونی.
/// </summary>
public class Inspector
{
    /// <summary>
    /// سریال حسابرس.
    /// </summary>
    public int Serial { get; set; }

    /// <summary>
    /// نام حسابرس.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// نوع (قانونی/علی البدل).
    /// </summary>
    public InspectorType Type { get; set; }
}
