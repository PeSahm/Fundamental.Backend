namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اعضای هیئت رئیسه مجمع.
/// </summary>
public sealed class AssemblyChiefMembersInfo
{
    /// <summary>
    /// رئیس مجمع.
    /// </summary>
    public string? AssemblyChief { get; set; }

    /// <summary>
    /// ناظر اول مجمع.
    /// </summary>
    public string? AssemblySuperVisor1 { get; set; }

    /// <summary>
    /// ناظر دوم مجمع.
    /// </summary>
    public string? AssemblySuperVisor2 { get; set; }

    /// <summary>
    /// منشی مجمع.
    /// </summary>
    public string? AssemblySecretary { get; set; }
}
