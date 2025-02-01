using Ardalis.Specification;
using Fundamental.BuildingBlock.Extensions;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Application.Symbols.Specifications;

public class IndexSpec : Specification<Index>
{
    public IndexSpec WhereIsin(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public IndexSpec WhereTseInsCode(string tseInsCode)
    {
        Query.Where(x => x.Symbol.TseInsCode == tseInsCode);
        return this;
    }

    public IndexSpec WhereDate(DateOnly date)
    {
        Query.Where(x => x.Date == date);
        return this;
    }

    public new IndexSpec AsNoTracking()
    {
        Query.AsNoTracking();
        return this;
    }

    public SimpleIndexSpec SelectSimpleIndex()
    {
        SimpleIndexSpec simpleIndexSpec = new();
        simpleIndexSpec.LoadSpecification(this);
        return simpleIndexSpec;
    }
}