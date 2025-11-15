namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// اعضای اصلی مجمع (رئیس، ناظرین، منشی).
/// </summary>
public class AssemblyChiefMembers
{
    /// <summary>
    /// رئیس مجمع.
    /// </summary>
    public string? AssemblyChief { get; set; }

    /// <summary>
    /// ناظر مجمع 1.
    /// </summary>
    public string? AssemblySuperVisor1 { get; set; }

    /// <summary>
    /// ناظر مجمع 2.
    /// </summary>
    public string? AssemblySuperVisor2 { get; set; }

    /// <summary>
    /// منشی مجمع.
    /// </summary>
    public string? AssemblySecretary { get; set; }
}
