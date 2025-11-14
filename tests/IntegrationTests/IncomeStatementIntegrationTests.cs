using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementDetails;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.IncomeStatements;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

/// <summary>
/// Integration tests for income statement processing functionality.
/// </summary>
public class IncomeStatementIntegrationTests : FinancialStatementTestBase
{
    public IncomeStatementIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task IncomeStatementV7Processing_ShouldProcessDataCorrectly()
    {
        // Get the test data directory (relative to the test project source)
        string testDataDirectory = GetTestDataDirectory(Path.Combine("Data", "IncomeStatements", "V7"));

        // Find all ISIN folders
        string[] isinFolders = GetIsinFolders(testDataDirectory);

        foreach (string isinFolder in isinFolders)
        {
            string isin = Path.GetFileName(isinFolder);
            (string jsonFilePath, string csvFilePath) = GetTestFiles(isinFolder, isin);

            // Read and parse JSON file
            string incomeStatementJson = await File.ReadAllTextAsync(jsonFilePath);

            // Read and parse CSV file for expected results
            List<IncomeStatementTestData.IncomeStatementExpectation> expectedResults =
                await IncomeStatementTestData.ParseCsvExpectations(csvFilePath);

            // Extract symbol information from JSON
            ulong authorizedCapital = ExtractAuthorizedCapital(incomeStatementJson);

            // Extract trace number from the first income statement entry
            ulong traceNo = expectedResults[0].TraceNo;

            // Create symbol
            Symbol symbol = CreateTestSymbol(isin, authorizedCapital, expectedResults[0].SymbolId.ToString());

            _fixture.DbContext.Symbols.Add(symbol);
            await _fixture.DbContext.SaveChangesAsync();

            // Clean any existing income statement data
            await CleanIncomeStatementData();

            // Act - Process the income statement data using IncomeStatementsV7Processor
            IncomeStatementsV7Processor processor = new IncomeStatementsV7Processor(
                _fixture.Services.GetRequiredService<IServiceScopeFactory>());

            GetStatementResponse statement = CreateStatementResponse(isin, traceNo);
            GetStatementJsonResponse jsonResponse = CreateJsonResponse(incomeStatementJson);

            await processor.Process(statement, jsonResponse, CancellationToken.None);

            // Assert - Verify income statement data was saved correctly
            List<IncomeStatement> savedIncomeStatements = await _fixture.DbContext.IncomeStatements
                .Include(x => x.Details)
                .Where(bs => bs.Symbol.Isin == isin && bs.TraceNo == traceNo)
                .ToListAsync();

            // Group expected results by fiscal year, report month, and audit status
            Dictionary<(int FiscalYear, int ReportMonth, bool IsAudited), List<IncomeStatementTestData.IncomeStatementExpectation>> expectedByPeriod = expectedResults.GroupBy(e => (e.FiscalYear, e.ReportMonth, e.IsAudited)).ToDictionary(g => g.Key, g => g.ToList());

            savedIncomeStatements.Should().HaveCount(expectedByPeriod.Count, $"Should have {expectedByPeriod.Count} income statement masters for ISIN {isin}");

            // Verify each income statement entry
            foreach (KeyValuePair<(int FiscalYear, int ReportMonth, bool IsAudited), List<IncomeStatementTestData.IncomeStatementExpectation>> period in expectedByPeriod)
            {
                (int fiscalYear, int reportMonth, bool isAudited) = period.Key;
                List<IncomeStatementTestData.IncomeStatementExpectation> expectedForPeriod = period.Value;

                IncomeStatement actualMaster = savedIncomeStatements.Single(s =>
                    s.FiscalYear.Year == fiscalYear &&
                    s.ReportMonth.Month == reportMonth &&
                    s.IsAudited == isAudited);

                actualMaster.Details.Should().HaveCount(expectedForPeriod.Count, $"Master should have {expectedForPeriod.Count} details for fiscal year {fiscalYear}, report month {reportMonth}, audited {isAudited}");

                // Verify each expected result for this period
                foreach (IncomeStatementTestData.IncomeStatementExpectation expected in expectedForPeriod)
                {
                    IncomeStatementDetail actual = actualMaster.Details.Single(d =>
                        d.CodalRow == expected.CodalRow &&
                        d.Row == expected.Row);

                    actual.Should()
                        .NotBeNull(
                            $"Income statement detail should exist for CodalRow {expected.CodalRow}, FiscalYear {expected.FiscalYear}, ReportMonth {expected.ReportMonth}");

                    // actual.Description.Should().Be(expected.Description, $"Description should match for CodalRow {expected.CodalRow}");
                    actual.Value.RealValue.Should().Be(expected.Value, $"Value should match for CodalRow {expected.CodalRow}");
                    actual.IncomeStatement.IsAudited.Should().Be(expected.IsAudited, $"IsAudited should match for CodalRow {expected.CodalRow}");
                    actual.IncomeStatement.Symbol.Isin.Should().Be(isin, $"Symbol ISIN should match");
                    actual.IncomeStatement.TraceNo.Should().Be(traceNo, $"Trace number should match");
                }
            }
        }
    }

