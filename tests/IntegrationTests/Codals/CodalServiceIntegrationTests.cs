using FluentAssertions;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.Enums;
using IntegrationTests.Shared;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Codals;

/// <summary>
/// Integration tests for RawCodalJson caching behavior.
/// These tests verify that RawCodalJson entities are correctly stored and retrieved.
/// Note: PostgreSQL's jsonb type may reorder JSON keys, so we test for key content rather than exact string equality.
/// </summary>
public class CodalServiceIntegrationTests : FinancialStatementTestBase
{
    public CodalServiceIntegrationTests(TestFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task RawCodalJson_ShouldBeStoredCorrectlyInDatabase()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 987654321UL;
        string rawJson = """{"version":"V5","data":{"test":"value"}}""";

        RawCodalJson entity = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: DateTime.UtcNow,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/report"),
            publisherId: 12345,
            isin: "IRO1TEST0001",
            rawJson: rawJson,
            createdAt: DateTime.UtcNow);

        // Act
        await _fixture.DbContext.RawCodalJsons.AddAsync(entity);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        RawCodalJson? savedJson = await _fixture.DbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == traceNo);

        savedJson.Should().NotBeNull();
        savedJson!.TraceNo.Should().Be(traceNo);

        // PostgreSQL jsonb reorders keys, so check for content containment instead
        savedJson.RawJson.Should().Contain("version");
        savedJson.RawJson.Should().Contain("V5");
        savedJson.RawJson.Should().Contain("data");
        savedJson.Isin.Should().Be("IRO1TEST0001");
        savedJson.PublisherId.Should().Be(12345);
        savedJson.ReportingType.Should().Be(ReportingType.Production);
        savedJson.StatementLetterType.Should().Be(LetterType.InterimStatement);
    }

    [Fact]
    public async Task RawCodalJson_WhenTraceNoExists_ShouldBeRetrievable()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 123456789UL;
        string cachedJson = """{"version":"V5","cached":true}""";

        RawCodalJson existingCache = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: DateTime.UtcNow.AddDays(-1),
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/cached"),
            publisherId: 12346,
            isin: "IRO1TEST0002",
            rawJson: cachedJson,
            createdAt: DateTime.UtcNow.AddDays(-1));

        await _fixture.DbContext.RawCodalJsons.AddAsync(existingCache);
        await _fixture.DbContext.SaveChangesAsync();
        _fixture.DbContext.ChangeTracker.Clear();

        // Act - Simulate what CodalService.ProcessCodal does for caching lookup
        RawCodalJson? retrieved = await _fixture.DbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == traceNo);

        // Assert
        retrieved.Should().NotBeNull();
        retrieved!.RawJson.Should().Contain("cached");
        retrieved.RawJson.Should().Contain("true");
        retrieved.Isin.Should().Be("IRO1TEST0002");
    }

    [Fact]
    public async Task RawCodalJson_TraceNoShouldBeUnique()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 111222333UL;
        string rawJson1 = """{"first":true}""";
        string rawJson2 = """{"second":true}""";

        RawCodalJson entity1 = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: DateTime.UtcNow,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/report1"),
            publisherId: 12347,
            isin: "IRO1TEST0003",
            rawJson: rawJson1,
            createdAt: DateTime.UtcNow);

        RawCodalJson entity2 = new(
            id: Guid.NewGuid(),
            traceNo: traceNo, // Same TraceNo
            publishDate: DateTime.UtcNow,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/report2"),
            publisherId: 12348,
            isin: "IRO1TEST0004",
            rawJson: rawJson2,
            createdAt: DateTime.UtcNow);

        // Act
        await _fixture.DbContext.RawCodalJsons.AddAsync(entity1);
        await _fixture.DbContext.SaveChangesAsync();

        // Adding second entity with same TraceNo should fail
        await _fixture.DbContext.RawCodalJsons.AddAsync(entity2);
        Func<Task> act = async () => await _fixture.DbContext.SaveChangesAsync();

        // Assert
        await act.Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task RawCodalJson_ShouldStoreNullHtmlUrl()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 333444555UL;
        string rawJson = """{"test":"nullUrl"}""";

        RawCodalJson entity = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: DateTime.UtcNow,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: null, // Null URL
            publisherId: 12349,
            isin: "IRO1TEST0005",
            rawJson: rawJson,
            createdAt: DateTime.UtcNow);

        // Act
        await _fixture.DbContext.RawCodalJsons.AddAsync(entity);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        RawCodalJson? savedJson = await _fixture.DbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == traceNo);

        savedJson.Should().NotBeNull();
        savedJson!.HtmlUrl.Should().BeNull();
    }

    [Fact]
    public async Task RawCodalJson_PublishDateShouldBeUtc()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 444555666UL;
        DateTime publishDate = DateTime.UtcNow;
        string rawJson = """{"test":"utcDate"}""";

        RawCodalJson entity = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: publishDate,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/report"),
            publisherId: 12350,
            isin: "IRO1TEST0006",
            rawJson: rawJson,
            createdAt: DateTime.UtcNow);

        // Act
        await _fixture.DbContext.RawCodalJsons.AddAsync(entity);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        RawCodalJson? savedJson = await _fixture.DbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == traceNo);

        savedJson.Should().NotBeNull();
        savedJson!.PublishDate.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public async Task RawCodalJson_ShouldSupportMultipleRecordsWithDifferentTraceNos()
    {
        // Arrange
        await CleanTestData();
        List<RawCodalJson> entities = new();

        for (int i = 0; i < 10; i++)
        {
            entities.Add(new RawCodalJson(
                id: Guid.NewGuid(),
                traceNo: (ulong)(1000000 + i),
                publishDate: DateTime.UtcNow.AddDays(-i),
                reportingType: ReportingType.Production,
                statementLetterType: LetterType.InterimStatement,
                htmlUrl: new Uri($"http://test.codal.ir/report{i}"),
                publisherId: 10000 + i,
                isin: $"IRO1TEST{i:D4}",
                rawJson: $$$"""{"index": {{{i}}}}""",
                createdAt: DateTime.UtcNow));
        }

        // Act
        await _fixture.DbContext.RawCodalJsons.AddRangeAsync(entities);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        int count = await _fixture.DbContext.RawCodalJsons
            .CountAsync(x => x.TraceNo >= 1000000 && x.TraceNo < 1000010);

        count.Should().Be(10);
    }

    [Fact]
    public async Task RawCodalJson_UpdateShouldModifyExistingRecord()
    {
        // Arrange
        await CleanTestData();
        ulong traceNo = 666777888UL;
        string originalJson = """{"original":true}""";
        string updatedJson = """{"updated":true}""";

        RawCodalJson entity = new(
            id: Guid.NewGuid(),
            traceNo: traceNo,
            publishDate: DateTime.UtcNow,
            reportingType: ReportingType.Production,
            statementLetterType: LetterType.InterimStatement,
            htmlUrl: new Uri("http://test.codal.ir/report"),
            publisherId: 12352,
            isin: "IRO1TEST0008",
            rawJson: originalJson,
            createdAt: DateTime.UtcNow);

        await _fixture.DbContext.RawCodalJsons.AddAsync(entity);
        await _fixture.DbContext.SaveChangesAsync();

        // Act - Update the entity
        entity.Update(
            DateTime.UtcNow.AddHours(1),
            updatedJson,
            "IRO1TEST0008",
            DateTime.UtcNow);

        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        RawCodalJson? savedJson = await _fixture.DbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == traceNo);

        savedJson.Should().NotBeNull();
        savedJson!.RawJson.Should().Contain("updated");
    }

    private async Task CleanTestData()
    {
        _fixture.DbContext.RawCodalJsons.RemoveRange(_fixture.DbContext.RawCodalJsons);
        await _fixture.DbContext.SaveChangesAsync();
        _fixture.DbContext.ChangeTracker.Clear();
    }
}
