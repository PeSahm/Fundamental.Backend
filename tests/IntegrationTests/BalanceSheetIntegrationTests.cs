using FluentAssertions;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IntegrationTests.Shared;

namespace IntegrationTests;

/// <summary>
/// Integration tests for balance sheet processing functionality.
/// </summary>
public class BalanceSheetIntegrationTests : FinancialStatementTestBase
{
    public BalanceSheetIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task BalanceSheetV5Processing_ShouldProcessDataCorrectly()
    {
        // Get the test data directory (relative to the test project source)
        string testDataDirectory = GetTestDataDirectory(Path.Combine("Data", "BalanceSheets", "V5"));

        // Find all ISIN folders
        string[] isinFolders = GetIsinFolders(testDataDirectory);

        foreach (string isinFolder in isinFolders)
        {
            string isin = Path.GetFileName(isinFolder);
            (string jsonFilePath, string csvFilePath) = GetTestFiles(isinFolder, isin);

            // Read and parse JSON file
            string balanceSheetJson = await File.ReadAllTextAsync(jsonFilePath);

            // Read and parse CSV file for expected results
            List<BalanceSheetTestData.BalanceSheetExpectation> expectedResults =
                await BalanceSheetTestData.ParseCsvExpectations(csvFilePath);

            // Extract symbol information from JSON
            ulong authorizedCapital = ExtractAuthorizedCapital(balanceSheetJson);

            // Extract trace number from the first balance sheet entry
            ulong traceNo = expectedResults[0].TraceNo;

            // Create symbol
            Symbol symbol = CreateTestSymbol(isin, authorizedCapital, expectedResults[0].SymbolId.ToString());

            _fixture.DbContext.Symbols.Add(symbol);
            await _fixture.DbContext.SaveChangesAsync();

            // Setup WireMock to return the balance sheet data
            SetupBalanceSheetApiResponse(balanceSheetJson);

            // Clean any existing balance sheet data
            await CleanBalanceSheetData();

            // Act - Process the balance sheet data using BalanceSheetV5Processor
            BalanceSheetV5Processor processor = new BalanceSheetV5Processor(
                _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
                _fixture.Services.GetRequiredService<ILogger<BalanceSheetV5Processor>>());

            GetStatementResponse statement = CreateStatementResponse(isin, traceNo);
            GetStatementJsonResponse jsonResponse = CreateJsonResponse(balanceSheetJson);

            await processor.Process(statement, jsonResponse, CancellationToken.None);

            // Assert - Verify balance sheet data was saved correctly
            List<BalanceSheet> savedBalanceSheets = await _fixture.DbContext.BalanceSheets
                .Where(bs => bs.Symbol.Isin == isin && bs.TraceNo == traceNo)
                .ToListAsync();

            savedBalanceSheets.Should().HaveCount(1, $"Should have 1 balance sheet header for ISIN {isin}");

            BalanceSheet balanceSheet = savedBalanceSheets[0];
            balanceSheet.Symbol.Isin.Should().Be(isin);
            balanceSheet.TraceNo.Should().Be(traceNo);

            List<BalanceSheetDetail> savedDetails = await _fixture.DbContext.BalanceSheetDetails
                .Where(d => d.BalanceSheet.Id == balanceSheet.Id)
                .ToListAsync();

            savedDetails.Should().HaveCount(expectedResults.Count, $"Should have {expectedResults.Count} balance sheet details for ISIN {isin}");

            // Verify each expected result
            foreach (BalanceSheetTestData.BalanceSheetExpectation expected in expectedResults)
            {
                BalanceSheetDetail actual = savedDetails.Single(d =>
                    d.CodalRow == expected.CodalRow &&
                    d.BalanceSheet.FiscalYear.Year == expected.FiscalYear &&
                    d.CodalCategory == expected.CodalCategory &&
                    d.BalanceSheet.ReportMonth.Month == expected.ReportMonth);

                actual.Should()
                    .NotBeNull(
                        $"Balance sheet detail should exist for CodalRow {expected.CodalRow}, FiscalYear {expected.FiscalYear}, ReportMonth {expected.ReportMonth}");

                actual.Description.Should().Be(expected.Description, $"Description should match for CodalRow {expected.CodalRow}");
                actual.Value.RealValue.Should().Be(expected.Value, $"Value should match for CodalRow {expected.CodalRow}");
                actual.CodalCategory.Should().Be(expected.CodalCategory, $"Category should match for CodalRow {expected.CodalRow}");
                actual.BalanceSheet.IsAudited.Should().Be(expected.IsAudited, $"IsAudited should match for CodalRow {expected.CodalRow}");
                actual.BalanceSheet.Symbol.Isin.Should().Be(isin, $"Symbol ISIN should match");
                actual.BalanceSheet.TraceNo.Should().Be(traceNo, $"Trace number should match");
            }
        }
    }

