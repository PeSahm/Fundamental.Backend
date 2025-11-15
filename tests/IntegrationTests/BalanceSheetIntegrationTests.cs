using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Helpers;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntegrationTests;

using Fundamental.ErrorHandling;

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

            savedBalanceSheets.Should().HaveCount(3, $"Should have 3 balance sheet headers for ISIN {isin} (one for each fiscal year/report month combination)");

            // Verify each balance sheet header
            var balanceSheet1404 = savedBalanceSheets.Single(bs => bs.FiscalYear.Year == 1404 && bs.ReportMonth.Month == 6 && !bs.IsAudited);
            var balanceSheet1403 = savedBalanceSheets.Single(bs => bs.FiscalYear.Year == 1403 && bs.ReportMonth.Month == 12 && bs.IsAudited);
            var balanceSheet1402 = savedBalanceSheets.Single(bs => bs.FiscalYear.Year == 1402 && bs.ReportMonth.Month == 12 && bs.IsAudited);

            // Verify each balance sheet has the correct properties
            foreach (var balanceSheet in savedBalanceSheets)
            {
                balanceSheet.Symbol.Isin.Should().Be(isin);
                balanceSheet.TraceNo.Should().Be(traceNo);
            }

            // Verify details for each balance sheet
            var details1404 = await _fixture.DbContext.BalanceSheetDetails
                .Where(d => d.BalanceSheet.Id == balanceSheet1404.Id)
                .ToListAsync();

            var details1403 = await _fixture.DbContext.BalanceSheetDetails
                .Where(d => d.BalanceSheet.Id == balanceSheet1403.Id)
                .ToListAsync();

            var details1402 = await _fixture.DbContext.BalanceSheetDetails
                .Where(d => d.BalanceSheet.Id == balanceSheet1402.Id)
                .ToListAsync();

            // Group expected results by fiscal year, report month, and audit status
            Dictionary<(int FiscalYear, int ReportMonth, bool IsAudited), List<BalanceSheetTestData.BalanceSheetExpectation>> expectedByPeriod = expectedResults.GroupBy(e => (e.FiscalYear, e.ReportMonth, e.IsAudited)).ToDictionary(g => g.Key, g => g.ToList());

            // Verify details for each period
            foreach (KeyValuePair<(int FiscalYear, int ReportMonth, bool IsAudited), List<BalanceSheetTestData.BalanceSheetExpectation>> period in expectedByPeriod)
            {
                (int fiscalYear, int reportMonth, bool isAudited) = period.Key;
                List<BalanceSheetTestData.BalanceSheetExpectation> expectedForPeriod = period.Value;
                List<BalanceSheetDetail> actualDetails;
                if (fiscalYear == 1404)
                {
                    actualDetails = details1404;
                }
                else if (fiscalYear == 1403)
                {
                    actualDetails = details1403;
                }
                else
                {
                    actualDetails = details1402;
                }

                actualDetails.Should().HaveCount(expectedForPeriod.Count, $"Should have {expectedForPeriod.Count} balance sheet details for fiscal year {fiscalYear}, report month {reportMonth}, audited {isAudited}");

                // Verify each expected result for this period
                foreach (BalanceSheetTestData.BalanceSheetExpectation expected in expectedForPeriod)
                {
                    BalanceSheetDetail actual = actualDetails.Single(d =>
                        d.CodalRow == expected.CodalRow &&
                        d.CodalCategory == expected.CodalCategory);

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
    [Fact]
    public async Task AddBalanceSheetCommandHandler_ShouldAddBalanceSheetSuccessfully()
    {
        // Arrange
        await CleanBalanceSheetData();

        // Seed required BalanceSheetSort data
        List<BalanceSheetSort> balanceSheetSorts = new()
        {
            new BalanceSheetSort(Guid.NewGuid(), 1, 4, BalanceSheetCategory.Assets, DateTime.UtcNow),
            new BalanceSheetSort(Guid.NewGuid(), 2, 6, BalanceSheetCategory.Liability, DateTime.UtcNow)
        };
        await _fixture.DbContext.CodalRowOrders.AddRangeAsync(balanceSheetSorts);
        await _fixture.DbContext.SaveChangesAsync();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1ADDBS001", 1000000000UL, "ADDBS001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create valid balance sheet items
        List<AddBalanceSheetItem> items = new()
        {
            new AddBalanceSheetItem(BalanceSheetCategory.Assets, 4, 1000000m),
            new AddBalanceSheetItem(BalanceSheetCategory.Liability, 6, 500000m)
        };

        AddBalanceSheetRequest request = new AddBalanceSheetRequest(
            Isin: "IRO1ADDBS001",
            TraceNo: 999999999UL,
            Uri: "http://test.codal.ir/add-test",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: items);

        AddBalanceSheetCommandHandler handler = new AddBalanceSheetCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();

        // Verify balance sheet was created
        BalanceSheet? savedBalanceSheet = await _fixture.DbContext.BalanceSheets
            .FirstOrDefaultAsync(bs => bs.TraceNo == 999999999UL);

        savedBalanceSheet.Should().NotBeNull();
        savedBalanceSheet!.Symbol.Isin.Should().Be("IRO1ADDBS001");
        savedBalanceSheet.FiscalYear.Year.Should().Be(1402);
        savedBalanceSheet.ReportMonth.Month.Should().Be(12);
        savedBalanceSheet.IsAudited.Should().BeTrue();

        // Verify balance sheet details were created
        List<BalanceSheetDetail> savedDetails = await _fixture.DbContext.BalanceSheetDetails
            .Where(d => d.BalanceSheet.Id == savedBalanceSheet.Id)
            .ToListAsync();

        savedDetails.Should().HaveCount(2);

        BalanceSheetDetail assetsDetail = savedDetails.First(d => d.CodalCategory == BalanceSheetCategory.Assets);
        assetsDetail.CodalRow.Should().Be(4);
        assetsDetail.Value.RealValue.Should().Be(1000000000000); // 1000000 * 1000000 (CodalMoneyMultiplier)

        BalanceSheetDetail liabilityDetail = savedDetails.First(d => d.CodalCategory == BalanceSheetCategory.Liability);
        liabilityDetail.CodalRow.Should().Be(6);
        liabilityDetail.Value.RealValue.Should().Be(500000000000); // 500000 * 1000000 (CodalMoneyMultiplier)
    }

    [Fact]
    public async Task AddBalanceSheetCommandHandler_ShouldReturnSymbolNotFound_WhenSymbolDoesNotExist()
    {
        // Arrange
        await CleanBalanceSheetData();

        AddBalanceSheetRequest request = new AddBalanceSheetRequest(
            Isin: "IRO1NONEXIST",
            TraceNo: 999999998UL,
            Uri: "http://test.codal.ir/not-found",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: new List<AddBalanceSheetItem>());

        AddBalanceSheetCommandHandler handler = new AddBalanceSheetCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        ResponseExtensions.ErrorCode(result).Should().Be((int)AddBalanceSheetErrorCodes.SymbolNotFound);
    }

    [Fact]
    public async Task AddBalanceSheetCommandHandler_ShouldReturnDuplicateTraceNo_WhenTraceNoAlreadyExists()
    {
        // Arrange
        await CleanBalanceSheetData();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1DUPTRC001", 1000000000UL, "DUPTRC001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create first balance sheet
        BalanceSheet existingBalanceSheet = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol,
            999999997UL,
            "http://test.codal.ir/existing",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.DbContext.BalanceSheets.AddAsync(existingBalanceSheet);
        await _fixture.DbContext.SaveChangesAsync();

        AddBalanceSheetRequest request = new AddBalanceSheetRequest(
            Isin: "IRO1DUPTRC001",
            TraceNo: 999999997UL, // Same trace number
            Uri: "http://test.codal.ir/duplicate",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: new List<AddBalanceSheetItem>());

        AddBalanceSheetCommandHandler handler = new AddBalanceSheetCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        ResponseExtensions.ErrorCode(result).Should().Be((int)AddBalanceSheetErrorCodes.DuplicateTraceNo);
    }

    [Fact]
    public async Task AddBalanceSheetCommandHandler_ShouldReturnDuplicateStatement_WhenStatementAlreadyExists()
    {
        // Arrange
        await CleanBalanceSheetData();

        // Seed required BalanceSheetSort data
        List<BalanceSheetSort> balanceSheetSorts = new()
        {
            new BalanceSheetSort(Guid.NewGuid(), 1, 4, BalanceSheetCategory.Assets, DateTime.UtcNow),
            new BalanceSheetSort(Guid.NewGuid(), 2, 6, BalanceSheetCategory.Liability, DateTime.UtcNow)
        };
        await _fixture.DbContext.CodalRowOrders.AddRangeAsync(balanceSheetSorts);
        await _fixture.DbContext.SaveChangesAsync();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1DUPSTMT001", 1000000000UL, "DUPSTMT001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create existing balance sheet
        BalanceSheet existingBalanceSheet = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol,
            999999996UL,
            "http://test.codal.ir/existing",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.DbContext.BalanceSheets.AddAsync(existingBalanceSheet);
        await _fixture.DbContext.SaveChangesAsync();

        AddBalanceSheetRequest request = new AddBalanceSheetRequest(
            Isin: "IRO1DUPSTMT001",
            TraceNo: 999999996UL,
            Uri: "http://test.codal.ir/duplicate",
            FiscalYear: 1402, // Same fiscal year
            YearEndMonth: 12, // Same year end month
            ReportMonth: 12, // Same report month
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: new List<AddBalanceSheetItem>());

        AddBalanceSheetCommandHandler handler = new AddBalanceSheetCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        ResponseExtensions.ErrorCode(result).Should().Be((int)AddBalanceSheetErrorCodes.DuplicateTraceNo);
    }

    [Fact]
    public async Task GetBalanceSheetDetailsQueryHandler_ShouldReturnDetailsSuccessfully()
    {
        // Arrange
        await CleanBalanceSheetData();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1GETDTL001", 1000000000UL, "GETDTL001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create balance sheet with details
        BalanceSheet balanceSheet = new BalanceSheet(
            Guid.NewGuid(),
            testSymbol,
            999999995UL,
            "http://test.codal.ir/details",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.DbContext.BalanceSheets.AddAsync(balanceSheet);

        // Create balance sheet details
        List<BalanceSheetDetail> details = new()
        {
            new BalanceSheetDetail(
                Guid.NewGuid(),
                balanceSheet,
                1,
                4,
                BalanceSheetCategory.Assets,
                "Current Assets",
                new SignedCodalMoney(1000000, IsoCurrency.IRR),
                DateTime.UtcNow),
            new BalanceSheetDetail(
                Guid.NewGuid(),
                balanceSheet,
                2,
                6,
                BalanceSheetCategory.Liability,
                "Current Liabilities",
                new SignedCodalMoney(500000, IsoCurrency.IRR),
                DateTime.UtcNow)
        };

        await _fixture.DbContext.BalanceSheetDetails.AddRangeAsync(details);
        await _fixture.DbContext.SaveChangesAsync();

        GetBalanceSheetDetailsRequest request = new GetBalanceSheetDetailsRequest(
            TraceNo: 999999995UL,
            FiscalYear: 1402,
            ReportMonth: 12);

        GetBalanceSheetDetailsQueryHandler handler = new GetBalanceSheetDetailsQueryHandler(
            _fixture.Services.GetRequiredService<IRepository>());

        // Act
        Response<List<GetBalanceSheetDetailResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(2);

        // Verify details are ordered by row
        GetBalanceSheetDetailResultDto firstDetail = result.Data[0];
        firstDetail.Order.Should().Be(1);
        firstDetail.CodalRow.Should().Be(4);
        firstDetail.Category.Should().Be(BalanceSheetCategory.Assets);

        GetBalanceSheetDetailResultDto secondDetail = result.Data[1];
        secondDetail.Order.Should().Be(2);
        secondDetail.CodalRow.Should().Be(6);
        secondDetail.Category.Should().Be(BalanceSheetCategory.Liability);
    }

    [Fact]
    public async Task GetBalanceSheetDetailsQueryHandler_ShouldReturnEmptyList_WhenNoDetailsExist()
    {
        // Arrange
        await CleanBalanceSheetData();

        GetBalanceSheetDetailsRequest request = new GetBalanceSheetDetailsRequest(
            TraceNo: 999999994UL,
            FiscalYear: 1402,
            ReportMonth: 12);

        GetBalanceSheetDetailsQueryHandler handler = new GetBalanceSheetDetailsQueryHandler(
            _fixture.Services.GetRequiredService<IRepository>());

        // Act
        Response<List<GetBalanceSheetDetailResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEmpty();
    }
}