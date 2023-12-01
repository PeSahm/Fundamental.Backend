namespace Fundamental.Application.Symbols.Queries;

public sealed record GetSymbolRelationsResultItem(
    string InvestorIsin,
    string InvestorSymbol,
    string InvestorTitle,
    string InvestmentIsin,
    string InvestmentSymbol,
    string InvestmentTitle,
    float Ratio
);