namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;

/// <summary>
/// List item for Annual Assembly queries.
/// Contains basic metadata without detailed collections.
/// </summary>
public sealed class GetAnnualAssemblyListItem
{
    public Guid Id { get; init; }
    public string Isin { get; init; } = null!;
    public string Symbol { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string HtmlUrl { get; init; } = null!;
    public string Version { get; init; } = null!;
    public int FiscalYear { get; init; }
    public int YearEndMonth { get; init; }
    public DateTime AssemblyDate { get; init; }
    public ulong TraceNo { get; init; }
    public DateTime PublishDate { get; init; }
    public string AssemblyResultTypeTitle { get; init; } = null!;
}
