using Ardalis.Specification;
using Fundamental.Domain.Prices.Entities;

namespace Fundamental.Application.Prices.Specifications;

public class ClosePriceSpec : Specification<ClosePrice>
{
    public static ClosePriceSpec Where(string isin, DateOnly date)
    {
        ClosePriceSpec spec = new();
        spec.Query.Where(x => x.Symbol.Isin == isin)
            .Where(x => x.Date == date);
        return spec;
    }
}