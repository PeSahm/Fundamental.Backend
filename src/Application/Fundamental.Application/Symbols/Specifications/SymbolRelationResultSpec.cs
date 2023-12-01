using Ardalis.Specification;
using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SymbolRelationResultSpec : Specification<SymbolRelation, GetSymbolRelationsResultItem>
{
    public SymbolRelationResultSpec()
    {
        Query.Select(x => new GetSymbolRelationsResultItem(
            x.Id,
            x.Parent.Isin,
            x.Parent.Name,
            x.Parent.Title,
            x.Child.Isin,
            x.Child.Name,
            x.Child.Title,
            x.Ratio
        ));
    }
}