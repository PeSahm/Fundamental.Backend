using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;

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

    public StockOwnershipSpec WhereReviewStatus(ReviewStatus reviewStatus)
    {
        Query.Where(x => x.ReviewStatus == reviewStatus);
        return this;
    }

    public StockOwnershipSpec WhereId(Guid requestId)
    {
        Query.Where(x => x.Id == requestId);
        return this;
    }
}