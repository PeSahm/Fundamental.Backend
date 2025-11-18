using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// حسابرس یا بازرس قانونی.
/// </summary>
public class Inspector
{
    private Inspector()
    {
    }

    public Inspector(int serial, string? name, InspectorType type)
    {
        Serial = serial;
        Name = name;
        Type = type;
    }

    /// <summary>
    /// سریال حسابرس.
    /// </summary>
    public int Serial { get; private set; }

    /// <summary>
    /// نام حسابرس.
    /// </summary>
    public string? Name { get; private set; }

    /// <summary>
    /// نوع (قانونی/علی البدل).
    /// </summary>
    public InspectorType Type { get; private set; }
}
