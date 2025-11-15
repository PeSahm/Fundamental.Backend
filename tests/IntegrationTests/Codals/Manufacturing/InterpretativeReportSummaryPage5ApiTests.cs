using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages5;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

public class InterpretativeReportSummaryPage5ApiTests : FinancialStatementTestBase
{
    public InterpretativeReportSummaryPage5ApiTests(TestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithoutFilters_ShouldReturnPaginatedList()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5sRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().HaveCountGreaterThan(0);
        response.Data.Meta.Total.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithIsinFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5sRequest request = new("IRO1SEPP0001", null, null);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.Isin.Should().Be("IRO1SEPP0001"));
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithFiscalYearFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what fiscal year we have
        CanonicalInterpretativeReportSummaryPage5? firstEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int fiscalYear = firstEntity!.FiscalYear.Year;

        GetInterpretativeReportSummaryPage5sRequest request = new(null, fiscalYear, null);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.FiscalYear.Should().Be(fiscalYear));
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithReportMonthFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what report month we have
        CanonicalInterpretativeReportSummaryPage5? firstEntity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int reportMonth = firstEntity!.ReportMonth.Month;

        GetInterpretativeReportSummaryPage5sRequest request = new(null, null, reportMonth);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.ReportMonth.Should().Be(reportMonth));
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5sRequest request = new(null, null, null)
        {
            PageNumber = 1,
            PageSize = 5
        };

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().HaveCountLessThanOrEqualTo(5);
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_ShouldOrderByFiscalYearDescThenReportMonthDesc()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5sRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();

        if (response.Data!.Items.Count > 1)
        {
            // Verify ordering: fiscal year DESC, then report month DESC
            for (int i = 0; i < response.Data.Items.Count - 1; i++)
            {
                GetInterpretativeReportSummaryPage5ListItem current = response.Data.Items[i];
                GetInterpretativeReportSummaryPage5ListItem next = response.Data.Items[i + 1];

                if (current.FiscalYear == next.FiscalYear)
                {
                    current.ReportMonth.Should().BeGreaterOrEqualTo(next.ReportMonth);
                }
                else
                {
                    current.FiscalYear.Should().BeGreaterThan(next.FiscalYear);
                }
            }
        }
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5ById_WithValidId_ShouldReturnDetailItem()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        CanonicalInterpretativeReportSummaryPage5? entity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync();

        entity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5ByIdRequest request = new(entity!.Id);

        // Act
        Response<GetInterpretativeReportSummaryPage5DetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(entity.Id);
        response.Data.Isin.Should().Be("IRO1SEPP0001");
        response.Data.FiscalYear.Should().BeGreaterThan(1400);
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5ById_ShouldIncludeAllCollections()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        CanonicalInterpretativeReportSummaryPage5? entity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync();

        entity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5ByIdRequest request = new(entity!.Id);

        // Act
        Response<GetInterpretativeReportSummaryPage5DetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        GetInterpretativeReportSummaryPage5DetailItem item = response.Data!;

        // Verify all collections are loaded
        item.OtherOperatingIncomes.Should().NotBeNull();
        item.OtherNonOperatingExpenses.Should().NotBeNull();
        item.FinancingDetails.Should().NotBeNull();
        item.FinancingDetailsEstimated.Should().NotBeNull();
        item.InvestmentIncomes.Should().NotBeNull();
        item.MiscellaneousExpenses.Should().NotBeNull();
        item.Descriptions.Should().NotBeNull();

        // At least some collections should have data
        bool hasData = item.OtherOperatingIncomes.Count > 0 ||
                       item.OtherNonOperatingExpenses.Count > 0 ||
                       item.FinancingDetails.Count > 0 ||
                       item.InvestmentIncomes.Count > 0 ||
                       item.MiscellaneousExpenses.Count > 0;

        hasData.Should().BeTrue("at least one collection should contain data");
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5ById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        Guid nonExistentId = Guid.NewGuid();
        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5ByIdRequest request = new(nonExistentId);

        // Act
        Response<GetInterpretativeReportSummaryPage5DetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5ById_ShouldReturnCorrectRowCodeEnums()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();
        await SeedTestData();

        CanonicalInterpretativeReportSummaryPage5? entity = await _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync();

        entity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5ByIdRequest request = new(entity!.Id);

        // Act
        Response<GetInterpretativeReportSummaryPage5DetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        GetInterpretativeReportSummaryPage5DetailItem item = response.Data!;

        // Verify enum types are correctly mapped
        if (item.OtherOperatingIncomes.Count > 0)
        {
            item.OtherOperatingIncomes.Should().AllSatisfy(x =>
                x.RowCode.Should().BeDefined("RowCode should be a valid enum value"));
        }

        if (item.FinancingDetails.Count > 0)
        {
            item.FinancingDetails.Should().AllSatisfy(x =>
                x.RowCode.Should().BeDefined("RowCode should be a valid enum value"));
        }
    }

    [Fact]
    public async Task GetInterpretativeReportSummaryPage5s_WithNoData_ShouldReturnEmptyList()
    {
        // Arrange
        await CleanInterpretativeReportSummaryPage5Data();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetInterpretativeReportSummaryPage5sRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().BeEmpty();
        response.Data.Meta.Total.Should().Be(0);
    }

    /// <summary>
    /// Seeds test data by processing the V2 JSON file.
    /// </summary>
    private async Task SeedTestData()
    {
        Symbol symbol = CreateTestSymbol("IRO1SEPP0001", 1234567UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = InterpretativeReportSummaryPage5TestData.GetV2TestData();
        SetupApiResponse("interpretative-report-summary-page5", testJson);

        InterpretativeReportSummaryPage5V2Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>()
        );

        GetStatementResponse statement = CreateStatementResponse("IRO1SEPP0001", 1234567);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);
    }

    /// <summary>
    /// Cleans all existing interpretative report summary page 5 data from the database.
    /// </summary>
    private async Task CleanInterpretativeReportSummaryPage5Data()
    {
        _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s.RemoveRange(
            _fixture.DbContext.CanonicalInterpretativeReportSummaryPage5s);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1SEPP0001")
            .ToListAsync();
        _fixture.DbContext.Symbols.RemoveRange(testSymbols);

        await _fixture.DbContext.SaveChangesAsync();
    }

    private void SetupApiResponse(string endpoint, string responseJson)
    {
        _fixture.WireMockServer
            .Given(Request.Create().WithPath($"/{endpoint}").UsingGet())
            .RespondWith(WireMockResponse.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithBody(responseJson));
    }
}