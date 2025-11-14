using Ardalis.Specification;
using Fundamental.Domain.Common.Enums;
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

    public static SymbolShareHolderSpec WhereId(Guid id)
    {
        SymbolShareHolderSpec spec = new();
        spec.Query.Where(x => x.Id == id);
        return spec;
    }

    public static SymbolShareHolderSpec WhereShareHolderName(string shareHolderName, ReviewStatus reviewStatus)
    {
        SymbolShareHolderSpec spec = new();
        spec.Query.Where(x => x.ShareHolderName == shareHolderName)
            .Where(x => x.ReviewStatus == reviewStatus);
        return spec;
    }
}