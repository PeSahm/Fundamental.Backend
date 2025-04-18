using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Domain.Symbols.Entities;

public class SymbolShareHolder : BaseEntity<Guid>
{
    public SymbolShareHolder(
        Guid id,
        Symbol symbol,
        string shareHolderName,
        decimal sharePercentage,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        ShareHolderName = shareHolderName;
        SharePercentage = sharePercentage;
        CreatedAt = createdAt;
    }

    private SymbolShareHolder()
    {
    }

    public Symbol Symbol { get; private set; }

    public string ShareHolderName { get; private set; }

    public decimal SharePercentage { get; private set; }

    public ReviewStatus ReviewStatus { get; private set; } = ReviewStatus.Pending;

    public Symbol? ShareHolderSymbol { get; private set; }

    public void ChangeReviewStatus(ReviewStatus reviewStatus, DateTime updatedAt)
    {
        ReviewStatus = reviewStatus;
        UpdatedAt = updatedAt;
    }

    public void SetShareHolderSymbol(Symbol shareHolderSymbol, DateTime updatedAt)
    {
        ShareHolderSymbol = shareHolderSymbol;
        ReviewStatus = ReviewStatus.Approved;
        UpdatedAt = updatedAt;
    }

    public void ChangeSharePercentage(decimal sharePercentage, DateTime updatedAt)
    {
        if (sharePercentage == SharePercentage)
        {
            return;
        }

        SharePercentage = sharePercentage;
        ReviewStatus = ReviewStatus.Pending;
        UpdatedAt = updatedAt;
    }
}