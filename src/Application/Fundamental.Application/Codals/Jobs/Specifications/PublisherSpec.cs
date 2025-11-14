using Ardalis.Specification;
using Fundamental.Domain.Codals;

namespace Fundamental.Application.Codals.Jobs.Specifications;

public class PublisherSpec : Specification<Publisher>
{
    public static PublisherSpec WithCodalId(string codalId)
    {
        PublisherSpec spec = new();
        spec.Query
            .Include(x => x.Symbol)
            .Where(x => x.CodalId == codalId);
        return spec;
    }

    public static PublisherSpec WhereParentIsin(string isin)
    {
        PublisherSpec spec = new();
        spec.Query
            .Include(x => x.Symbol)
            .Where(x => x.ParentSymbol != null && x.ParentSymbol.Isin == isin);
        return spec;
    }

    public static PublisherSpec WhereCodalIdsIn(IEnumerable<string> codalIds)
    {
        PublisherSpec spec = new();
        List<string> codalIdList = codalIds.ToList();
        spec.Query
            .Include(x => x.Symbol)
            .Include(x => x.ParentSymbol)
            .Where(x => codalIdList.Contains(x.CodalId));
        return spec;
    }
}