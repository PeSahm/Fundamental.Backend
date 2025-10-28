using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FluentAssertions;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PerformanceTests;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class FinancialStatementProcessingBenchmarks
{
    private FundamentalDbContext _context;
    private List<Symbol> _testSymbols;
    private List<BalanceSheet> _testBalanceSheets;

    [GlobalSetup]
    public async Task Setup()
    {
        // Setup in-memory database for performance testing
        var options = new DbContextOptionsBuilder<FundamentalDbContext>()
            .UseInMemoryDatabase("PerformanceTestDb")
            .Options;

        _context = new FundamentalDbContext(options);

        // Generate test data
        _testSymbols = GenerateTestSymbols(100);
        _testBalanceSheets = GenerateTestBalanceSheets(_testSymbols, 5); // 5 years each

        // Seed database
        await _context.Symbols.AddRangeAsync(_testSymbols);
        await _context.BalanceSheets.AddRangeAsync(_testBalanceSheets);
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task QueryBalanceSheetsBySymbol()
    {
        foreach (var symbol in _testSymbols.Take(10)) // Test with first 10 symbols
        {
            var balanceSheets = await _context.BalanceSheets
                .Where(bs => bs.Symbol.Id == symbol.Id)
                .OrderBy(bs => bs.FiscalYear)
                .ToListAsync();

            balanceSheets.Should().NotBeEmpty();
        }
    }

    [Benchmark]
    public async Task BulkInsertBalanceSheets()
    {
        var newBalanceSheets = GenerateTestBalanceSheets(_testSymbols.Take(50), 1);

        await _context.BalanceSheets.AddRangeAsync(newBalanceSheets);
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task ComplexFinancialQuery()
    {
        // Simulate complex financial analysis query
        var results = await _context.BalanceSheets
            .Include(bs => bs.Symbol)
            .Where(bs => bs.IsAudited)
            .GroupBy(bs => bs.Symbol.Id)
            .Select(g => new
            {
                SymbolId = g.Key,
                AverageValue = g.SelectMany(bs => bs.Details).Average(d => d.Value.Value),
                RecordCount = g.Count()
            })
            .ToListAsync();

        results.Should().NotBeEmpty();
    }

    private List<Symbol> GenerateTestSymbols(int count)
    {
        var symbols = new List<Symbol>();
        for (int i = 0; i < count; i++)
        {
            symbols.Add(new Symbol(
                Guid.NewGuid(),
                $"TEST{i:D6}",
                $"TSE{i:D6}",
                $"Test Company {i} EN",
                $"TEST{i:D6}",
                $"Test Company {i}",
                $"Test Company {i}",
                $"TC{i:D6}",
                $"شرکت تست {i}",
                $"TEST{i:D6}ISIN",
                (ulong)(1000000 + i * 1000),
                "01",
                "001",
                ProductType.Equity,
                ExchangeType.TSE,
                null,
                DateTime.UtcNow
            ));
        }
        return symbols;
    }

    private List<BalanceSheet> GenerateTestBalanceSheets(IEnumerable<Symbol> symbols, int yearsBack)
    {
        var balanceSheets = new List<BalanceSheet>();
        var random = new Random(42); // Fixed seed for reproducible results

        foreach (var symbol in symbols)
        {
            for (int year = 0; year < yearsBack; year++)
            {
                var balanceSheet = new BalanceSheet(
                    Guid.NewGuid(),
                    symbol,
                    (ulong)random.Next(1000, 999999),
                    $"http://test.uri/{year}",
                    new FiscalYear(DateTime.Now.Year - year),
                    new StatementMonth(12), // December
                    new StatementMonth(12), // December
                    random.NextDouble() > 0.2, // 80% audited
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(-random.Next(1, 365))
                );

                // Add some detail records
                for (int category = 0; category < 5; category++) // Multiple rows per year
                {
                    var value = random.Next(1000000, 100000000);
                    var detail = new BalanceSheetDetail(
                        Guid.NewGuid(),
                        balanceSheet,
                        (ushort)category,
                        (ushort)category,
                        (BalanceSheetCategory)category,
                        $"Test Description {category}",
                        new SignedCodalMoney(value, IsoCurrency.IRR),
                        DateTime.UtcNow
                    );
                    balanceSheet.Details.Add(detail);
                }

                balanceSheets.Add(balanceSheet);
            }
        }

        return balanceSheets;
    }
}

public class PerformanceTests : IAsyncLifetime
{
    private FundamentalDbContext _context;

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<FundamentalDbContext>()
            .UseInMemoryDatabase("PerfTestDb")
            .Options;

        _context = new FundamentalDbContext(options);
        await SeedTestDataAsync();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    [Fact]
    public async Task ProcessLargeDataset_ShouldCompleteWithinTimeLimit()
    {
        // Arrange
        var largeDataset = GenerateLargeBalanceSheetDataset(10000);
        var stopwatch = Stopwatch.StartNew();

        // Act
        await _context.BalanceSheets.AddRangeAsync(largeDataset);
        await _context.SaveChangesAsync();
        stopwatch.Stop();

        // Assert
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(30));
    }

    [Fact]
    public async Task MemoryUsage_ShouldRemainStable()
    {
        // Arrange
        var initialMemory = GC.GetTotalMemory(true);
        var dataset = GenerateLargeBalanceSheetDataset(5000);

        // Act
        await _context.BalanceSheets.AddRangeAsync(dataset);
        await _context.SaveChangesAsync();
        var finalMemory = GC.GetTotalMemory(true);

        // Assert
        var memoryIncrease = finalMemory - initialMemory;
        memoryIncrease.Should().BeLessThan(50 * 1024 * 1024); // 50MB limit
    }

    [Fact]
    public async Task ConcurrentQueries_ShouldPerformWell()
    {
        // Arrange
        var symbolIds = (await _context.Symbols.Select(s => s.Id).ToListAsync()).Take(10);

        // Act - Simple count queries to avoid complex value object issues
        var queryTasks = symbolIds.Select(async symbolId =>
        {
            return await _context.BalanceSheets
                .CountAsync(bs => bs.Symbol.Id == symbolId);
        });

        var stopwatch = Stopwatch.StartNew();
        var results = await Task.WhenAll(queryTasks);
        stopwatch.Stop();

        // Assert
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
        results.Should().AllSatisfy(r => r.Should().BeGreaterThan(0));
    }

    private async Task SeedTestDataAsync()
    {
        var symbols = new List<Symbol>();
        for (int i = 0; i < 100; i++)
        {
            symbols.Add(new Symbol(
                Guid.NewGuid(),
                $"PERF{i:D6}",
                $"TSE{i:D6}",
                $"Performance Test Company {i} EN",
                $"PERF{i:D6}",
                $"Performance Test Company {i}",
                $"Performance Test Company {i}",
                $"PTC{i:D6}",
                $"شرکت تست عملکرد {i}",
                $"PERF{i:D6}ISIN",
                (ulong)(1000000 + i * 1000),
                "01",
                "001",
                ProductType.Equity,
                ExchangeType.TSE,
                null,
                DateTime.UtcNow
            ));
        }

        await _context.Symbols.AddRangeAsync(symbols);
        await _context.SaveChangesAsync();

        // Create balance sheets for the symbols
        var balanceSheets = GenerateLargeBalanceSheetDataset(1000);
        await _context.BalanceSheets.AddRangeAsync(balanceSheets);
        await _context.SaveChangesAsync();
    }

    private List<BalanceSheet> GenerateLargeBalanceSheetDataset(int count)
    {
        var balanceSheets = new List<BalanceSheet>();
        var random = new Random(12345);
        var symbols = _context.Symbols.ToList();

        for (int i = 0; i < count; i++)
        {
            var symbol = symbols[random.Next(symbols.Count)];

            var balanceSheet = new BalanceSheet(
                Guid.NewGuid(),
                symbol,
                (ulong)random.Next(1000, 999999),
                $"http://test.uri/{i}",
                new FiscalYear(DateTime.Now.Year - random.Next(0, 5)),
                new StatementMonth(12), // December
                new StatementMonth(12), // December
                random.NextDouble() > 0.2,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(-random.Next(1, 365))
            );

            // Add a single detail record for simplicity in performance tests
            var value = random.Next(1000000, 100000000);
            var detail = new BalanceSheetDetail(
                Guid.NewGuid(),
                balanceSheet,
                (ushort)random.Next(0, 10),
                (ushort)random.Next(0, 10),
                BalanceSheetCategory.Assets,
                $"Large Dataset Description {i}",
                new SignedCodalMoney(value, IsoCurrency.IRR),
                DateTime.UtcNow
            );
            balanceSheet.Details.Add(detail);

            balanceSheets.Add(balanceSheet);
        }

        return balanceSheets;
    }
}