using Fundamental.Domain.Codals.Manufacturing.Exceptions;
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
        string url,
        DateTime createdAt
    )
    {
        Id = id;
        ParentSymbol = parentSymbol;
        SubsidiarySymbolName = subsidiarySymbolName;
        OwnershipPercentage = ownershipPercentage;
        CostPrice = costPrice;
        TraceNo = traceNo;
        Url = url;
        CreatedAt = createdAt;
    }

    private StockOwnership()
    {
    }

    public Symbol ParentSymbol { get; private set; }

    public string SubsidiarySymbolName { get; private set; }

    public decimal OwnershipPercentage { get; private set; }

    public decimal? OwnershipPercentageProvidedByAdmin { get; private set; }

    public SignedCodalMoney CostPrice { get; private set; }

    public Symbol? SubsidiarySymbol { get; private set; }

    public ReviewStatus ReviewStatus { get; private set; } = ReviewStatus.Pending;

    public ulong? TraceNo { get; private set; }

    public string? Url { get; private set; }

    public StockOwnership Reject(DateTime updatedAt)
    {
        ReviewStatus = ReviewStatus.Rejected;
        UpdatedAt = updatedAt;
        return this;
    }

    public StockOwnership SetOwnershipPercentageProvidedByAdmin(decimal ownershipPercentage, DateTime updatedAt)
    {
        OwnershipPercentageProvidedByAdmin = ownershipPercentage;
        UpdatedAt = updatedAt;
        ReviewStatus = ReviewStatus.Approved;
        return this;
    }

    public StockOwnership SetSubsidiarySymbol(Symbol subsidiarySymbol, DateTime updatedAt)
    {
        SubsidiarySymbol = subsidiarySymbol;
        UpdatedAt = updatedAt;
        return this;
    }

    public StockOwnership ChangeOwnershipPercentage(
        decimal ownershipPercentage,
        SignedCodalMoney costPrice,
        ulong traceNo,
        string url,
        DateTime updatedAt
    )
    {
        if (traceNo != TraceNo)
        {
            throw new OwnershipTraceNoMismatchException(traceNo);
        }

        if (ReviewStatus == ReviewStatus.Rejected)
        {
            UpdatedAt = updatedAt;
            Url = url;
            OwnershipPercentage = ownershipPercentage;
            CostPrice = costPrice;
            return this;
        }

        if (ownershipPercentage != OwnershipPercentage)
        {
            ReviewStatus = ReviewStatus.Pending;
            OwnershipPercentage = ownershipPercentage;
            CostPrice = costPrice;
        }

        UpdatedAt = updatedAt;
        Url = url;

        return this;
    }
}