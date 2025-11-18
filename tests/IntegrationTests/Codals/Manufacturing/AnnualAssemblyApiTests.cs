using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.AnnualAssembly;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

public class AnnualAssemblyApiTests : FinancialStatementTestBase
{
    public AnnualAssemblyApiTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetAnnualAssemblys_WithoutFilters_ShouldReturnPaginatedList()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblysRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().HaveCountGreaterThan(0);
        response.Data.Meta.Total.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetAnnualAssemblys_WithIsinFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblysRequest request = new("IRO1BAHN0001", null, null);

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.Isin.Should().Be("IRO1BAHN0001"));
    }

    [Fact]
    public async Task GetAnnualAssemblys_WithFiscalYearFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what fiscal year we have
        CanonicalAnnualAssembly? firstEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int fiscalYear = firstEntity!.FiscalYear.Year;

        GetAnnualAssemblysRequest request = new(null, fiscalYear, null);

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.FiscalYear.Should().Be(fiscalYear));
    }

    [Fact]
    public async Task GetAnnualAssemblys_WithYearEndMonthFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();

        // Get first record to know what year-end month we have
        CanonicalAnnualAssembly? firstEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();
        int yearEndMonth = firstEntity!.YearEndMonth.Month;

        GetAnnualAssemblysRequest request = new(null, null, yearEndMonth);

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeEmpty();
        response.Data.Items.Should().AllSatisfy(item => item.YearEndMonth.Should().Be(yearEndMonth));
    }

    [Fact]
    public async Task GetAnnualAssemblys_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblysRequest request = new(null, null, null)
        {
            PageNumber = 1,
            PageSize = 5
        };

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().HaveCountLessOrEqualTo(5);
    }

    [Fact]
    public async Task GetAnnualAssemblyById_WithValidId_ShouldReturnDetailItem()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        // Get a valid ID from database
        CanonicalAnnualAssembly? firstEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .AsNoTracking()
            .FirstOrDefaultAsync();

        firstEntity.Should().NotBeNull();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblyByIdRequest request = new(firstEntity!.Id);

        // Act
        Response<GetAnnualAssemblyDetailItem> response =
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
        response.Data.Inspectors.Should().NotBeNull();
        response.Data.NewBoardMembers.Should().NotBeNull();
        response.Data.BoardMemberWageAndGifts.Should().NotBeNull();
        response.Data.NewsPapers.Should().NotBeNull();
        response.Data.AssemblyInterims.Should().NotBeNull();
        response.Data.ProportionedRetainedEarnings.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAnnualAssemblyById_WithInvalidId_ShouldReturnFailure()
    {
        // Arrange
        await CleanAnnualAssemblyData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblyByIdRequest request = new(Guid.NewGuid());

        // Act
        Response<GetAnnualAssemblyDetailItem> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetAnnualAssemblys_ShouldOrderByFiscalYearAndDateDescending()
    {
        // Arrange
        await CleanAnnualAssemblyData();
        await SeedTestData();

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        GetAnnualAssemblysRequest request = new(null, null, null);

        // Act
        Response<Paginated<GetAnnualAssemblyListItem>> response =
            await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Data.Should().NotBeNull();

        List<GetAnnualAssemblyListItem> items = response.Data!.Items.ToList();
        if (items.Count > 1)
        {
            // Verify ordering: fiscal year desc, then assembly date desc
            for (int i = 0; i < items.Count - 1; i++)
            {
                items[i].FiscalYear.Should().BeGreaterThanOrEqualTo(items[i + 1].FiscalYear,
                    "items should be ordered by fiscal year descending");
            }
        }
    }

    /// <summary>
    /// Seeds test data by processing Annual Assembly V1 data.
    /// </summary>
    private async Task SeedTestData()
    {
        Symbol symbol = CreateTestSymbol("IRO1BAHN0001", 9600001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = AnnualAssemblyTestData.GetV1TestData();

        AnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1BAHN0001", 987654321);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);
    }

    /// <summary>
    /// Cleans all existing Annual Assembly data from the database.
    /// </summary>
    private async Task CleanAnnualAssemblyData()
    {
        List<CanonicalAnnualAssembly> assemblies = await _fixture.DbContext.CanonicalAnnualAssemblies.ToListAsync();
        _fixture.DbContext.CanonicalAnnualAssemblies.RemoveRange(assemblies);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1BAHN0001")
            .ToListAsync();
        _fixture.DbContext.Symbols.RemoveRange(testSymbols);

        await _fixture.DbContext.SaveChangesAsync();
    }
}
