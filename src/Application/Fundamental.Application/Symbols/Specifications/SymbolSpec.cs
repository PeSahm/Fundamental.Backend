using Ardalis.Specification;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public class SymbolSpec : Specification<Symbol>
{
    public SymbolSpec WhereIsin(string isin)
    {
        Query.Where(x => x.Isin == isin);
        return this;
    }

    public SymbolSpec Filter(string filter)
    {
        Query.Where(x => x.Isin.StartsWith(filter) || x.Name.StartsWith(filter) || x.Title.StartsWith(filter));
        return this;
    }

    public SymbolsResultDtoSpec Select()
    {
        SymbolsResultDtoSpec select = new();

        foreach (WhereExpressionInfo<Symbol> whereExpression in WhereExpressions)
        {
            select.Query.Where(whereExpression.Filter);
        }

        return select;
    }

    public SymbolSpec NoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}