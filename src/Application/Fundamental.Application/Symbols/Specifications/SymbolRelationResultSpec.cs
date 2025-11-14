using Ardalis.Specification;
using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SymbolRelationResultSpec : Specification<SymbolRelation, GetSymbolRelationsResultItem>
{
    public SymbolRelationResultSpec()
    {
        Query.Select(x => new GetSymbolRelationsResultItem
        {
            Id = x.Id,
            InvestorIsin = x.Parent.Isin,
            InvestorSymbol = x.Parent.Name,
            InvestorTitle = x.Parent.Title,
            InvestmentSymbol = x.Child.Name,
            InvestmentTitle = x.Child.Title,
            Ratio = x.Ratio
        });
    }
}