using FluentAssertions;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Xunit;

namespace Domain.UnitTests.Codals.Manufacturing;

public class CanonicalMonthlyActivityTests
{
    [Fact]
    public void GetProductionAndSalesDataRows_ShouldReturnOnlyDataRows()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        List<ProductionAndSalesItem> dataRows = monthlyActivity.GetProductionAndSalesDataRows().ToList();

        // Assert
        dataRows.Should().HaveCount(4); // 2 internal + 2 export products
        dataRows.Should().AllSatisfy(x =>
        {
            x.RowCode.Should().Be(ProductionSalesRowCode.Data);
            x.IsDataRow.Should().BeTrue();
            x.IsSummaryRow.Should().BeFalse();
        });
    }

    [Fact]
    public void GetInternalSaleDataRows_ShouldReturnOnlyInternalProducts()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        List<ProductionAndSalesItem> internalProducts = monthlyActivity.GetInternalSaleDataRows().ToList();

        // Assert
        internalProducts.Should().HaveCount(2);
        internalProducts.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(ProductionSalesCategory.Internal);
            x.RowCode.Should().Be(ProductionSalesRowCode.Data);
        });
    }

    [Fact]
    public void GetExportSaleDataRows_ShouldReturnOnlyExportProducts()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        List<ProductionAndSalesItem> exportProducts = monthlyActivity.GetExportSaleDataRows().ToList();

        // Assert
        exportProducts.Should().HaveCount(2);
        exportProducts.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(ProductionSalesCategory.Export);
            x.RowCode.Should().Be(ProductionSalesRowCode.Data);
        });
    }

    [Fact]
    public void GetInternalSaleSummary_ShouldReturnInternalSummaryRow()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        ProductionAndSalesItem? internalSummary = monthlyActivity.GetInternalSaleSummary();

        // Assert
        internalSummary.Should().NotBeNull();
        internalSummary!.RowCode.Should().Be(ProductionSalesRowCode.InternalSale);
        internalSummary.Category.Should().Be(ProductionSalesCategory.Sum);
        internalSummary.ProductName.Should().Be("جمع فروش داخلی");
        internalSummary.YearToDateSalesAmount.Should().Be(10000);
    }

    [Fact]
    public void GetExportSaleSummary_ShouldReturnExportSummaryRow()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        ProductionAndSalesItem? exportSummary = monthlyActivity.GetExportSaleSummary();

        // Assert
        exportSummary.Should().NotBeNull();
        exportSummary!.RowCode.Should().Be(ProductionSalesRowCode.ExportSale);
        exportSummary.Category.Should().Be(ProductionSalesCategory.Sum);
        exportSummary.ProductName.Should().Be("جمع فروش صادراتی");
        exportSummary.YearToDateSalesAmount.Should().Be(20000);
    }

    [Fact]
    public void GetTotalSummary_ShouldReturnGrandTotalRow()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        ProductionAndSalesItem? totalSummary = monthlyActivity.GetTotalSummary();

        // Assert
        totalSummary.Should().NotBeNull();
        totalSummary!.RowCode.Should().Be(ProductionSalesRowCode.TotalSum);
        totalSummary.Category.Should().Be(ProductionSalesCategory.Sum);
        totalSummary.ProductName.Should().Be("جمع");
        totalSummary.YearToDateSalesAmount.Should().Be(30000);
    }

    [Fact]
    public void GetAllSummaryRows_ShouldReturnOnlySummaryRows()
    {
        // Arrange
        CanonicalMonthlyActivity monthlyActivity = CreateTestMonthlyActivity();

        // Act
        List<ProductionAndSalesItem> summaryRows = monthlyActivity.GetAllSummaryRows().ToList();

        // Assert
        summaryRows.Should().HaveCount(3); // Internal, Export, Total
        summaryRows.Should().AllSatisfy(x =>
        {
            x.IsSummaryRow.Should().BeTrue();
            x.IsDataRow.Should().BeFalse();
        });
    }

    [Fact]
    public void IsDataRow_ShouldReturnTrueForDataRows()
    {
        // Arrange
        ProductionAndSalesItem dataRow = new()
        {
            RowCode = ProductionSalesRowCode.Data,
            ProductName = "Test Product"
        };

        // Act & Assert
        dataRow.IsDataRow.Should().BeTrue();
        dataRow.IsSummaryRow.Should().BeFalse();
    }

    [Fact]
    public void IsSummaryRow_ShouldReturnTrueForSummaryRows()
    {
        // Arrange
        ProductionAndSalesItem summaryRow = new()
        {
            RowCode = ProductionSalesRowCode.TotalSum,
            Category = ProductionSalesCategory.Sum,
            ProductName = "جمع"
        };

        // Act & Assert
        summaryRow.IsSummaryRow.Should().BeTrue();
        summaryRow.IsDataRow.Should().BeFalse();
    }

    private static CanonicalMonthlyActivity CreateTestMonthlyActivity()
    {
        var symbol = new Symbol(
            Guid.NewGuid(),
            "ISIN12345678",
            "TSE123",
            "Test Company",
            "TST",
            "Test Company Title",
            "Test Company",
            "TST",
            "شرکت تست",
            null,
            1000000,
            "01",
            "001",
            ProductType.Equity,
            ExchangeType.TSE,
            null,
            DateTime.UtcNow);

        var monthlyActivity = new CanonicalMonthlyActivity(
            Guid.NewGuid(),
            symbol,
            123456,
            "http://test.codal.ir",
            new FiscalYear(1403),
            new StatementMonth(12),
            new StatementMonth(9),
            DateTime.UtcNow,
            "5");

        monthlyActivity.ProductionAndSalesItems = new List<ProductionAndSalesItem>
        {
            // Internal products (data rows)
            new()
            {
                ProductName = "کلینکر",
                RowCode = ProductionSalesRowCode.Data,
                Category = ProductionSalesCategory.Internal,
                YearToDateSalesAmount = 5000
            },
            new()
            {
                ProductName = "سیمان",
                RowCode = ProductionSalesRowCode.Data,
                Category = ProductionSalesCategory.Internal,
                YearToDateSalesAmount = 5000
            },

            // Export products (data rows)
            new()
            {
                ProductName = "سیمان صادراتی",
                RowCode = ProductionSalesRowCode.Data,
                Category = ProductionSalesCategory.Export,
                YearToDateSalesAmount = 10000
            },
            new()
            {
                ProductName = "کلینکر صادراتی",
                RowCode = ProductionSalesRowCode.Data,
                Category = ProductionSalesCategory.Export,
                YearToDateSalesAmount = 10000
            },

            // Internal sale summary (blue box)
            new()
            {
                ProductName = "جمع فروش داخلی",
                RowCode = ProductionSalesRowCode.InternalSale,
                Category = ProductionSalesCategory.Sum,
                YearToDateSalesAmount = 10000
            },

            // Export sale summary (red box)
            new()
            {
                ProductName = "جمع فروش صادراتی",
                RowCode = ProductionSalesRowCode.ExportSale,
                Category = ProductionSalesCategory.Sum,
                YearToDateSalesAmount = 20000
            },

            // Total summary (green box)
            new()
            {
                ProductName = "جمع",
                RowCode = ProductionSalesRowCode.TotalSum,
                Category = ProductionSalesCategory.Sum,
                YearToDateSalesAmount = 30000
            }
        };

        return monthlyActivity;
    }
}