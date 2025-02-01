using Ardalis.Specification;
using Fundamental.BuildingBlock.Extensions;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SymbolSpec : Specification<Symbol>
{
    public SymbolSpec WhereIsin(string isin, bool cacheResult = false)
    {
        Query.Where(x => x.Isin == isin);

        if (cacheResult)
        {
            Query.EnableCache(nameof(SymbolSpec), "Isin", isin);
        }

        return this;
    }

    public SymbolSpec WhereName(string name)
    {
        Query.Where(x => x.Name == name);
        return this;
    }

    public SymbolSpec Filter(string filter)
    {
        Query.Where(x => x.Isin.StartsWith(filter) || x.Name.StartsWith(filter) || x.Title.StartsWith(filter));
        return this;
    }

    public new SymbolSpec AsNoTracking()
    {
        Query.AsNoTracking();
        return this;
    }

    public SymbolSpec ShowOfficialSymbols(bool showOfficialSymbolsOnly)
    {
        if (showOfficialSymbolsOnly)
        {
            Query.Where(x => !x.IsUnOfficial);
        }
        else
        {
            Query.Where(x => x.IsUnOfficial);
        }

        return this;
    }

    public SymbolsResultDtoSpec Select()
    {
        SymbolsResultDtoSpec select = new();
        select.LoadSpecification(this);
        return select;
    }

    public new SymbolSpec NoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}