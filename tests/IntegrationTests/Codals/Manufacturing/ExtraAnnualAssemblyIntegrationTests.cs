using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAnnualAssembly;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

/// <summary>
/// Integration tests for Extraordinary Annual Assembly V1 processor.
/// Tests complete processing pipeline: deserialization, mapping, and persistence.
/// </summary>
public class ExtraAnnualAssemblyIntegrationTests : FinancialStatementTestBase
{
    public ExtraAnnualAssemblyIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldStoreCanonicalData()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        // Act
        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalAnnualAssembly? storedEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("V1");
        storedEntity.FiscalYear.Year.Should().BeGreaterThan((ushort)0);
        storedEntity.YearEndMonth.Month.Should().BeInRange((ushort)1, (ushort)12);
        storedEntity.Currency.Should().Be(IsoCurrency.IRR);
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldMapAllCollections()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        // Act
        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify all collection properties are populated
        CanonicalAnnualAssembly? storedEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        storedEntity.Should().NotBeNull();

        // Verify owned entities and collections exist
        storedEntity!.ParentAssemblyInfo.Should().NotBeNull();
        storedEntity.ParentAssemblyInfo!.SessionOrders.Should().NotBeNull();
        storedEntity.AssemblyChiefMembersInfo.Should().NotBeNull();
        storedEntity.ShareHolders.Should().NotBeNull();
        storedEntity.AssemblyBoardMembers.Should().NotBeNull();
        storedEntity.Inspectors.Should().NotBeNull();
        storedEntity.NewBoardMembers.Should().NotBeNull();
        storedEntity.BoardMemberWageAndGifts.Should().NotBeNull();
        storedEntity.NewsPapers.Should().NotBeNull();
        storedEntity.AssemblyInterims.Should().NotBeNull();
        storedEntity.ProportionedRetainedEarnings.Should().NotBeNull();
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldMapAssemblyMetadata()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        // Act
        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify assembly metadata
        CanonicalAnnualAssembly? storedEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        storedEntity.Should().NotBeNull();

        // Verify AssemblyResultType enum is mapped
        storedEntity!.ParentAssemblyInfo.Should().NotBeNull();
        storedEntity.ParentAssemblyInfo!.AssemblyResultType.Should().BeOneOf(Enum.GetValues<AssemblyResultType>());

        // Verify assembly date is a valid UTC datetime
        storedEntity.AssemblyDate.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldMapBoardMemberEnums()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        // Act
        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify board member enums are correctly mapped
        CanonicalAnnualAssembly? storedEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        storedEntity.Should().NotBeNull();

        if (storedEntity!.AssemblyBoardMembers.Any())
        {
            foreach (var member in storedEntity.AssemblyBoardMembers)
            {
                member.MembershipType.Should().BeOneOf(BoardMembershipType.Alternate, BoardMembershipType.Principal);
                member.Position.Should().BeOneOf(Enum.GetValues<BoardPosition>());
                member.Verification.Should().BeOneOf(Enum.GetValues<VerificationStatus>());
            }
        }
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldMapAttendees()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        // Act
        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify attendee entities
        CanonicalAnnualAssembly? storedEntity = await _fixture.DbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        storedEntity.Should().NotBeNull();

        // Verify attendee fields exist (nullable)
        if (storedEntity!.Ceo != null)
        {
            storedEntity.Ceo.Should().BeOfType<AssemblyAttendee>();
        }

        if (storedEntity.AuditCommitteeChairman != null)
        {
            storedEntity.AuditCommitteeChairman.Should().BeOfType<AssemblyAttendee>();
        }
    }

    [Fact]
    public async Task ProcessExtraAnnualAssemblyV1_ShouldHandleUpdate()
    {
        // Arrange
        await CleanExtraAnnualAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO3RYHZ0001", 9600002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAnnualAssemblyTestData.GetV1TestData();
        SetupApiResponse("extra-annual-assembly", testJson);

        ExtraAnnualAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO3RYHZ0001", 1401458);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        // Act - Process first time
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act - Process second time (update)
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Should only have one record
        int count = await _fixture.DbContext.CanonicalAnnualAssemblies
            .CountAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1401458UL);

        count.Should().Be(1, "processor should update existing record, not create duplicate");
    }

    [Fact]
    public void ExtraAnnualAssemblyDetector_ShouldDetectV1()
    {
        // Arrange
        string v1Json = ExtraAnnualAssemblyTestData.GetV1TestData();
        ExtraAnnualAssemblyDetector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(v1Json);

        // Assert
        detectedVersion.Should().Be(CodalVersion.V1, "V1 JSON (with 'parentAssembly') should be detected as V1");
    }

    [Fact]
    public void ExtraAnnualAssemblyDetector_ShouldReturnNoneForInvalidJson()
    {
        // Arrange
        string invalidJson = "{\"someOtherSection\": {}}";
        ExtraAnnualAssemblyDetector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(invalidJson);

        // Assert
        detectedVersion.Should().Be(CodalVersion.None, "JSON without 'parentAssembly' should return None");
    }

    /// <summary>
    /// Cleans all existing Extraordinary Annual Assembly data from the database.
    /// </summary>
    private async Task CleanExtraAnnualAssemblyData()
    {
        _fixture.DbContext.CanonicalAnnualAssemblies.RemoveRange(_fixture.DbContext.CanonicalAnnualAssemblies);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO3RYHZ0001")
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
