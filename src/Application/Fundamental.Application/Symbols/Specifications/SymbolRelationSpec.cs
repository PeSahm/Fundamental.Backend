using Ardalis.Specification;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public class SymbolRelationSpec : Specification<SymbolRelation>
{
    public SymbolRelationSpec WhereParentIsin(string isin)
    {
        Query.Where(x => x.Parent.Isin == isin);
        return this;
    }

    public SymbolRelationSpec WhereChildIsin(string isin)
    {
        Query.Where(x => x.Child.Isin == isin);
        return this;
    }

    public SymbolRelationSpec NoTracking()
    {
        Query.AsNoTracking();
        return this;
    }

    public SymbolRelationResultSpec Select()
    {
        SymbolRelationResultSpec select = new();

        foreach (WhereExpressionInfo<SymbolRelation> whereExpression in WhereExpressions)
        {
            select.Query.Where(whereExpression.Filter);
        }

        return select;
    }

    public SymbolRelationSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}