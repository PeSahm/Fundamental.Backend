using Ardalis.Specification;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SymbolShareHolderSpec : Specification<SymbolShareHolder>
{
    public static SymbolShareHolderSpec WhereIsin(string isin)
    {
        SymbolShareHolderSpec spec = new();
        spec.Query.Where(x => x.Symbol.Isin == isin);
        return spec;
    }
}