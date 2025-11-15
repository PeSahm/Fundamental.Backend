using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

/// <summary>
/// Integration tests for InterpretativeReportSummaryPage5 V2 processor.
/// Tests complete processing pipeline: deserialization, mapping, and persistence.
/// </summary>
public class InterpretativeReportSummaryPage5IntegrationTests : FinancialStatementTestBase
{
    public InterpretativeReportSummaryPage5IntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task ProcessInterpretativeReportSummaryPage5V2_ShouldStoreCanonicalData()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        // Act
        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalInterpretativeReportSummaryPage5? storedEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 987654321UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("V2");
        storedEntity.FiscalYear.Year.Should().BeGreaterThan(0);
        storedEntity.ReportMonth.Month.Should().BeInRange(1, 12);
        storedEntity.YearEndMonth.Month.Should().BeInRange(1, 12);
        storedEntity.Currency.Should().Be(IsoCurrency.IRR);
    }

    [Fact]
    public async Task ProcessInterpretativeReportSummaryPage5V2_ShouldMapAllSections()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        // Act
        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify all 7 collection properties are populated
        CanonicalInterpretativeReportSummaryPage5? storedEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 987654321UL);

        storedEntity.Should().NotBeNull();

        // Verify collections are populated (should have data from test JSON)
        storedEntity!.OtherOperatingIncomes.Should().NotBeNullOrEmpty("otherOperatingIncome section should have data");
        storedEntity.OtherNonOperatingExpenses.Should().NotBeNullOrEmpty("otherNonOperatingExpenses section should have data");
        storedEntity.FinancingDetails.Should().NotBeNullOrEmpty("financingDetails section should have data");
        storedEntity.FinancingDetailsEstimated.Should().NotBeNullOrEmpty("financingDetailsEstimated section should have data");
        storedEntity.InvestmentIncomes.Should().NotBeNullOrEmpty("investmentIncome section should have data");
        storedEntity.MiscellaneousExpenses.Should().NotBeNullOrEmpty("miscellaneousItems section should have data");
        storedEntity.Descriptions.Should().NotBeNullOrEmpty("description sections should have data");
    }

    [Fact]
    public async Task ProcessInterpretativeReportSummaryPage5V2_ShouldCorrectlyMapRowCodes()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        // Act
        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify row codes are correctly mapped to enums
        CanonicalInterpretativeReportSummaryPage5? storedEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 987654321UL);

        storedEntity.Should().NotBeNull();

        // Check OtherOperatingIncome row codes
        storedEntity!.OtherOperatingIncomes.Should().Contain(x => x.RowCode == OtherOperatingIncomeRowCode.Data);
        storedEntity.OtherOperatingIncomes.Should().Contain(x => x.RowCode == OtherOperatingIncomeRowCode.Total);

        // Check OtherNonOperatingExpenses row codes
        storedEntity.OtherNonOperatingExpenses.Should().Contain(x => x.RowCode == OtherNonOperatingExpenseRowCode.Data);
        storedEntity.OtherNonOperatingExpenses.Should().Contain(x => x.RowCode == OtherNonOperatingExpenseRowCode.Total);

        // Check FinancingDetails row codes
        storedEntity.FinancingDetails.Should().Contain(x => x.RowCode == FinancingDetailRowCode.BankFacilitiesCurrent);
        storedEntity.FinancingDetails.Should().Contain(x => x.RowCode == FinancingDetailRowCode.TotalCurrent);

        // Check InvestmentIncome row codes
        storedEntity.InvestmentIncomes.Should().Contain(x => x.RowCode == InvestmentIncomeRowCode.Data);
        storedEntity.InvestmentIncomes.Should().Contain(x => x.RowCode == InvestmentIncomeRowCode.Total);

        // Check MiscellaneousExpenses row codes
        storedEntity.MiscellaneousExpenses.Should().Contain(x => x.RowCode == MiscellaneousExpenseRowCode.Data);
        storedEntity.MiscellaneousExpenses.Should().Contain(x => x.RowCode == MiscellaneousExpenseRowCode.Total);
    }

    [Fact]
    public async Task ProcessInterpretativeReportSummaryPage5V2_ShouldExtractCorrectPeriodData()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        // Act
        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify fiscal year and report month are extracted from first yearData
        CanonicalInterpretativeReportSummaryPage5? storedEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 987654321UL);

        storedEntity.Should().NotBeNull();

        // Fiscal year should be extracted from first yearData's periodEndToDate
        storedEntity!.FiscalYear.Year.Should().BeGreaterThan(1400, "fiscal year should be valid Persian year");

        // Report month should match the period from first yearData
        storedEntity.ReportMonth.Month.Should().BeInRange(1, 12);

        // Year-end month should match the year-end date from first yearData
        storedEntity.YearEndMonth.Month.Should().BeInRange(1, 12);
    }

    [Fact]
    public async Task ProcessInterpretativeReportSummaryPage5V2_ShouldHandleUpdate()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        // Act - Process first time
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act - Process second time (update)
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Should only have one record
        int count = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .CountAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 987654321UL);

        count.Should().Be(1, "processor should update existing record, not create duplicate");
    }

    [Fact]
    public void InterpretativeReportSummaryPage5Detector_ShouldDetectV2()
    {
        // Arrange
        string v2Json = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        InterpretativeReportSummaryPage5Detector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(v2Json);

        // Assert
        detectedVersion.Should().Be(
            CodalVersion.V2,
            "V2 JSON (with interpretativeReportSummaryPage5 version='2') should be detected as V2");
    }

    [Fact]
    public void InterpretativeReportSummaryPage5Detector_ShouldReturnNoneForInvalidVersion()
    {
        // Arrange
        string invalidJson = "{\"interpretativeReportSummaryPage5\": {\"version\": \"99\"}}";
        InterpretativeReportSummaryPage5Detector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(invalidJson);

        // Assert
        detectedVersion.Should().Be(
            CodalVersion.None,
            "JSON with invalid version should return None");
    }

    [Fact]
    public void InterpretativeReportSummaryPage5Detector_ShouldReturnNoneForMissingSection()
    {
        // Arrange
        string invalidJson = "{\"someOtherSection\": {}}";
        InterpretativeReportSummaryPage5Detector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(invalidJson);

        // Assert
        detectedVersion.Should().Be(
            CodalVersion.None,
            "JSON without interpretativeReportSummaryPage5 section should return None");
    }

    /// <summary>
    /// Cleans all existing InterpretativeReportSummaryPage5 data from the database.
    /// </summary>
    private async Task CleanInterpretativeReportSummaryPage5Data()
    {
        _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s.RemoveRange(_fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1SEPP0001")
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
