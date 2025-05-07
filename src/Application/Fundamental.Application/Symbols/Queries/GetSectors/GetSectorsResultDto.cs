namespace Fundamental.Application.Symbols.Queries.GetSectors;

public sealed class GetSectorsResultDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SymbolCount { get; set; }
}