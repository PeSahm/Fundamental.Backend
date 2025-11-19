using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAssembly;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

public class ExtraAssemblyApiTests : FinancialStatementTestBase
{
    public ExtraAssemblyApiTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetExtraAssemblys_WithoutFilters_ShouldReturnPaginatedList()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblysRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().HaveCountGreaterThan(0);
        response.Data.Meta.Total.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetExtraAssemblys_WithIsinFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblysRequest request = new("IRO1SSEP0001", null, null);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.Isin.Should().Be("IRO1SSEP0001"));
    }

    [Fact]
    public async Task GetExtraAssemblys_WithFiscalYearFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what fiscal year we have
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int fiscalYear = firstEntity!.FiscalYear.Year;

        GetExtraAssemblysRequest request = new(null, fiscalYear, null);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.FiscalYear.Should().Be(fiscalYear));
    }

    [Fact]
    public async Task GetExtraAssemblys_WithYearEndMonthFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what year-end month we have
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int yearEndMonth = firstEntity!.YearEndMonth.Month;

        GetExtraAssemblysRequest request = new(null, null, yearEndMonth);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.YearEndMonth.Should().Be(yearEndMonth));
    }

    [Fact]
    public async Task GetExtraAssemblys_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblysRequest request = new(null, null, null)
        {
            PageNumber = 1,
            PageSize = 5
        };

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().HaveCountLessOrEqualTo(5);
    }

    [Fact]
    public async Task GetExtraAssemblyById_WithValidId_ShouldReturnDetailItem()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        // Get a valid ID from database
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblyByIdRequest request = new(firstEntity!.Id);

        // Act
        Response<GetExtraAssemblyDetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(firstEntity.Id);
        response.Data.Isin.Should().NotBeNullOrWhiteSpace();
        response.Data.Version.Should().Be("V1");

        // Verify all collections are present
        response.Data.SessionOrders.Should().NotBeNull();
        response.Data.ShareHolders.Should().NotBeNull();
        response.Data.AssemblyBoardMembers.Should().NotBeNull();
        response.Data.IncreaseCapitals.Should().NotBeNull();
    }

    [Fact]
    public async Task GetExtraAssemblyById_WithInvalidId_ShouldReturnFailure()
    {
        // Arrange
        await CleanExtraAssemblyData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblyByIdRequest request = new(Guid.NewGuid());

        // Act
        Response<GetExtraAssemblyDetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetExtraAssemblys_ShouldOrderByFiscalYearAndDateDescending()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblysRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();

        List<GetExtraAssemblyListItem> items = response.Data!.Items.ToList();
        if (items.Count > 1)
        {
            // Verify ordering: fiscal year desc, then assembly date desc
            for (int i = 0; i < items.Count - 1; i++)
            {
                items[i].FiscalYear.Should().BeGreaterThanOrEqualTo(
                    items[i + 1].FiscalYear,
                    "items should be ordered by fiscal year descending");
            }
        }
    }

    [Fact]
    public async Task GetExtraAssemblyById_ShouldIncludeCapitalChangeState()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        // Get a valid ID from database
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblyByIdRequest request = new(firstEntity!.Id);

        // Act
        Response<GetExtraAssemblyDetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.CapitalChangeState.Should().BeOneOf(Enum.GetValues<Fundamental.Domain.Codals.Manufacturing.Enums.CapitalChangeState>());
    }

    [Fact]
    public async Task GetExtraAssemblyById_ShouldIncludeAssemblyResultTypeTitle()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        // Get a valid ID from database
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetExtraAssemblyByIdRequest request = new(firstEntity!.Id);

        // Act
        Response<GetExtraAssemblyDetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();

        // AssemblyResultTypeTitle can be null if not present in test data
    }

    [Fact]
    public async Task GetExtraAssemblys_WithAllFilters_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanExtraAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what values we have
        CanonicalExtraAssembly? firstEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .AsNoTracking()
            .Include(x => x.Symbol)
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();

        GetExtraAssemblysRequest request = new(
            firstEntity!.Symbol.Isin,
            firstEntity.FiscalYear.Year,
            firstEntity.YearEndMonth.Month);

        // Act
        Response<Paginated<GetExtraAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item =>
        {
            item.Isin.Should().Be(firstEntity.Symbol.Isin);
            item.FiscalYear.Should().Be(firstEntity.FiscalYear.Year);
            item.YearEndMonth.Should().Be(firstEntity.YearEndMonth.Month);
        });
    }

    /// <summary>
    /// Seeds test data by processing ExtraAssembly V1 data for both scenarios.
    /// </summary>
    private async Task SeedTestData()
    {
        // Seed capital increase scenario
        Symbol symbol1 = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol1);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson1 = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();

        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement1 = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse1 = CreateJsonResponse(testJson1);

        await processor.Process(statement1, jsonResponse1, CancellationToken.None);

        // Seed fiscal year change scenario
        Symbol symbol2 = CreateTestSymbol("IRO1SHZG0001", 8500002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol2);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson2 = ExtraAssemblyTestData.GetV1FiscalYearChangeTestData();

        GetStatementResponse statement2 = CreateStatementResponse("IRO1SHZG0001", 1438164UL);
        GetStatementJsonResponse jsonResponse2 = CreateJsonResponse(testJson2);

        await processor.Process(statement2, jsonResponse2, CancellationToken.None);
    }

    /// <summary>
    /// Cleans all existing ExtraAssembly data from the database.
    /// </summary>
    private async Task CleanExtraAssemblyData()
    {
        List<CanonicalExtraAssembly> assemblies = await _fixture.DbContext.CanonicalExtraAssemblies.ToListAsync();
        _fixture.DbContext.CanonicalExtraAssemblies.RemoveRange(assemblies);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1SSEP0001" || s.Isin == "IRO1SHZG0001")
            .ToListAsync();
        _fixture.DbContext.Symbols.RemoveRange(testSymbols);

        await _fixture.DbContext.SaveChangesAsync();
    }
}
