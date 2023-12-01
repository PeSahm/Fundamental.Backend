namespace Fundamental.Application.Symbols.Queries.GetSymbolRelations;

public sealed record GetSymbolRelationsResultItem(
    Guid Id,
    string InvestorIsin,
    string InvestorSymbol,
    string InvestorTitle,
    string InvestmentIsin,
    string InvestmentSymbol,
    string InvestmentTitle,
    float Ratio
);