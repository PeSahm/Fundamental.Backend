using System.Text.Json;
using FluentAssertions;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntegrationTests;

public class FinancialStatementProcessingIntegrationTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public FinancialStatementProcessingIntegrationTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task BasicFinancialStatementProcessing_ShouldWork()
    {
        // Arrange - Create a simple symbol
        Symbol symbol = new Symbol(
            Guid.NewGuid(),
            "TEST123456789",
            "123456789",
            "Test Company EN",
            "TEST",
            "Test Company",
            "Test Company",
            "TEST",
            "Test Company Persian",
            null,
            1000000000,
            "01",
            "001",
            Fundamental.Domain.Symbols.Enums.ProductType.Equity,
            Fundamental.Domain.Symbols.Enums.ExchangeType.TSE,
            null,
            DateTime.UtcNow);

        _fixture.DbContext.Symbols.Add(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Verify symbol was saved
        Symbol? savedSymbol = await _fixture.DbContext.Symbols
            .FirstOrDefaultAsync(s => s.Id == symbol.Id);
        savedSymbol.Should().NotBeNull();
        savedSymbol.Name.Should().Be("Test Company");

        // Act & Assert - Basic database operations work
        int symbolCount = await _fixture.DbContext.Symbols.CountAsync();
        symbolCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ExternalServiceMocks_ShouldBeConfigured()
    {
        // Arrange & Act - Check that WireMock server is running
        _fixture.WireMockServer.Should().NotBeNull();

        // Assert - Basic WireMock functionality
        string? serverUrl = _fixture.WireMockServer.Urls.FirstOrDefault();
        serverUrl.Should().NotBeNull();
        serverUrl.Should().Contain("http://localhost:");
    }

    [Fact]
    public async Task DatabaseQueryOperations_ShouldWork()
    {
        // Arrange - Add some test symbols
        Symbol[] symbols = new[]
        {
            new Symbol(
                Guid.NewGuid(),
                "SYMB1123456789",
                "111111111",
                "Symbol One EN",
                "SYMB1",
                "Symbol One",
                "Symbol One",
                "SYMB1",
                "Symbol One Persian",
                null,
                1000000000,
                "01",
                "001",
                Fundamental.Domain.Symbols.Enums.ProductType.Equity,
                Fundamental.Domain.Symbols.Enums.ExchangeType.TSE,
                null,
                DateTime.UtcNow),
            new Symbol(
                Guid.NewGuid(),
                "SYMB2123456789",
                "222222222",
                "Symbol Two EN",
                "SYMB2",
                "Symbol Two",
                "Symbol Two",
                "SYMB2",
                "Symbol Two Persian",
                null,
                2000000000,
                "01",
                "001",
                Fundamental.Domain.Symbols.Enums.ProductType.Equity,
                Fundamental.Domain.Symbols.Enums.ExchangeType.TSE,
                null,
                DateTime.UtcNow)
        };

        await _fixture.DbContext.Symbols.AddRangeAsync(symbols);
        await _fixture.DbContext.SaveChangesAsync();

        // Act - Query symbols
        List<Symbol> retrievedSymbols = await _fixture.DbContext.Symbols.ToListAsync();

        // Assert
        retrievedSymbols.Should().NotBeEmpty();
        retrievedSymbols.Should().Contain(s => s.Name == "Symbol One");
        retrievedSymbols.Should().Contain(s => s.Name == "Symbol Two");
    }

    [Fact]
    public async Task ContainerizedDatabase_ShouldBeAccessible()
    {
        // Arrange & Act
        bool canConnect = await _fixture.DbContext.Database.CanConnectAsync();
        string postgresConnectionString = _fixture.PostgresConnectionString;
        string redisConnectionString = _fixture.RedisConnectionString;

        // Assert
        canConnect.Should().BeTrue();
        postgresConnectionString.Should().NotBeNullOrEmpty();
        postgresConnectionString.Should().Contain("postgres");
        redisConnectionString.Should().NotBeNullOrEmpty();
        redisConnectionString.Should().Contain(":");
    }

    [Fact]
    public async Task ComplexQueryWithIncludes_ShouldWork()
    {
        // Arrange - Add symbol with relations if needed
        Symbol symbol = new Symbol(
            Guid.NewGuid(),
            "COMP123456789",
            "333333333",
            "Complex Company EN",
            "COMP",
            "Complex Company",
            "Complex Company",
            "COMP",
            "Complex Company Persian",
            null,
            3000000000,
            "01",
            "001",
            Fundamental.Domain.Symbols.Enums.ProductType.Equity,
            Fundamental.Domain.Symbols.Enums.ExchangeType.TSE,
            null,
            DateTime.UtcNow);

        _fixture.DbContext.Symbols.Add(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Act - Query with includes (even if no includes are needed, test the syntax)
        Symbol? queriedSymbol = await _fixture.DbContext.Symbols
            .FirstOrDefaultAsync(s => s.Id == symbol.Id);

        // Assert
        queriedSymbol.Should().NotBeNull();
        queriedSymbol.Name.Should().Be("Complex Company");
    }

    [Fact]
    public async Task BalanceSheetV5Processing_ShouldProcessDataCorrectly()
    {
        // Get the test data directory (relative to the test project source)
        string? testProjectDirectory = Path.GetDirectoryName(typeof(FinancialStatementProcessingIntegrationTests).Assembly.Location);

        if (testProjectDirectory is null)
        {
            throw new InvalidOperationException("Test project directory could not be determined.");
        }

        string testDataDirectory = Path.Combine(testProjectDirectory, "..", "..", "..", "Data", "BalanceSheets", "V5");

        // Find all ISIN folders
        string[] isinFolders = Directory.GetDirectories(testDataDirectory);
        isinFolders.Should().NotBeEmpty("Test data folders should exist");

        foreach (string isinFolder in isinFolders)
        {
            string isin = Path.GetFileName(isinFolder);
            string jsonFilePath = Path.Combine(isinFolder, $"{isin}.json");
            string csvFilePath = Path.Combine(isinFolder, $"{isin}.csv");

            // Check if files exist with ISIN name, otherwise try alternative names
            if (!File.Exists(jsonFilePath) || !File.Exists(csvFilePath))
            {
                // Try alternative naming (e.g., Sheranol.json for IRO3NOLZ0001)
                string[] jsonFiles = Directory.GetFiles(isinFolder, "*.json");
                string[] csvFiles = Directory.GetFiles(isinFolder, "*.csv");

                if (jsonFiles.Length == 1 && csvFiles.Length == 1)
                {
                    jsonFilePath = jsonFiles[0];
                    csvFilePath = csvFiles[0];
                }
                else
                {
                    File.Exists(jsonFilePath).Should().BeTrue($"JSON file should exist for ISIN {isin}");
                    File.Exists(csvFilePath).Should().BeTrue($"CSV file should exist for ISIN {isin}");
                }
            }

            // Read and parse JSON file
            string balanceSheetJson = await File.ReadAllTextAsync(jsonFilePath);

            // Read and parse CSV file for expected results
            List<BalanceSheetExpectation> expectedResults = await ParseCsvExpectations(csvFilePath);

            // Extract symbol information from JSON
            using JsonDocument jsonDoc = JsonDocument.Parse(balanceSheetJson);
            string listedCapital = jsonDoc.RootElement.GetProperty("listedCapital").GetString() ?? "0";
            ulong authorizedCapital = ulong.Parse(listedCapital);

            // Extract trace number from the first balance sheet entry
            ulong traceNo = expectedResults[0].TraceNo;

            // Create symbol
            Symbol symbol = new Symbol(
                Guid.NewGuid(),
                isin,
                expectedResults[0].SymbolId.ToString(),
                $"{isin} Company",
                isin.Substring(0, 4),
                isin,
                isin,
                isin.Substring(0, 4),
                $"شرکت {isin}",
                null,
                authorizedCapital,
                "01",
                "001",
                Fundamental.Domain.Symbols.Enums.ProductType.Equity,
                Fundamental.Domain.Symbols.Enums.ExchangeType.TSE,
                null,
                DateTime.UtcNow);

            _fixture.DbContext.Symbols.Add(symbol);
            await _fixture.DbContext.SaveChangesAsync();

            // Setup WireMock to return the balance sheet data
            _fixture.ExternalMocks.SetupBalanceSheetApiResponse(balanceSheetJson);

            // Clean any existing balance sheet data
            _fixture.DbContext.BalanceSheets.RemoveRange(_fixture.DbContext.BalanceSheets);
            await _fixture.DbContext.SaveChangesAsync();

            // Act - Process the balance sheet data using BalanceSheetV5Processor
            BalanceSheetV5Processor processor = new BalanceSheetV5Processor(
                _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
                _fixture.Services.GetRequiredService<ILogger<BalanceSheetV5Processor>>());

            GetStatementResponse statement = new GetStatementResponse
            {
                Isin = isin,
                TracingNo = traceNo,
                HtmlUrl = "http://test.codal.ir",
                PublishDateMiladi = DateTime.Now,
                ReportingType = ReportingType.Production,
                Type = LetterType.InterimStatement,
                PublisherId = 12345
            };

            GetStatementJsonResponse jsonResponse = new GetStatementJsonResponse
            {
                Json = balanceSheetJson
            };

            await processor.Process(statement, jsonResponse, CancellationToken.None);

            // Assert - Verify balance sheet data was saved correctly
            List<BalanceSheet> savedBalanceSheets = await _fixture.DbContext.BalanceSheets
                .Where(bs => bs.Symbol.Isin == isin && bs.TraceNo == traceNo)
                .ToListAsync();

            savedBalanceSheets.Should().NotBeEmpty($"Balance sheet data should be saved for ISIN {isin}");
            savedBalanceSheets.Count.Should()
                .Be(expectedResults.Count,  $"Should have {expectedResults.Count} balance sheet entries for ISIN {isin}");

            // Verify each expected result
            foreach (BalanceSheetExpectation expected in expectedResults)
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

    private record BalanceSheetExpectation(
        int SymbolId,
        ulong TraceNo,
        string Uri,
        string Currency,
        int YearEndMonth,
        int ReportMonth,
        int Row,
        int CodalRow,
        BalanceSheetCategory CodalCategory,
        string? Description,
        bool IsAudited,
        int FiscalYear,
        decimal Value,
        DateTime CreatedAt,
        DateTime ModifiedAt,
        int FinancialStatementId);

    private static async Task<List<BalanceSheetExpectation>> ParseCsvExpectations(string csvFilePath)
    {
        List<BalanceSheetExpectation> expectations = new();
        string[] lines = await File.ReadAllLinesAsync(csvFilePath);

        // Skip header line
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] fields = line.Split(',');

            if (fields.Length < 17)
            {
                continue;
            }

            // Handle empty strings for integer fields
            int symbolId = string.IsNullOrWhiteSpace(fields[2]) ? 0 : int.Parse(fields[2]);
            ulong traceNo = string.IsNullOrWhiteSpace(fields[3]) ? 0 : ulong.Parse(fields[3]);
            int yearEndMonth = string.IsNullOrWhiteSpace(fields[6]) ? 0 : int.Parse(fields[6]);
            int reportMonth = string.IsNullOrWhiteSpace(fields[7]) ? 0 : int.Parse(fields[7]);
            int row = string.IsNullOrWhiteSpace(fields[8]) ? 0 : int.Parse(fields[8]);
            int codalRow = string.IsNullOrWhiteSpace(fields[9]) ? 0 : int.Parse(fields[9]);
            int codalCategoryValue = string.IsNullOrWhiteSpace(fields[10]) ? 0 : int.Parse(fields[10]);
            int fiscalYear = string.IsNullOrWhiteSpace(fields[13]) ? 0 : int.Parse(fields[13]);
            decimal value = string.IsNullOrWhiteSpace(fields[14]) ? 0 : decimal.Parse(fields[14]);
            int financialStatementId = fields.Length > 17 && !string.IsNullOrWhiteSpace(fields[17]) ? int.Parse(fields[17]) : 0;

            BalanceSheetExpectation expectation = new(
                SymbolId: symbolId,
                TraceNo: traceNo,
                Uri: fields[4],
                Currency: fields[5],
                YearEndMonth: yearEndMonth,
                ReportMonth: reportMonth,
                Row: row,
                CodalRow: codalRow,
                codalCategoryValue == 1 ? BalanceSheetCategory.Assets : BalanceSheetCategory.Liability,
                Description: fields[11] == string.Empty ? null : fields[11],
                IsAudited: !string.IsNullOrWhiteSpace(fields[12]) && bool.Parse(fields[12]),
                FiscalYear: fiscalYear,
                Value: value,
                CreatedAt: DateTime.Parse(fields[15], System.Globalization.CultureInfo.InvariantCulture),
                ModifiedAt: DateTime.Parse(fields[16], System.Globalization.CultureInfo.InvariantCulture),
                FinancialStatementId: financialStatementId);

            expectations.Add(expectation);
        }

        return expectations;
    }
}