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
    public async Task Database_ShouldCreateSectorTable()
    {
        // Act
        bool exists = await TableExists("sector");

        // Assert
        exists.Should().BeTrue("Sector table should exist in database");
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
    public async Task SymbolTable_ShouldHaveSectorIdColumn()
    {
        // Act
        List<string> columns = await GetColumnsForTable("symbol");

        // Assert - EnsureCreatedAsync uses "Sector_id" (PascalCase shadow property)
        columns.Should().Contain(
            c => c.Equals("sector_id", StringComparison.OrdinalIgnoreCase) ||
                 c.Equals("Sector_id", StringComparison.OrdinalIgnoreCase),
            "Symbol table should have 'sector_id' column for Sector FK");
    }

    [Fact]
    public async Task SectorTable_ShouldHaveRequiredColumns()
    {
        // Act
        List<string> columns = await GetColumnsForTable("sector");

        // Assert - Column names may be PascalCase (EnsureCreatedAsync) or snake_case (migrations)
        columns.Should().Contain(
            c => c.Equals("id", StringComparison.OrdinalIgnoreCase) ||
                 c.Equals("Id", StringComparison.OrdinalIgnoreCase),
            "Sector table should have 'id' column");
        columns.Should().Contain(
            c => c.Equals("name", StringComparison.OrdinalIgnoreCase) ||
                 c.Equals("Name", StringComparison.OrdinalIgnoreCase),
            "Sector table should have 'name' column");
    }

    [Fact]
    public async Task Database_ShouldHaveSymbolToSectorForeignKey()
    {
        // Act
        List<string> foreignKeys = await GetForeignKeysForTable("symbol");

        // Assert
        foreignKeys.Should().Contain(
            fk => fk.Contains("sector", StringComparison.OrdinalIgnoreCase),
            "Symbol table should have FK constraint to Sector table");
    }

    [Fact]
    public async Task Database_ShouldCreateAllCoreEntities()
    {
        // Core tables that must exist for the application to function
        string[] coreTables =
        [
            "symbol",
            "sector",
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

    private async Task<List<string>> GetColumnsForTable(string tableName)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(_fixture.PostgresConnectionString);
        await connection.OpenAsync();

        const string sql = """
            SELECT column_name
            FROM information_schema.columns
            WHERE table_name = @table
            ORDER BY ordinal_position;
            """;

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("table", tableName);

        List<string> columns = [];
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            columns.Add(reader.GetString(0));
        }

        return columns;
    }

    private async Task<List<string>> GetForeignKeysForTable(string tableName)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(_fixture.PostgresConnectionString);
        await connection.OpenAsync();

        const string sql = """
            SELECT tc.constraint_name
            FROM information_schema.table_constraints tc
            WHERE tc.table_name = @table
            AND tc.constraint_type = 'FOREIGN KEY'
            ORDER BY tc.constraint_name;
            """;

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("table", tableName);

        List<string> foreignKeys = [];
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            foreignKeys.Add(reader.GetString(0));
        }

        return foreignKeys;
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