    [Fact]
    public async Task GetIncomeStatementsQueryHandler_ShouldReturnIncomeStatementsFromDatabase()
    {
        // Arrange
        await CleanIncomeStatementData();

        // Create test symbols
        Symbol testSymbol1 = CreateTestSymbol("IRO1TEST0001", 1000000000UL, "TEST1");
        Symbol testSymbol2 = CreateTestSymbol("IRO1TEST0002", 2000000000UL, "TEST2");

        await _fixture.DbContext.Symbols.AddRangeAsync(testSymbol1, testSymbol2);
        await _fixture.DbContext.SaveChangesAsync();

        // Create test income statements with different fiscal years/report months to avoid grouping
        DateTime createdAt = DateTime.UtcNow;
        DateTime publishDate1 = DateTime.UtcNow.AddDays(-1);
        DateTime publishDate2 = DateTime.UtcNow; // Later publish date
        IncomeStatement incomeStatement1 = new IncomeStatement(
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
        incomeStatement1.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            incomeStatement1,
            1,
            3,
            "Sales",
            new SignedCodalMoney(1000000, IsoCurrency.IRR),
            createdAt));

        IncomeStatement incomeStatement2 = new IncomeStatement(
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
        incomeStatement2.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            incomeStatement2,
            2,
            4,
            "Cost of Goods Sold",
            new SignedCodalMoney(500000, IsoCurrency.IRR),
            createdAt));

        IncomeStatement incomeStatement3 = new IncomeStatement(
            Guid.NewGuid(),
            testSymbol2,
            123456791UL,
            "http://test.codal.ir/3",
            new FiscalYear(1402),
            new StatementMonth(12),
            new StatementMonth(12),
            true,
            createdAt,
            publishDate2);
        incomeStatement3.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            incomeStatement3,
            1,
            3,
            "Sales",
            new SignedCodalMoney(2000000, IsoCurrency.IRR),
            createdAt));

        await _fixture.DbContext.IncomeStatements.AddRangeAsync(incomeStatement1, incomeStatement2, incomeStatement3);
        await _fixture.DbContext.SaveChangesAsync();

        GetIncomeStatementsRequest request = new GetIncomeStatementsRequest(
            IsinList: new List<string> { "IRO1TEST0001" },
            TraceNo: null,
            FiscalYear: 1402,
            ReportMonth: null);

        GetIncomeStatementsQueryHandler handler = new GetIncomeStatementsQueryHandler(
            _fixture.Services.GetRequiredService<IIncomeStatementsReadRepository>());

        Response<Paginated<GetIncomeStatementsResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Items.Should().HaveCount(2);

        GetIncomeStatementsResultDto firstItem = result.Data.Items[0];
        firstItem.Isin.Should().Be("IRO1TEST0001");
        firstItem.FiscalYear.Should().Be(1402);
        firstItem.ReportMonth.Should().Be(6); // Ordered by PublishDate desc (incomeStatement2 has later publish date)

        GetIncomeStatementsResultDto secondItem = result.Data.Items[1];
        secondItem.Isin.Should().Be("IRO1TEST0001");
        secondItem.FiscalYear.Should().Be(1402);
        secondItem.ReportMonth.Should().Be(12); // incomeStatement1 has earlier publish date

        // Test with non-existent ISIN
        GetIncomeStatementsRequest emptyRequest = new GetIncomeStatementsRequest(
            IsinList: new List<string> { "NONEXISTENT" },
            TraceNo: null,
            FiscalYear: 1402,
            ReportMonth: null);

        Response<Paginated<GetIncomeStatementsResultDto>> emptyResult = await handler.Handle(emptyRequest, CancellationToken.None);

        emptyResult.Success.Should().BeTrue();
        emptyResult.Data?.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task AddIncomeStatementCommandHandler_ShouldAddIncomeStatementSuccessfully()
    {
        // Arrange
        await CleanIncomeStatementData();

        // Seed required IncomeStatementSort data
        List<IncomeStatementSort> incomeStatementSorts = new()
        {
            new IncomeStatementSort(Guid.NewGuid(), 1, 3, "Sales", DateTime.UtcNow),
            new IncomeStatementSort(Guid.NewGuid(), 2, 4, "Cost of Goods Sold", DateTime.UtcNow)
        };
        await _fixture.DbContext.IncomeStatementSorts.AddRangeAsync(incomeStatementSorts);
        await _fixture.DbContext.SaveChangesAsync();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1ADDIS001", 1000000000UL, "ADDIS001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create valid income statement items
        List<AddIncomeStatementItem> items = new()
        {
            new AddIncomeStatementItem(1, 3, 1000000m),
            new AddIncomeStatementItem(2, 4, 500000m)
        };

        AddIncomeStatementRequest request = new AddIncomeStatementRequest(
            Isin: "IRO1ADDIS001",
            TraceNo: 999999999UL,
            Uri: "http://test.codal.ir/add-test",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: items);

        AddIncomeStatementCommandHandler handler = new AddIncomeStatementCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();

        // Verify income statement was created
        List<IncomeStatement> savedIncomeStatements = await _fixture.DbContext.IncomeStatements
            .Where(bs => bs.TraceNo == 999999999UL)
            .ToListAsync();

        savedIncomeStatements.Should().HaveCount(1);
        IncomeStatement master = savedIncomeStatements[0];
        master.Details.Should().HaveCount(2);

        IncomeStatementDetail salesDetail = master.Details.First(d => d.CodalRow == 3);
        salesDetail.IncomeStatement.Symbol.Isin.Should().Be("IRO1ADDIS001");
        salesDetail.IncomeStatement.FiscalYear.Year.Should().Be(1402);
        salesDetail.IncomeStatement.ReportMonth.Month.Should().Be(12);
        salesDetail.IncomeStatement.IsAudited.Should().BeTrue();
        salesDetail.Value.RealValue.Should().Be(1000000000000); // 1000000 * 1000000 (CodalMoneyMultiplier)

        IncomeStatementDetail cogsDetail = master.Details.First(d => d.CodalRow == 4);
        cogsDetail.Value.RealValue.Should().Be(500000000000); // 500000 * 1000000 (CodalMoneyMultiplier)
    }

    [Fact]
    public async Task AddIncomeStatementCommandHandler_ShouldReturnSymbolNotFound_WhenSymbolDoesNotExist()
    {
        // Arrange
        await CleanIncomeStatementData();

        AddIncomeStatementRequest request = new AddIncomeStatementRequest(
            Isin: "IRO1NONEXIST",
            TraceNo: 999999998UL,
            Uri: "http://test.codal.ir/not-found",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: new List<AddIncomeStatementItem>());

        AddIncomeStatementCommandHandler handler = new AddIncomeStatementCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        ResponseExtensions.ErrorCode(result).Should().Be((int)AddIncomeStatementErrorCodes.SymbolNotFound);
    }

    [Fact]
    public async Task AddIncomeStatementCommandHandler_ShouldReturnDuplicateTraceNo_WhenTraceNoAlreadyExists()
    {
        // Arrange
        await CleanIncomeStatementData();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1DUPTRC001", 1000000000UL, "DUPTRC001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create first income statement
        IncomeStatement existingIncomeStatement = new IncomeStatement(
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
        existingIncomeStatement.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            existingIncomeStatement,
            1,
            3,
            "Sales",
            new SignedCodalMoney(1000000, IsoCurrency.IRR),
            DateTime.UtcNow));

        await _fixture.DbContext.IncomeStatements.AddAsync(existingIncomeStatement);
        await _fixture.DbContext.SaveChangesAsync();

        AddIncomeStatementRequest request = new AddIncomeStatementRequest(
            Isin: "IRO1DUPTRC001",
            TraceNo: 999999997UL, // Same trace number
            Uri: "http://test.codal.ir/duplicate",
            FiscalYear: 1402,
            YearEndMonth: 12,
            ReportMonth: 12,
            IsAudited: true,
            PublishDate: DateTime.UtcNow,
            Items: new List<AddIncomeStatementItem>());

        AddIncomeStatementCommandHandler handler = new AddIncomeStatementCommandHandler(
            _fixture.Services.GetRequiredService<IRepository>(),
            _fixture.Services.GetRequiredService<IUnitOfWork>());

        // Act
        Response result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        ResponseExtensions.ErrorCode(result).Should().Be((int)AddIncomeStatementErrorCodes.DuplicateTraceNo);
    }

    [Fact]
    public async Task GetIncomeStatementDetailsQueryHandler_ShouldReturnDetailsSuccessfully()
    {
        // Arrange
        await CleanIncomeStatementData();

        // Create test symbol
        Symbol testSymbol = CreateTestSymbol("IRO1GETDTL001", 1000000000UL, "GETDTL001");
        await _fixture.DbContext.Symbols.AddAsync(testSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        // Create income statements
        IncomeStatement incomeStatement = new IncomeStatement(
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
        incomeStatement.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            incomeStatement,
            1,
            3,
            "Sales",
            new SignedCodalMoney(1000000, IsoCurrency.IRR),
            DateTime.UtcNow));
        incomeStatement.Details.Add(new IncomeStatementDetail(
            Guid.NewGuid(),
            incomeStatement,
            2,
            4,
            "Cost of Goods Sold",
            new SignedCodalMoney(500000, IsoCurrency.IRR),
            DateTime.UtcNow));

        await _fixture.DbContext.IncomeStatements.AddAsync(incomeStatement);
        await _fixture.DbContext.SaveChangesAsync();

        GetIncomeStatementDetailsRequest request = new GetIncomeStatementDetailsRequest(
            TraceNo: 999999995UL,
            FiscalYear: 1402,
            ReportMonth: 12);

        GetIncomeStatementDetailsQueryHandler handler = new GetIncomeStatementDetailsQueryHandler(
            _fixture.Services.GetRequiredService<IRepository>());

        // Act
        Response<List<GetIncomeStatementDetailsResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(2);

        // Verify details are ordered by row
        GetIncomeStatementDetailsResultDto firstDetail = result.Data[0];
        firstDetail.Order.Should().Be(1);
        firstDetail.CodalRow.Should().Be(3);
        firstDetail.Description.Should().Be("درآمد عملیاتی");

        GetIncomeStatementDetailsResultDto secondDetail = result.Data[1];
        secondDetail.Order.Should().Be(2);
        secondDetail.CodalRow.Should().Be(4);
        secondDetail.Description.Should().Be("بهای تمام شده درآمدهای عملیاتی");
    }

    [Fact]
    public async Task GetIncomeStatementDetailsQueryHandler_ShouldReturnEmptyList_WhenNoDetailsExist()
    {
        // Arrange
        await CleanIncomeStatementData();

        GetIncomeStatementDetailsRequest request = new GetIncomeStatementDetailsRequest(
            TraceNo: 999999994UL,
            FiscalYear: 1402,
            ReportMonth: 12);

        GetIncomeStatementDetailsQueryHandler handler = new GetIncomeStatementDetailsQueryHandler(
            _fixture.Services.GetRequiredService<IRepository>());

        // Act
        Response<List<GetIncomeStatementDetailsResultDto>> result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEmpty();
    }
}