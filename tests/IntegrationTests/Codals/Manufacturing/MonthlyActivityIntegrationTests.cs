using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

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
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

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

    /// <summary>
    /// Cleans all existing monthly activity data from the database.
    /// </summary>
    protected async Task CleanMonthlyActivityData()
    {
        _fixture.DbContext.CanonicalMonthlyActivities.RemoveRange(_fixture.DbContext.CanonicalMonthlyActivities);
        _fixture.DbContext.RawMonthlyActivityJsons.RemoveRange(_fixture.DbContext.RawMonthlyActivityJsons);
        await _fixture.DbContext.SaveChangesAsync();
    }

    private void SetupApiResponse(string endpoint, string responseJson)
    {
        _fixture.WireMockServer.Given(Request.Create().WithPath($"/{endpoint}").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithBody(responseJson));
    }
}