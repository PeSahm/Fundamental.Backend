namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public sealed record GetSymbolsResultDto(
    string Isin,
    string TseInsCode,
    string Title,
    string Name,
    ulong MarketCap
)
{
    public SymbolPriceInfo? PriceInfo { get; set; }
}