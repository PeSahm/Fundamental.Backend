namespace Fundamental.Application.Symbols.Queries.GetSymbolRelations;

public sealed class GetSymbolRelationsResultItem
{
    public Guid Id { get; init; }
    public string InvestorIsin { get; init; }
    public string InvestorSymbol { get; init; }
    public string InvestorTitle { get; init; }
    public string InvestmentSymbol { get; init; }
    public string InvestmentTitle { get; init; }
    public float Ratio { get; init; }
}