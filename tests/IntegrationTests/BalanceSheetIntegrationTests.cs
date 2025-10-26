using FluentAssertions;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Symbols.Entities;
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

            savedBalanceSheets.Should().NotBeEmpty($"Balance sheet data should be saved for ISIN {isin}");
            savedBalanceSheets.Count.Should()
                .Be(expectedResults.Count, $"Should have {expectedResults.Count} balance sheet entries for ISIN {isin}");

            // Verify each expected result
            foreach (BalanceSheetTestData.BalanceSheetExpectation expected in expectedResults)
            {
                BalanceSheet actual = savedBalanceSheets.Single(bs =>
                    bs.CodalRow == expected.CodalRow &&
                    bs.FiscalYear.Year == expected.FiscalYear &&
                    bs.CodalCategory == expected.CodalCategory &&
                    bs.ReportMonth.Month == expected.ReportMonth);

                actual.Should()
                    .NotBeNull(
                        $"Balance sheet entry should exist for CodalRow {expected.CodalRow}, FiscalYear {expected.FiscalYear}, ReportMonth {expected.ReportMonth}");

                actual.Description.Should().Be(expected.Description, $"Description should match for CodalRow {expected.CodalRow}");
                actual.Value.RealValue.Should().Be(expected.Value, $"Value should match for CodalRow {expected.CodalRow}");
                actual.CodalCategory.Should().Be(expected.CodalCategory, $"Category should match for CodalRow {expected.CodalRow}");
                actual.IsAudited.Should().Be(expected.IsAudited, $"IsAudited should match for CodalRow {expected.CodalRow}");
                actual.Symbol.Isin.Should().Be(isin, $"Symbol ISIN should match");
                actual.TraceNo.Should().Be(traceNo, $"Trace number should match");
            }
        }
    }
}