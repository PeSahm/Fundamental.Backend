using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class StockOwnershipSpec : Specification<StockOwnership>
{
    public StockOwnershipSpec WhereParentSymbolId(Guid parentSymbolId)
    {
        Query.Where(x => x.ParentSymbol.Id == parentSymbolId);
        return this;
    }

    public StockOwnershipSpec WhereSubsidiarySymbolName(string subsidiarySymbolName)
    {
        Query.Where(x => x.SubsidiarySymbolName == subsidiarySymbolName);
        return this;
    }
}