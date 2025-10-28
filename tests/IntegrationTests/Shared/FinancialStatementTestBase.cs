using System.Text.Json;
using FluentAssertions;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntegrationTests.Shared;

/// <summary>
/// Base class for financial statement integration tests providing common setup and utilities.
/// </summary>
public abstract class FinancialStatementTestBase : IClassFixture<TestFixture>
{
    protected readonly TestFixture _fixture;

    protected FinancialStatementTestBase(TestFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Creates a test symbol with the specified ISIN and basic properties.
    /// </summary>
    protected Symbol CreateTestSymbol(string isin, ulong authorizedCapital, string symbolId = null)
    {
        symbolId ??= Guid.NewGuid().ToString();

        return new Symbol(
            Guid.NewGuid(),
            isin,
            symbolId,
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
    }

    /// <summary>
    /// Creates a GetStatementResponse for testing.
    /// </summary>
    protected GetStatementResponse CreateStatementResponse(string isin, ulong traceNo)
    {
        return new GetStatementResponse
        {
            Isin = isin,
            TracingNo = traceNo,
            HtmlUrl = "http://test.codal.ir",
            PublishDateMiladi = DateTime.Now,
            ReportingType = ReportingType.Production,
            Type = LetterType.InterimStatement,
            PublisherId = 12345
        };
    }

    /// <summary>
    /// Creates a GetStatementJsonResponse for testing.
    /// </summary>
    protected GetStatementJsonResponse CreateJsonResponse(string jsonData)
    {
        return new GetStatementJsonResponse
        {
            Json = jsonData
        };
    }

    /// <summary>
    /// Extracts the listed capital from balance sheet JSON data.
    /// </summary>
    protected ulong ExtractAuthorizedCapital(string balanceSheetJson)
    {
        using JsonDocument jsonDoc = JsonDocument.Parse(balanceSheetJson);
        string listedCapital = jsonDoc.RootElement.GetProperty("listedCapital").GetString() ?? "0";
        return ulong.Parse(listedCapital);
    }

    /// <summary>
    /// Sets up WireMock to return the specified balance sheet data.
    /// </summary>
    protected void SetupBalanceSheetApiResponse(string balanceSheetJson)
    {
        _fixture.ExternalMocks.SetupBalanceSheetApiResponse(balanceSheetJson);
    }

    /// <summary>
    /// Cleans all existing balance sheet data from the database.
    /// </summary>
    protected async Task CleanBalanceSheetData()
    {
        _fixture.DbContext.BalanceSheetDetails.RemoveRange(_fixture.DbContext.BalanceSheetDetails);
        _fixture.DbContext.BalanceSheets.RemoveRange(_fixture.DbContext.BalanceSheets);
        _fixture.DbContext.CodalRowOrders.RemoveRange(_fixture.DbContext.CodalRowOrders);
        await _fixture.DbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Cleans all existing income statement data from the database.
    /// </summary>
    protected async Task CleanIncomeStatementData()
    {
        _fixture.DbContext.IncomeStatements.RemoveRange(_fixture.DbContext.IncomeStatements);
        _fixture.DbContext.IncomeStatementSorts.RemoveRange(_fixture.DbContext.IncomeStatementSorts);
        await _fixture.DbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Gets the test data directory path relative to the test assembly.
    /// </summary>
    protected string GetTestDataDirectory(string relativePath)
    {
        string? testProjectDirectory = Path.GetDirectoryName(GetType().Assembly.Location);

        if (testProjectDirectory is null)
        {
            throw new InvalidOperationException("Test project directory could not be determined.");
        }

        return Path.Combine(testProjectDirectory, "..", "..", "..", relativePath);
    }

    /// <summary>
    /// Finds all ISIN folders in the specified directory.
    /// </summary>
    protected string[] GetIsinFolders(string dataDirectory)
    {
        string[] isinFolders = Directory.GetDirectories(dataDirectory);
        isinFolders.Should().NotBeEmpty("Test data folders should exist");
        return isinFolders;
    }

    /// <summary>
    /// Gets the JSON and CSV file paths for an ISIN folder.
    /// </summary>
    protected static (string JsonPath, string CsvPath) GetTestFiles(string isinFolder, string isin)
    {
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

        return (jsonFilePath, csvFilePath);
    }
}