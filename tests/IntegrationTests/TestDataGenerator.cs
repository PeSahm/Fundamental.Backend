using System.Globalization;

using Bogus; // Commented out due to complex entity constructors
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Symbols.Entities;

namespace IntegrationTests;

public static class TestDataGenerator
{
    // private static readonly Faker _faker = new Faker("en"); // Commented out

    // Commented out complex data generation due to entity constructor requirements
    // public static IEnumerable<Symbol> GenerateSymbols(int count = 10)
    // {
    //     var faker = new Faker<Symbol>()
    //         .RuleFor(s => s.Code, f => f.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"))
    //         .RuleFor(s => s.Name, f => f.Company.CompanyName())
    //         .RuleFor(s => s.SymbolType, f => f.PickRandom<SymbolType>());
    //
    //     return faker.Generate(count);
    // }

    // Commented out due to BalanceSheet constructor requirements
    // public static IEnumerable<BalanceSheet> GenerateBalanceSheets(IEnumerable<Symbol> symbols, int yearsBack = 3)
    // {
    //     ...
    // }

    // Commented out due to IncomeStatement constructor requirements
    // public static IEnumerable<IncomeStatement> GenerateIncomeStatements(IEnumerable<Symbol> symbols, int yearsBack = 3)
    // {
    //     ...
    // }

    // Commented out due to complex entity requirements
    // public static async Task SeedDatabaseAsync(FundamentalDbContext context, int symbolCount = 10, int yearsBack = 3)
    // {
    //     // Generate and seed symbols
    //     var symbols = GenerateSymbols(symbolCount).ToList();
    //     await context.Symbols.AddRangeAsync(symbols);
    //     await context.SaveChangesAsync();
    //
    //     // Generate and seed balance sheets
    //     var balanceSheets = GenerateBalanceSheets(symbols, yearsBack);
    //     await context.BalanceSheets.AddRangeAsync(balanceSheets);
    //
    //     // Generate and seed income statements
    //     var incomeStatements = GenerateIncomeStatements(symbols, yearsBack);
    //     await context.IncomeStatements.AddRangeAsync(incomeStatements);
    //
    //     await context.SaveChangesAsync();
    // }
    public static string GenerateFinancialStatementJson(string type, int version = 1)
    {
        return type.ToLower() switch
        {
            "balancesheet" => GenerateBalanceSheetJson(version),
            "incomestatement" => GenerateIncomeStatementJson(version),
            _ => throw new ArgumentException($"Unknown statement type: {type}")
        };
    }

    private static string GenerateBalanceSheetJson(int version)
    {
        if (version == 1)
        {
            return @"{
                ""version"": ""1.0"",
                ""companyCode"": ""123456789"",
                ""fiscalYear"": 1402,
                ""reportDate"": ""1402/12/29"",
                ""isAudited"": true,
                ""assets"": {
                    ""currentAssets"": 500000000,
                    ""nonCurrentAssets"": 800000000,
                    ""totalAssets"": 1300000000
                },
                ""liabilities"": {
                    ""currentLiabilities"": 300000000,
                    ""nonCurrentLiabilities"": 400000000,
                    ""totalLiabilities"": 700000000
                },
                ""equity"": {
                    ""totalEquity"": 600000000
                }
            }";
        }

        // Version 2.0
        return @"{
            ""version"": ""2.0"",
            ""metadata"": {
                ""companyCode"": ""123456789"",
                ""fiscalYear"": 1402,
                ""period"": ""annual"",
                ""currency"": ""IRR""
            },
            ""data"": {
                ""balanceSheet"": {
                    ""assets"": {
                        ""current"": 500000000,
                        ""nonCurrent"": 800000000
                    },
                    ""liabilitiesAndEquity"": {
                        ""liabilities"": {
                            ""current"": 300000000,
                            ""nonCurrent"": 400000000
                        },
                        ""equity"": 600000000
                    }
                }
            },
            ""audit"": {
                ""isAudited"": true,
                ""auditor"": ""Test Audit Firm""
            }
        }";
    }

    private static string GenerateIncomeStatementJson(int version)
    {
        if (version == 1)
        {
            return @"{
                ""version"": ""1.0"",
                ""companyCode"": ""123456789"",
                ""fiscalYear"": 1402,
                ""revenue"": 1000000000,
                ""costOfGoodsSold"": 600000000,
                ""grossProfit"": 400000000,
                ""operatingExpenses"": 150000000,
                ""operatingIncome"": 250000000,
                ""netIncome"": 200000000,
                ""isAudited"": true
            }";
        }

        // Version 2.0
        return @"{
            ""version"": ""2.0"",
            ""metadata"": {
                ""companyCode"": ""123456789"",
                ""fiscalYear"": 1402,
                ""period"": ""annual""
            },
            ""data"": {
                ""incomeStatement"": {
                    ""revenue"": 1000000000,
                    ""expenses"": {
                        ""costOfGoodsSold"": 600000000,
                        ""operating"": 150000000
                    },
                    ""profits"": {
                        ""gross"": 400000000,
                        ""operating"": 250000000,
                        ""net"": 200000000
                    }
                }
            }
        }";
    }
}