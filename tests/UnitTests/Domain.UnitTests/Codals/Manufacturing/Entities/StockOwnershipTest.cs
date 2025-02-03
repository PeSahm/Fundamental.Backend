using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;

namespace Domain.UnitTests.Codals.Manufacturing.Entities;

public class StockOwnershipTests
{
    [Fact]
    public void CreateStockOwnership_ShouldInitializeCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        string subsidiarySymbolName = "Subsidiary";
        decimal ownershipPercentage = 50m;
        SignedCodalMoney costPrice = new SignedCodalMoney(1000000);
        ulong traceNo = 123456;
        string url = "http://example.com";
        DateTime createdAt = DateTime.UtcNow;

        // Act
        StockOwnership stockOwnership = new StockOwnership(
            id,
            parentSymbol,
            subsidiarySymbolName,
            ownershipPercentage,
            costPrice,
            traceNo,
            url,
            createdAt);

        // Assert
        Assert.Equal(id, stockOwnership.Id);
        Assert.Equal(parentSymbol, stockOwnership.ParentSymbol);
        Assert.Equal(subsidiarySymbolName, stockOwnership.SubsidiarySymbolName);
        Assert.Equal(ownershipPercentage, stockOwnership.OwnershipPercentage);
        Assert.Equal(costPrice, stockOwnership.CostPrice);
        Assert.Equal(traceNo, stockOwnership.TraceNo);
        Assert.Equal(url, stockOwnership.Url);
        Assert.Equal(createdAt, stockOwnership.CreatedAt);
        Assert.Equal(ReviewStatus.Pending, stockOwnership.ReviewStatus);
    }

    [Fact]
    public void ChangeOwnershipPercentage_ShouldUpdateCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        decimal newOwnershipPercentage = 60m;
        SignedCodalMoney newCostPrice = new SignedCodalMoney(2000000);
        ulong newTraceNo = 123457;
        string newUrl = "http://example.com/new";
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.ChangeOwnershipPercentage(newOwnershipPercentage, newCostPrice, newTraceNo, newUrl, updatedAt);

        // Assert
        Assert.Equal(newOwnershipPercentage, stockOwnership.OwnershipPercentage);
        Assert.Equal(newCostPrice, stockOwnership.CostPrice);
        Assert.Equal(newTraceNo, stockOwnership.TraceNo);
        Assert.Equal(newUrl, stockOwnership.Url);
        Assert.Equal(updatedAt, stockOwnership.UpdatedAt);
        Assert.Equal(ReviewStatus.Pending, stockOwnership.ReviewStatus);
    }

    [Fact]
    public void Reject_ShouldUpdateReviewStatusToRejected()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.Reject(updatedAt);

        // Assert
        Assert.Equal(ReviewStatus.Rejected, stockOwnership.ReviewStatus);
        Assert.Equal(updatedAt, stockOwnership.UpdatedAt);
    }

    [Fact]
    public void SetSubsidiarySymbol_ShouldUpdateCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        Symbol subsidiarySymbol = CreateSymbol(Guid.NewGuid());
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.SetSubsidiarySymbol(subsidiarySymbol, updatedAt);

        // Assert
        Assert.Equal(subsidiarySymbol, stockOwnership.SubsidiarySymbol);
        Assert.Equal(updatedAt, stockOwnership.UpdatedAt);
    }

    [Fact]
    public void ChangeOwnershipPercentage_ShouldNotUpdate_WhenTraceNoIsLower()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        decimal newOwnershipPercentage = 60m;
        SignedCodalMoney newCostPrice = new SignedCodalMoney(2000000);
        ulong newTraceNo = 123455; // Lower trace number
        string newUrl = "http://example.com/new";
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.ChangeOwnershipPercentage(newOwnershipPercentage, newCostPrice, newTraceNo, newUrl, updatedAt);

        // Assert
        Assert.Equal(50m, stockOwnership.OwnershipPercentage); // Should not change
        Assert.Equal(123456u, stockOwnership.TraceNo); // Should not change
    }

    [Fact]
    public void ChangeOwnershipPercentage_ShouldUpdate_WhenReviewStatusIsRejected()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        stockOwnership.Reject(DateTime.UtcNow); // Set status to Rejected
        decimal newOwnershipPercentage = 60m;
        SignedCodalMoney newCostPrice = new SignedCodalMoney(2000000);
        ulong newTraceNo = 123457;
        string newUrl = "http://example.com/new";
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.ChangeOwnershipPercentage(newOwnershipPercentage, newCostPrice, newTraceNo, newUrl, updatedAt);

        // Assert
        Assert.Equal(newOwnershipPercentage, stockOwnership.OwnershipPercentage);
        Assert.Equal(newCostPrice, stockOwnership.CostPrice);
        Assert.Equal(newTraceNo, stockOwnership.TraceNo);
        Assert.Equal(newUrl, stockOwnership.Url);
        Assert.Equal(updatedAt, stockOwnership.UpdatedAt);
    }

    [Fact]
    public void SetOwnershipPercentageProvidedByAdmin_ShouldUpdateCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Symbol parentSymbol = CreateSymbol(id);
        StockOwnership stockOwnership = CreateStockOwnership(id, parentSymbol);
        decimal adminOwnershipPercentage = 55m;
        DateTime updatedAt = DateTime.UtcNow;

        // Act
        stockOwnership.SetOwnershipPercentageProvidedByAdmin(adminOwnershipPercentage, updatedAt);

        // Assert
        Assert.Equal(adminOwnershipPercentage, stockOwnership.OwnershipPercentageProvidedByAdmin);
        Assert.Equal(ReviewStatus.Approved, stockOwnership.ReviewStatus);
        Assert.Equal(updatedAt, stockOwnership.UpdatedAt);
    }

    private static Symbol CreateSymbol(Guid id)
    {
        return new Symbol(
            id,
            "ISIN",
            "TSE",
            "EnName",
            "SymbolEnName",
            "Title",
            "Name",
            "CompanyEnCode",
            "CompanyPersianName",
            "CompanyIsin",
            1000000,
            "SectorCode",
            "SubSectorCode",
            ProductType.Equity,
            ExchangeType.None,
            EtfType.Equity,
            DateTime.UtcNow);
    }

    private static StockOwnership CreateStockOwnership(Guid id, Symbol parentSymbol)
    {
        return new StockOwnership(
            id,
            parentSymbol,
            "Subsidiary",
            50m,
            new SignedCodalMoney(1000000),
            123456,
            "http://example.com",
            DateTime.UtcNow);
    }
}