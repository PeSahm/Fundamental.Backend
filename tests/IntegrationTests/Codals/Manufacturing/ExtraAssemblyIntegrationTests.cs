using System.Net;
using FluentAssertions;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAssembly;
using Fundamental.IntegrationTests.TestData;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace IntegrationTests.Codals.Manufacturing;

/// <summary>
/// Integration tests for ExtraAssembly V1 processor.
/// Tests complete processing pipeline: deserialization, mapping, and persistence.
/// </summary>
public class ExtraAssemblyIntegrationTests : FinancialStatementTestBase
{
    public ExtraAssemblyIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldStoreCanonicalData()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("V1");
        storedEntity.FiscalYear.Year.Should().BeGreaterThan((ushort)0);
        storedEntity.YearEndMonth.Month.Should().BeInRange((ushort)1, (ushort)12);
        storedEntity.Currency.Should().Be(IsoCurrency.IRR);
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldMapAllCollections()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify all collection properties are populated
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        storedEntity.Should().NotBeNull();

        // Verify owned entities and collections exist
        storedEntity!.ParentAssemblyInfo.Should().NotBeNull();
        storedEntity.ParentAssemblyInfo!.SessionOrders.Should().NotBeNull();
        storedEntity.AssemblyChiefMembersInfo.Should().NotBeNull();
        storedEntity.ShareHolders.Should().NotBeNull();
        storedEntity.AssemblyBoardMembers.Should().NotBeNull();
        storedEntity.ExtraAssemblyIncreaseCapitals.Should().NotBeNull();
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldMapAssemblyMetadata()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify assembly metadata
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        storedEntity.Should().NotBeNull();

        // Verify AssemblyResultType enum is mapped (from ParentAssemblyInfo)
        storedEntity!.ParentAssemblyInfo.Should().NotBeNull();
        storedEntity.ParentAssemblyInfo!.AssemblyResultType.Should().BeOneOf(Enum.GetValues<AssemblyResultType>());

        // Verify assembly date is a valid UTC datetime
        storedEntity.AssemblyDate.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldMapBoardMemberEnums()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify board member enums are correctly mapped
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

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
    public async Task ProcessExtraAssemblyV1_ShouldMapAttendees()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify attendee entities
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        storedEntity.Should().NotBeNull();

        // Verify attendee fields exist (nullable)
        // These should be null or have valid AssemblyAttendee structure
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
    public async Task ProcessExtraAssemblyV1_ShouldHandleUpdate()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        // Act - Process first time
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Act - Process second time (update)
        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Should only have one record
        int count = await _fixture.DbContext.CanonicalExtraAssemblies
            .CountAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        count.Should().Be(1, "processor should update existing record, not create duplicate");
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldHandleCapitalIncreaseScenario()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SSEP0001", 8500001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SSEP0001", 1418249UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify capital increase details
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1418249UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.ExtraAssemblyIncreaseCapitals.Should().NotBeEmpty();

        // Verify CapitalIncreaseApprovalType is mapped correctly
        foreach (var capitalIncrease in storedEntity.ExtraAssemblyIncreaseCapitals)
        {
            capitalIncrease.Type.Should().BeOneOf(Enum.GetValues<CapitalIncreaseApprovalType>());
        }
    }

    [Fact]
    public async Task ProcessExtraAssemblyV1_ShouldHandleFiscalYearChangeScenario()
    {
        // Arrange
        await CleanExtraAssemblyData();
        Symbol symbol = CreateTestSymbol("IRO1SHZG0001", 8500002UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = ExtraAssemblyTestData.GetV1FiscalYearChangeTestData();
        SetupApiResponse("extra-assembly", testJson);

        // Act
        ExtraAssemblyV1Processor processor = new(
            _fixture.Services.GetRequiredService<IServiceScopeFactory>(),
            _fixture.Services.GetRequiredService<ICanonicalMappingServiceFactory>());

        GetStatementResponse statement = CreateStatementResponse("IRO1SHZG0001", 1438164UL);
        GetStatementJsonResponse jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert - Verify fiscal year change scenario
        CanonicalExtraAssembly? storedEntity = await _fixture.DbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 1438164UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("V1");
        storedEntity.FiscalYear.Year.Should().BeGreaterThan((ushort)0);
    }

    [Fact]
    public void ExtraAssemblyDetector_ShouldDetectV1()
    {
        // Arrange
        string v1Json = ExtraAssemblyTestData.GetV1CapitalIncreaseTestData();
        ExtraAssemblyDetector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(v1Json);

        // Assert
        detectedVersion.Should().Be(CodalVersion.V1, "V1 JSON (with 'parentAssembly') should be detected as V1");
    }

    [Fact]
    public void ExtraAssemblyDetector_ShouldReturnNoneForInvalidJson()
    {
        // Arrange
        string invalidJson = "{\"someOtherSection\": {}}";
        ExtraAssemblyDetector detector = new();

        // Act
        CodalVersion detectedVersion = detector.DetectVersion(invalidJson);

        // Assert
        detectedVersion.Should().Be(CodalVersion.None, "JSON without 'parentAssembly' should return None");
    }

    /// <summary>
    /// Cleans all existing ExtraAssembly data from the database.
    /// </summary>
    private async Task CleanExtraAssemblyData()
    {
        _fixture.DbContext.CanonicalExtraAssemblies.RemoveRange(_fixture.DbContext.CanonicalExtraAssemblies);

        // Also clean up test symbols to avoid duplicate ISIN conflicts
        List<Symbol> testSymbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin == "IRO1SSEP0001" || s.Isin == "IRO1SHZG0001")
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