    [Fact]
    public async Task GetBalanceSheetQueryHandler_ShouldReturnBalanceSheetsFromDatabase()
    {
        // Arrange
        await CleanBalanceSheetData();

        // Create test symbols
        Symbol testSymbol1 = CreateTestSymbol("IRO1TEST0001", 1000000000UL, "TEST1");
        Symbol testSymbol2 = CreateTestSymbol("IRO1TEST0002", 2000000000UL, "TEST2");

        await _fixture.DbContext.Symbols.AddRangeAsync(testSymbol1, testSymbol2);
        await _fixture.DbContext.SaveChangesAsync();

        // Create test balance sheets with different fiscal years/report months to avoid grouping
        DateTime createdAt = DateTime.UtcNow;
        DateTime publishDate1 = DateTime.UtcNow.AddDays(-1);
        DateTime publishDate2 = DateTime.UtcNow; // Later publish date
        DateTime publishDate3 = DateTime.UtcNow.AddDays(-1);
        BalanceSheet balanceSheet1 = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol1,
            123456789UL,
            "http://test.codal.ir/1",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            createdAt,
            publishDate1);

        BalanceSheet balanceSheet2 = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol1,
            123456790UL,
            "http://test.codal.ir/2",
            new FiscalYear(1402), // Same fiscal year
            new StatementMonth(12),
            new StatementMonth(6), // Different report month
            true,
            createdAt,
            publishDate2);

        BalanceSheet balanceSheet3 = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol2,
            123456791UL,
            "http://test.codal.ir/3",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            createdAt,
            publishDate3);

        await _fixture.DbContext.BalanceSheets.AddRangeAsync(balanceSheet1, balanceSheet2, balanceSheet3);
        await _fixture.DbContext.SaveChangesAsync();

        GetBalanceSheetRequest request = new GetBalanceSheetRequest(
            IsinList: new List<string> { "IRO1TEST0001" },
            TraceNo: null,
            FiscalYear: 1402,
            ReportMonth: null);

        GetBalanceSheetQueryHandler handler = new GetBalanceSheetQueryHandler(
            _fixture.Services.GetRequiredService<IBalanceSheetReadRepository>());

        Response<Paginated<GetBalanceSheetResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Items.Should().HaveCount(2);

        GetBalanceSheetResultDto firstItem = result.Data.Items[0];
        firstItem.Isin.Should().Be("IRO1TEST0001");
        firstItem.FiscalYear.Should().Be(1402);
        firstItem.ReportMonth.Should().Be(6); // Ordered by PublishDate desc

        GetBalanceSheetResultDto secondItem = result.Data.Items[1];
        secondItem.Isin.Should().Be("IRO1TEST0001");
        secondItem.FiscalYear.Should().Be(1402);
        secondItem.ReportMonth.Should().Be(12);

        // Test with non-existent ISIN
        GetBalanceSheetRequest emptyRequest = new GetBalanceSheetRequest(
            IsinList: new List<string> { "NONEXISTENT" },
            TraceNo: null,
            FiscalYear: 1402,
            ReportMonth: null);

        Response<Paginated<GetBalanceSheetResultDto>> emptyResult = await handler.Handle(emptyRequest, CancellationToken.None);

        emptyResult.Success.Should().BeTrue();
        emptyResult.Data?.Items.Should().BeEmpty();
    }
}