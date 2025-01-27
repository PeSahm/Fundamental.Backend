using Fundamental.Domain.Common.BaseTypes;
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
        DateTime createdAt
    )
    {
        Id = id;
        ParentSymbol = parentSymbol;
        SubsidiarySymbolName = subsidiarySymbolName;
        OwnershipPercentage = ownershipPercentage;
        CostPrice = costPrice;
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

    public void SetSubsidiarySymbol(Symbol subsidiarySymbol, DateTime updatedAt)
    {
        SubsidiarySymbol = subsidiarySymbol;
        UpdatedAt = updatedAt;
    }

    public void ChangeOwnershipPercentage(
        decimal ownershipPercentage,
        SignedCodalMoney costPrice,
        DateTime updatedAt
    )
    {
        if (ownershipPercentage <= 100)
        {
            OwnershipPercentage = ownershipPercentage;
        }

        CostPrice = costPrice;
        UpdatedAt = updatedAt;
    }
}