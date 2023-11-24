using Ardalis.Specification;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public class SymbolRelationSpecification : Specification<SymbolRelation>
{
    public SymbolRelationSpecification WhereParentIsin(string isin)
    {
        Query.Where(x => x.Parent.Isin == isin);
        return this;
    }

    public SymbolRelationSpecification WhereChildIsin(string isin)
    {
        Query.Where(x => x.Child.Isin == isin);
        return this;
    }

    public SymbolRelationSpecification NoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}