using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public sealed class StockOwnership : BaseEntity<Guid>
{
    public StockOwnership(
        Guid id,
        Symbol parentSymbol,
        string subsidiarySymbolName,
        decimal ownershipPercentage,
        SignedCodalMoney costPrice,
        ulong traceNo,
        DateTime createdAt
    )
    {
        Id = id;
        ParentSymbol = parentSymbol;
        SubsidiarySymbolName = subsidiarySymbolName;
        OwnershipPercentage = ownershipPercentage;
        CostPrice = costPrice;
        TraceNo = traceNo;
        CreatedAt = createdAt;
    }

    private StockOwnership()
    {
    }

    public Symbol ParentSymbol { get; private set; }

    public string SubsidiarySymbolName { get; private set; }

    public decimal OwnershipPercentage { get; set; }

    public SignedCodalMoney CostPrice { get; set; }

    public Symbol? SubsidiarySymbol { get; private set; }

    public ReviewStatus ReviewStatus { get; private set; } = ReviewStatus.Pending;

    public ulong? TraceNo { get; private set; }

    public StockOwnership SetReviewStatus(ReviewStatus reviewStatus, DateTime updatedAt)
    {
        ReviewStatus = reviewStatus;
        UpdatedAt = updatedAt;
        return this;
    }

    public StockOwnership SetSubsidiarySymbol(Symbol subsidiarySymbol, DateTime updatedAt)
    {
        SubsidiarySymbol = subsidiarySymbol;
        ReviewStatus = ReviewStatus.Approved;
        UpdatedAt = updatedAt;
        return this;
    }

    public StockOwnership ChangeOwnershipPercentage(
        decimal ownershipPercentage,
        SignedCodalMoney costPrice,
        ulong traceNo,
        DateTime updatedAt
    )
    {
        if (traceNo < TraceNo)
        {
            return this;
        }

        if (ownershipPercentage <= 100)
        {
            OwnershipPercentage = ownershipPercentage;
        }

        CostPrice = costPrice;
        UpdatedAt = updatedAt;
        return this;
    }
}