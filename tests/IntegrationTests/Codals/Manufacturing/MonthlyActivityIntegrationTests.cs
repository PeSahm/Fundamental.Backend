using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

public class MonthlyActivityIntegrationTests : FinancialStatementTestBase
{
    public MonthlyActivityIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task ProcessMonthlyActivityV5_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();

        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.ProductionAndSalesItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("V5");
        storedEntity.FiscalYear.Year.Should().Be(1404);
        storedEntity.ReportMonth.Month.Should().Be(7);
        storedEntity.Currency.Should().Be(IsoCurrency.IRR);
        storedEntity.HasSubCompanySale.Should().BeFalse();

        // Verify collections are populated
        storedEntity.ProductionAndSalesItems.Should().NotBeEmpty();
        storedEntity.BuyRawMaterialItems.Should().NotBeEmpty();
        storedEntity.EnergyItems.Should().NotBeEmpty();
        storedEntity.CurrencyExchangeItems.Should().NotBeEmpty();
        storedEntity.Descriptions.Should().NotBeEmpty();

        // Verify raw JSON is stored
        RawMonthlyActivityJson? rawJsonEntity = await _fixture.DbContext.RawMonthlyActivityJsons
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789L);
        rawJsonEntity.Should().NotBeNull();
        rawJsonEntity!.RawJson.Should().NotBeNullOrEmpty();
        rawJsonEntity.Version.Should().Be("5");
    }

    [Fact]
    public async Task ProcessMonthlyActivityV5_ShouldCorrectlyMapRowCodeAndCategory()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.ProductionAndSalesItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();

        // Verify data rows have RowCode = Data
        var dataRows = storedEntity!.GetProductionAndSalesDataRows().ToList();
        dataRows.Should().NotBeEmpty();
        dataRows.Should().AllSatisfy(x =>
        {
            x.IsDataRow.Should().BeTrue();
            x.IsSummaryRow.Should().BeFalse();
        });

        // Verify summary rows exist
        var summaryRows = storedEntity.GetAllSummaryRows().ToList();
        summaryRows.Should().NotBeEmpty();
        summaryRows.Should().AllSatisfy(x =>
        {
            x.IsDataRow.Should().BeFalse();
            x.IsSummaryRow.Should().BeTrue();
        });

        // Verify total sum exists (green box)
        var totalSummary = storedEntity.GetTotalSummary();
        totalSummary.Should().NotBeNull();
        totalSummary!.ProductName.Should().Be("جمع");
        totalSummary.YearToDateSalesAmount.Should().BeGreaterThan(0);

        // Verify internal sale summary exists (blue box)
        var internalSummary = storedEntity.GetInternalSaleSummary();
        internalSummary.Should().NotBeNull();
        internalSummary!.ProductName.Should().Be("جمع فروش داخلی");

        // Verify export sale summary exists (red box)
        var exportSummary = storedEntity.GetExportSaleSummary();
        exportSummary.Should().NotBeNull();
        exportSummary!.ProductName.Should().Be("جمع فروش صادراتی");
    }

    [Fact]
    public async Task GetInternalSaleDataRows_ShouldReturnOnlyInternalProducts()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.ProductionAndSalesItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var internalProducts = storedEntity!.GetInternalSaleDataRows().ToList();

        // Assert
        internalProducts.Should().NotBeEmpty();
        internalProducts.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(ProductionSalesCategory.Internal);
            x.IsDataRow.Should().BeTrue();
        });

        // Verify some known internal products from test data
        internalProducts.Should().Contain(x => x.ProductName.Contains("کلینکر"));
        internalProducts.Should().Contain(x => x.ProductName.Contains("محصولات بتنی"));
        internalProducts.Should().Contain(x => x.ProductName.Contains("سیمان فله تیپ2(داخلی"));
    }

    [Fact]
    public async Task GetExportSaleDataRows_ShouldReturnOnlyExportProducts()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.ProductionAndSalesItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var exportProducts = storedEntity!.GetExportSaleDataRows().ToList();

        // Assert
        exportProducts.Should().NotBeEmpty();
        exportProducts.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(ProductionSalesCategory.Export);
            x.IsDataRow.Should().BeTrue();
        });

        // Verify some known export products from test data
        exportProducts.Should().Contain(x => x.ProductName.Contains("سیمان فله تیپ2(صادراتی"));
        exportProducts.Should().Contain(x => x.ProductName.Contains("سیمان پاکتی تیپ 2(صادراتی"));
    }

    [Fact]
    public async Task ProcessMonthlyActivityV4_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0002", 9600060UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV4TestData();

        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV4Processor processor = new MonthlyActivityV4Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0002", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("4");
        storedEntity.FiscalYear.Year.Should().Be(1402);
        storedEntity.ReportMonth.Month.Should().Be(1);

        // V4 should have production and sales, but not energy or currency exchange
        storedEntity.ProductionAndSalesItems.Should().NotBeEmpty();
        storedEntity.BuyRawMaterialItems.Should().BeEmpty();
        storedEntity.EnergyItems.Should().BeEmpty();
        storedEntity.CurrencyExchangeItems.Should().BeEmpty();
        storedEntity.Descriptions.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ProcessMonthlyActivityV3_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0003", 9600061UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV3TestData();

        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV3Processor processor = new MonthlyActivityV3Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0003", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("3");
        storedEntity.FiscalYear.Year.Should().Be(1401);
        storedEntity.ReportMonth.Month.Should().Be(1);

        // V3 should have production and sales, descriptions, but not energy or currency exchange
        storedEntity.ProductionAndSalesItems.Should().NotBeEmpty();
        storedEntity.BuyRawMaterialItems.Should().BeEmpty();
        storedEntity.EnergyItems.Should().BeEmpty();
        storedEntity.CurrencyExchangeItems.Should().BeEmpty();
        storedEntity.Descriptions.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ProcessMonthlyActivityV2_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0004", 9600062UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV2TestData();

        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV2Processor processor = new MonthlyActivityV2Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0004", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("2");
        storedEntity.FiscalYear.Year.Should().Be(1399);
        storedEntity.ReportMonth.Month.Should().Be(1);

        // V2 should have basic production and sales and descriptions
        storedEntity.ProductionAndSalesItems.Should().NotBeEmpty();
        storedEntity.BuyRawMaterialItems.Should().BeEmpty();
        storedEntity.EnergyItems.Should().BeEmpty();
        storedEntity.CurrencyExchangeItems.Should().BeEmpty();
        storedEntity.Descriptions.Should().BeEmpty(); // V2 test data has empty descriptions
    }

    [Fact]
    public async Task ProcessMonthlyActivityV1_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0005", 9600063UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV1TestData();

        SetupApiResponse("monthly-activity", testJson);

        // Act
        MonthlyActivityV1Processor processor = new MonthlyActivityV1Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0005", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("1");
        storedEntity.FiscalYear.Year.Should().Be(1398);
        storedEntity.ReportMonth.Month.Should().Be(1);

        // V1 should have minimal data
        storedEntity.ProductionAndSalesItems.Should().NotBeEmpty();
        storedEntity.BuyRawMaterialItems.Should().BeEmpty();
        storedEntity.EnergyItems.Should().BeEmpty();
        storedEntity.CurrencyExchangeItems.Should().BeEmpty();
        storedEntity.Descriptions.Should().BeEmpty();
    }

    [Fact]
    public async Task ProcessMonthlyActivity_WithApiFailure_ShouldNotStoreData()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0006", 9600064UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Setup API to return 500 error
        _fixture.WireMockServer.Given(Request.Create().WithPath("/monthly-activity").UsingGet())
            .RespondWith(WireMockResponse.Create().WithStatusCode(HttpStatusCode.InternalServerError));

        // Act
        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0006", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse("{}");

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - No data should be stored
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().BeNull();

        RawMonthlyActivityJson? rawJsonEntity = await _fixture.DbContext.RawMonthlyActivityJsons
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789L);

        rawJsonEntity.Should().BeNull();
    }

    [Fact]
    public async Task GetDomesticRawMaterials_ShouldReturnOnlyDomesticMaterials()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var domesticMaterials = storedEntity!.GetDomesticRawMaterials().ToList();

        // Assert
        domesticMaterials.Should().NotBeEmpty();
        domesticMaterials.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(BuyRawMaterialCategory.Domestic);
            x.MaterialName.Should().NotBeNullOrEmpty();
        });

        // Verify some known domestic materials from test data
        domesticMaterials.Should().Contain(x => x.MaterialName.Contains("آهن"));
    }

    [Fact]
    public async Task GetImportedRawMaterials_ShouldReturnOnlyImportedMaterials()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var importedMaterials = storedEntity!.GetImportedRawMaterials().ToList();

        // Assert
        importedMaterials.Should().NotBeEmpty();
        importedMaterials.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(BuyRawMaterialCategory.Imported);
            x.MaterialName.Should().NotBeNullOrEmpty();
        });
    }

    [Fact]
    public async Task GetTotalRawMaterials_ShouldReturnTotalSummaryRows()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var totalMaterials = storedEntity!.GetTotalRawMaterials().ToList();

        // Assert
        totalMaterials.Should().NotBeEmpty();
        totalMaterials.Should().AllSatisfy(x =>
        {
            x.Category.Should().Be(BuyRawMaterialCategory.Total);
        });

        // Total rows should have aggregated amounts
        var firstTotal = totalMaterials.FirstOrDefault();
        if (firstTotal != null)
        {
            // Total amount should be sum of domestic + imported
            firstTotal.YearToDateAmount.Should().BeGreaterThanOrEqualTo(0);
        }
    }

    [Fact]
    public async Task GetRawMaterialDataRows_ShouldExcludeSummaryRows()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var dataRows = storedEntity!.GetRawMaterialDataRows().ToList();

        // Assert
        dataRows.Should().NotBeEmpty();
        dataRows.Should().AllSatisfy(x =>
        {
            x.IsDataRow.Should().BeTrue();
            x.IsSummaryRow.Should().BeFalse();
            x.RowCode.Should().Be(BuyRawMaterialRowCode.Data);
        });

        // Verify data rows are actual material entries, not totals
        var domesticData = dataRows.Where(x => x.Category == BuyRawMaterialCategory.Domestic).ToList();
        var importedData = dataRows.Where(x => x.Category == BuyRawMaterialCategory.Imported).ToList();

        (domesticData.Count + importedData.Count).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetDomesticRawMaterialsSum_ShouldReturnDomesticSummaryRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var domesticSum = storedEntity!.GetDomesticRawMaterialsSum();

        // Assert
        domesticSum.Should().NotBeNull();
        domesticSum!.RowCode.Should().Be(BuyRawMaterialRowCode.DomesticSum);
        domesticSum.Category.Should().Be(BuyRawMaterialCategory.Domestic);
        domesticSum.MaterialName.Should().Contain("جمع مواد اولیه داخلی");
        domesticSum.IsSummaryRow.Should().BeTrue();
        domesticSum.IsDataRow.Should().BeFalse();

        // Verify it has aggregated amounts
        domesticSum.YearToDateAmount.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetImportedRawMaterialsSum_ShouldReturnImportedSummaryRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var importedSum = storedEntity!.GetImportedRawMaterialsSum();

        // Assert
        importedSum.Should().NotBeNull();
        importedSum!.RowCode.Should().Be(BuyRawMaterialRowCode.ImportedSum);
        importedSum.Category.Should().Be(BuyRawMaterialCategory.Imported);
        importedSum.MaterialName.Should().Contain("جمع مواد اولیه وارداتی");
        importedSum.IsSummaryRow.Should().BeTrue();
        importedSum.IsDataRow.Should().BeFalse();
    }

    [Fact]
    public async Task GetRawMaterialsTotalSum_ShouldReturnTotalSummaryRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.BuyRawMaterialItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var totalSum = storedEntity!.GetRawMaterialsTotalSum();

        // Assert
        totalSum.Should().NotBeNull();
        totalSum!.RowCode.Should().Be(BuyRawMaterialRowCode.TotalSum);
        totalSum.Category.Should().Be(BuyRawMaterialCategory.Total);
        totalSum.MaterialName.Should().Contain("جمع کل");
        totalSum.IsSummaryRow.Should().BeTrue();
        totalSum.IsDataRow.Should().BeFalse();

        // Verify total equals domestic + imported
        var domesticSum = storedEntity.GetDomesticRawMaterialsSum();
        var importedSum = storedEntity.GetImportedRawMaterialsSum();

        if (domesticSum != null && importedSum != null)
        {
            totalSum.YearToDateAmount.Should().Be(
                (domesticSum.YearToDateAmount ?? 0) + (importedSum.YearToDateAmount ?? 0));
        }
    }

    [Fact]
    public async Task GetEnergyDataRows_ShouldExcludeSummaryRows()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.EnergyItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var dataRows = storedEntity!.GetEnergyDataRows().ToList();

        // Assert
        dataRows.Should().NotBeEmpty();
        dataRows.Should().AllSatisfy(x =>
        {
            x.RowCode.Should().Be(EnergyRowCode.Data);
            x.IsDataRow.Should().BeTrue();
            x.IsSummaryRow.Should().BeFalse();
            x.EnergyType.Should().NotBeNullOrEmpty();
        });
    }

    [Fact]
    public async Task GetEnergyTotalSum_ShouldReturnTotalSummaryRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.EnergyItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var totalSum = storedEntity!.GetEnergyTotalSum();

        // Assert
        totalSum.Should().NotBeNull();
        totalSum!.RowCode.Should().Be(EnergyRowCode.TotalSum);
        totalSum.Category.Should().Be(0);
        totalSum.Industry.Should().Contain("جمع کل");
        totalSum.IsSummaryRow.Should().BeTrue();
        totalSum.IsDataRow.Should().BeFalse();
        totalSum.YearToDateCost.Should().BeGreaterThan(0);

        // Verify total matches sum of all data rows (within rounding tolerance)
        var dataRows = storedEntity.GetEnergyDataRows().ToList();
        var calculatedTotal = dataRows.Sum(x => x.YearToDateCost ?? 0);
        
        if (totalSum.YearToDateCost.HasValue && calculatedTotal > 0)
        {
            // Allow small rounding differences
            totalSum.YearToDateCost.Value.Should().BeApproximately(calculatedTotal, 1);
        }
    }

    [Fact]
    public async Task ProcessMonthlyActivityV5_ShouldCorrectlyMapEnergyRowCodeAndCategory()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.EnergyItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        // Assert
        storedEntity.Should().NotBeNull();
        storedEntity!.EnergyItems.Should().NotBeEmpty();

        // Check data rows
        var dataRows = storedEntity.EnergyItems.Where(x => x.IsDataRow).ToList();
        dataRows.Should().HaveCountGreaterThan(0);
        dataRows.Should().AllSatisfy(x =>
        {
            x.RowCode.Should().Be(EnergyRowCode.Data);
            x.Category.Should().Be(0);
            x.EnergyType.Should().NotBeNullOrEmpty();
        });

        // Check sum row
        var sumRow = storedEntity.EnergyItems.FirstOrDefault(x => x.IsSummaryRow);
        if (sumRow != null)
        {
            sumRow.RowCode.Should().Be(EnergyRowCode.TotalSum);
            sumRow.Category.Should().Be(0);
            sumRow.Industry.Should().Contain("جمع");
        }
    }

    [Fact]
    public async Task GetCurrencySourcesDataRows_ShouldReturnOnlySourcesDataRows()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.CurrencyExchangeItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var sourcesData = storedEntity!.GetCurrencySourcesDataRows().ToList();

        // Assert
        sourcesData.Should().NotBeEmpty();
        sourcesData.Should().AllSatisfy(x =>
        {
            x.IsDataRow.Should().BeTrue();
            x.IsSummaryRow.Should().BeFalse();
            x.RowCode.Should().Be(CurrencyExchangeRowCode.Data);
            x.Category.Should().Be(CurrencyExchangeCategory.Sources);
            x.IsSourcesRow.Should().BeTrue();
            x.IsUsesRow.Should().BeFalse();
            x.Description.Should().NotBeNullOrEmpty();
        });
    }

    [Fact]
    public async Task GetCurrencyUsesDataRows_ShouldReturnOnlyUsesDataRows()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.CurrencyExchangeItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var usesData = storedEntity!.GetCurrencyUsesDataRows().ToList();

        // Assert
        // Note: In the test data (Sorood-.1404-07-30.json), there are no actual uses data rows (RowCode=-1, Category=Uses)
        // Only "سایر" fixed rows exist which are not data rows (RowCode=32 or 36)
        // So we validate the filter works correctly by checking it returns empty as expected
        usesData.Should().BeEmpty("test data contains no currency uses data rows, only fixed 'سایر' rows");
    }

    [Fact]
    public async Task GetCurrencySourcesSum_ShouldReturnSourcesTotalRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.CurrencyExchangeItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var sourcesSum = storedEntity!.GetCurrencySourcesSum();

        // Assert
        sourcesSum.Should().NotBeNull();
        sourcesSum!.RowCode.Should().Be(CurrencyExchangeRowCode.SourcesSum);
        sourcesSum.Category.Should().Be(CurrencyExchangeCategory.Total);
        sourcesSum.Description.Should().Contain("جمع منابع ارزی");
        sourcesSum.IsSummaryRow.Should().BeTrue();
        sourcesSum.IsDataRow.Should().BeFalse();

        // Verify it has aggregated amounts matching the test data
        sourcesSum.YearToDateForeignAmount.Should().Be(16708843);
        sourcesSum.YearToDateRialAmount.Should().Be(11963932);
    }

    [Fact]
    public async Task GetCurrencyUsesSum_ShouldReturnUsesTotalRow()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.CurrencyExchangeItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var usesSum = storedEntity!.GetCurrencyUsesSum();

        // Assert
        usesSum.Should().NotBeNull();
        usesSum!.RowCode.Should().Be(CurrencyExchangeRowCode.UsesSum);
        usesSum.Category.Should().Be(CurrencyExchangeCategory.Total);
        usesSum.Description.Should().Contain("جمع مصارف ارزی");
        usesSum.IsSummaryRow.Should().BeTrue();
        usesSum.IsDataRow.Should().BeFalse();

        // In test data, uses are all zero
        usesSum.YearToDateForeignAmount.Should().Be(0);
        usesSum.YearToDateRialAmount.Should().Be(0);
    }

    [Fact]
    public async Task ProcessMonthlyActivityV5_ShouldCorrectlyMapCurrencyExchangeRowCodeAndCategory()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act
        CanonicalMonthlyActivity? storedEntity = await _fixture.DbContext.CanonicalMonthlyActivities
            .Include(x => x.CurrencyExchangeItems)
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id);

        var allItems = storedEntity!.CurrencyExchangeItems.ToList();
        var dataRows = allItems.Where(x => x.RowCode == CurrencyExchangeRowCode.Data).ToList();
        var sourcesSum = allItems.FirstOrDefault(x => x.RowCode == CurrencyExchangeRowCode.SourcesSum);
        var usesSum = allItems.FirstOrDefault(x => x.RowCode == CurrencyExchangeRowCode.UsesSum);

        // Assert
        allItems.Should().NotBeEmpty();

        // Validate data rows have correct enum values
        dataRows.Should().NotBeEmpty();
        dataRows.Should().AllSatisfy(x =>
        {
            x.RowCode.Should().Be(CurrencyExchangeRowCode.Data);
            x.Category.Should().BeOneOf(CurrencyExchangeCategory.Sources, CurrencyExchangeCategory.Uses);
        });

        // Validate sources sum row
        sourcesSum.Should().NotBeNull();
        sourcesSum!.RowCode.Should().Be(CurrencyExchangeRowCode.SourcesSum);
        sourcesSum.Category.Should().Be(CurrencyExchangeCategory.Total);

        // Validate uses sum row
        usesSum.Should().NotBeNull();
        usesSum!.RowCode.Should().Be(CurrencyExchangeRowCode.UsesSum);
        usesSum.Category.Should().Be(CurrencyExchangeCategory.Total);
    }

    [Fact]
    public async Task GetMonthlyActivities_Query_ShouldReturnPaginatedList()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Process test data to create entities
        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Act - query without filters to get all data
        GetMonthlyActivitiesRequest request = new(new[] { "IRO1SROD0001" }, null, null)
        {
            PageSize = 20,
            PageNumber = 1
        };
        Response<Paginated<GetMonthlyActivitiesResultItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();

        GetMonthlyActivitiesResultItem item = response.Data.Items.First();
        item.Isin.Should().Be("IRO1SROD0001");
        item.ProductionAndSalesItems.Should().NotBeEmpty();
        item.BuyRawMaterialItems.Should().NotBeEmpty();
        item.EnergyItems.Should().NotBeEmpty();
        item.CurrencyExchangeItems.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetMonthlyActivities_WithIsinFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol1 = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        Symbol symbol2 = CreateTestSymbol("IRO1BMLT0001", 9600060UL);
        await _fixture.DbContext.Symbols.AddRangeAsync(symbol1, symbol2);
        await _fixture.DbContext.SaveChangesAsync();

        // Process test data for first symbol only
        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement1 = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);
        await processor.Process(statement1, jsonResponse, CancellationToken.None);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Act - filter by specific ISIN
        GetMonthlyActivitiesRequest request = new(new[] { "IRO1SROD0001" }, null, null)
        {
            PageSize = 20,
            PageNumber = 1
        };
        Response<Paginated<GetMonthlyActivitiesResultItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data!.Items.Should().OnlyContain(x => x.Isin == "IRO1SROD0001");
    }

    [Fact]
    public async Task GetMonthlyActivityById_Query_ShouldReturnSingleEntity()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Process test data
        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Get the entity ID
        CanonicalMonthlyActivity? entity = await _fixture.DbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync();
        entity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Act
        GetMonthlyActivityByIdRequest request = new(entity!.Id);
        Response<GetMonthlyActivitiesResultItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(entity.Id);
        response.Data.Isin.Should().Be("IRO1SROD0001");
        response.Data.ProductionAndSalesItems.Should().NotBeEmpty();
        response.Data.BuyRawMaterialItems.Should().NotBeEmpty();
        response.Data.EnergyItems.Should().NotBeEmpty();
        response.Data.CurrencyExchangeItems.Should().NotBeEmpty();
        response.Data.Descriptions.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMonthlyActivityById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        Guid nonExistentId = Guid.NewGuid();
        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Act
        GetMonthlyActivityByIdRequest request = new(nonExistentId);
        Response<GetMonthlyActivitiesResultItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMonthlyActivities_ShouldIncludeAllCollections()
    {
        // Arrange
        await CleanMonthlyActivityData();
        Symbol symbol = CreateTestSymbol("IRO1SROD0001", 9600059UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = MonthlyActivityTestData.GetV5TestData();
        SetupApiResponse("monthly-activity", testJson);

        MonthlyActivityV5Processor processor = new MonthlyActivityV5Processor(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SROD0001", 123456789);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Act
        GetMonthlyActivitiesRequest request = new(Array.Empty<string>(), null, null)
        {
            PageSize = 20,
            PageNumber = 1
        };
        Response<Paginated<GetMonthlyActivitiesResultItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        GetMonthlyActivitiesResultItem item = response.Data!.Items.First();

        // Verify all collections are loaded and not empty
        item.ProductionAndSalesItems.Should().NotBeEmpty("ProductionAndSalesItems collection should be loaded");
        item.BuyRawMaterialItems.Should().NotBeEmpty("BuyRawMaterialItems collection should be loaded");
        item.EnergyItems.Should().NotBeEmpty("EnergyItems collection should be loaded");
        item.CurrencyExchangeItems.Should().NotBeEmpty("CurrencyExchangeItems collection should be loaded");

        // Verify collection items have expected properties
        item.ProductionAndSalesItems.First().Should().Match<ProductionAndSalesItem>(x =>
            x.RowCode != default && x.Category != default);
        item.BuyRawMaterialItems.First().Should().Match<BuyRawMaterialItem>(x =>
            x.RowCode != default && x.Category != default);
        item.EnergyItems.First().Should().Match<EnergyItem>(x =>
            x.RowCode != default);
        item.CurrencyExchangeItems.First().Should().Match<CurrencyExchangeItem>(x =>
            x.RowCode != default && x.Category != default);
    }

    /// <summary>
    /// Cleans all existing monthly activity data from the database.
    /// </summary>
    protected async Task CleanMonthlyActivityData()
    {
        _fixture.DbContext.CanonicalMonthlyActivities.RemoveRange(_fixture.DbContext.CanonicalMonthlyActivities);
        _fixture.DbContext.RawMonthlyActivityJsons.RemoveRange(_fixture.DbContext.RawMonthlyActivityJsons);
        
        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1SROD0001" || s.Isin == "IRO1BMLT0001")
            .ToListAsync();
        _fixture.DbContext.Symbols.RemoveRange(testSymbols);
        
        await _fixture.DbContext.SaveChangesAsync();
    }

    private void SetupApiResponse(string endpoint, string responseJson)
    {
        _fixture.WireMockServer.Given(Request.Create().WithPath($"/{endpoint}").UsingGet())
            .RespondWith(WireMockResponse.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithBody(responseJson));
    }
}