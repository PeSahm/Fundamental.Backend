using FluentAssertions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace IntegrationTests;

/// <summary>
/// Integration tests for database schema created by EF Core.
/// Verifies that all expected tables are created correctly when using EnsureCreatedAsync.
/// These tests validate the model configuration and ensure all entity types are properly mapped.
/// </summary>
public class MigrationIntegrationTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public MigrationIntegrationTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Database_ShouldCreateSymbolTable()
    {
        // Act
        bool exists = await TableExists("symbol");

        // Assert
        exists.Should().BeTrue("Symbol table should exist in database");
    }

    [Fact]
    public async Task Database_ShouldCreateBalanceSheetTable()
    {
        // Act
        bool exists = await TableExists("balance_sheet");

        // Assert
        exists.Should().BeTrue("BalanceSheet table should exist in database");
    }

    [Fact]
    public async Task Database_ShouldCreateIncomeStatementTable()
    {
        // Act
        bool exists = await TableExists("income_statement");

        // Assert
        exists.Should().BeTrue("IncomeStatement table should exist in database");
    }

    [Fact]
    public async Task Database_ShouldCreateCanonicalMonthlyActivityTable()
    {
        // Act
        bool exists = await TableExists("canonical_monthly_activity");

        // Assert
        exists.Should().BeTrue("CanonicalMonthlyActivity table should exist in database");
    }

    [Fact]
    public async Task Database_ShouldCreatePublisherTable()
    {
        // Act
        bool exists = await TableExists("publisher");

        // Assert
        exists.Should().BeTrue("Publisher table should exist in database");
    }

    [Fact]
    public async Task Database_ShouldCreateAllCoreEntities()
    {
        // Core tables that must exist for the application to function
        string[] coreTables =
        [
            "symbol",
            "balance_sheet",
            "income_statement",
            "publisher",
            "canonical_monthly_activity"
        ];

        // Act
        List<string> allTables = await GetAllTables();

        // Assert
        foreach (string table in coreTables)
        {
            allTables.Should().Contain(
                t => t.Equals(table, StringComparison.OrdinalIgnoreCase),
                $"Core table '{table}' should exist in database");
        }
    }

    private async Task<bool> TableExists(string tableName)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(_fixture.PostgresConnectionString);
        await connection.OpenAsync();

        const string sql = """
            SELECT EXISTS (
                SELECT 1
                FROM information_schema.tables
                WHERE table_name = @table
                AND table_type = 'BASE TABLE'
            );
            """;

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("table", tableName);

        object? result = await command.ExecuteScalarAsync();
        return result is true;
    }

    private async Task<List<string>> GetAllTables()
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(_fixture.PostgresConnectionString);
        await connection.OpenAsync();

        const string sql = """
            SELECT table_name
            FROM information_schema.tables
            WHERE table_type = 'BASE TABLE'
            AND table_schema NOT IN ('pg_catalog', 'information_schema')
            ORDER BY table_name;
            """;

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);

        List<string> tables = [];
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }
}
