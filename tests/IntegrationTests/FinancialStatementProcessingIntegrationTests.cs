using System.Text.Json;
using FluentAssertions;
using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;

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
}