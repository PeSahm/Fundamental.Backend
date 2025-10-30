# Financial Statement JSON Parser Implementation Prompt

## Overview

Create a complete implementation for parsing Iranian financial API JSON reports into canonical entities for a specific
financial statement type (e.g., BalanceSheet, IncomeStatement, CashFlow, etc.) that has multiple versions with different
structures.

## Context

Based on the MonthlyActivity implementation, create a similar pattern for [FINANCIAL_STATEMENT_TYPE] with versions V1-V5
that handles:

- Version-specific JSON structures
- Canonical data normalization
- Raw JSON storage with metadata
- Integration with existing factory patterns
- Comprehensive integration testing

## Required Components

### 1. Domain Entities

Create the following in `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/`:

#### Canonical[FinancialStatementType].cs

```csharp
public class Canonical[FinancialStatementType] : BaseEntity<Guid>
{
    // Navigation property to Symbol
    public Symbol Symbol { get; set; } = null!;

    // Basic metadata
    public long TraceNo { get; set; }
    public string Uri { get; set; } = null!;
    public string Version { get; set; } = null!;

    // Date/time fields
    public FiscalYear FiscalYear { get; set; } = null!;
    public StatementMonth ReportMonth { get; set; } = null!;
    public IsoCurrency Currency { get; set; }

    // Version-specific fields (use V5 as reference)
    // [Add all fields from V5 structure]

    // Owned collections for complex data
    public ICollection<[DataSection1]Item> [DataSection1]Items { get; set; } = new List<[DataSection1]Item>();
    public ICollection<[DataSection2]Item> [DataSection2]Items { get; set; } = new List<[DataSection2]Item>();
    // [Add all owned collections]
}
```

#### Raw[FinancialStatementType]Json.cs

```csharp
public class Raw[FinancialStatementType]Json : BaseEntity<Guid>
{
    public long TraceNo { get; set; }
    public Guid SymbolId { get; set; } // Shadow FK
    public DateOnly PublishDate { get; set; }
    public string Version { get; set; } = null!;
    public string RawJson { get; set; } = null!;
}
```

#### Owned Entity Classes

Create owned entity classes for each data section (e.g., `[DataSection1]Item.cs`).

### 2. EF Core Configuration

Create
`src/Infrastructure/Fundamental.Infrastructure/Configuration/Fundamental/Codals/Manufacturing/[FinancialStatementType]Configuration.cs`:

```csharp
public class Canonical[FinancialStatementType]Configuration : IEntityTypeConfiguration<Canonical[FinancialStatementType]>
{
    public void Configure(EntityTypeBuilder<Canonical[FinancialStatementType]> builder)
    {
        builder.ToTable("[financial_statement_type]s");

        // Shadow foreign key
        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id");

        // Value objects
        builder.OwnsOne(x => x.FiscalYear);
        builder.OwnsOne(x => x.ReportMonth);

        // Owned collections
        builder.OwnsMany(x => x.[DataSection1]Items, navigationBuilder => {
            navigationBuilder.ToJson();
            // Configure properties
        });

        // [Configure all owned collections]
    }
}

public class Raw[FinancialStatementType]JsonConfiguration : IEntityTypeConfiguration<Raw[FinancialStatementType]Json>
{
    public void Configure(EntityTypeBuilder<Raw[FinancialStatementType]Json> builder)
    {
        builder.ToTable("raw_[financial_statement_type]_jsons");

        builder.Property(x => x.RawJson).HasColumnType("jsonb");
    }
}
```

### 3. DTOs for Each Version

Create DTOs in `src/Application/Fundamental.Application/Codals/Dto/[FinancialStatementType]s/V[1-5]/`:

#### V5 DTOs (Reference Implementation)

- `Codal[FinancialStatementType]V5.cs` - Main DTO
- `[FinancialStatementType]DtoV5.cs` - MonthlyActivity DTO
- Section-specific DTOs (e.g., `[Section1]V5.cs`, `[Section2]V5.cs`)
- `[FinancialStatementType]V5ColumnId.cs` - Enums for column IDs

#### V4-V1 DTOs

- Simplified versions based on actual JSON structures
- Use appropriate JSON attributes (Newtonsoft.Json for processors)

### 4. Processor Classes

Create processors in
`src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Processors/[FinancialStatementType]s/`:

#### [FinancialStatementType]V5Processor.cs

```csharp
public class [FinancialStatementType]V5Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.[AppropriateType];
    public static LetterType LetterType => LetterType.[FinancialStatementType];
    public static CodalVersion CodalVersion => CodalVersion.V5;
    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        // 1. Deserialize JSON
        var monthlyActivity = JsonConvert.DeserializeObject<Codal[FinancialStatementType]V5>(model.Json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        // 2. Validate data
        if (monthlyActivity?.[FinancialStatementType] is null) return;

        // 3. Extract fiscal year and report month
        var yearData = monthlyActivity.[FinancialStatementType].[Section]?.YearData.FirstOrDefault();
        int fiscalYear = yearData?.FiscalYear ?? DateTime.Now.Year;
        int reportMonth = yearData?.ReportMonth ?? 1;

        // 4. Get/create symbol
        using var scope = serviceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        var symbol = await dbContext.Symbols.FirstAsync(x => x.Isin == statement.Isin, cancellationToken);

        // 5. Store raw JSON
        var existingRawJson = await dbContext.Raw[FinancialStatementType]Jsons
            .FirstOrDefaultAsync(x => x.SymbolId == symbol.Id && x.Version == "5", cancellationToken);

        if (existingRawJson == null)
        {
            var rawJson = new Raw[FinancialStatementType]Json
            {
                TraceNo = (long)statement.TracingNo,
                SymbolId = symbol.Id,
                PublishDate = DateOnly.FromDateTime(statement.PublishDateMiladi),
                Version = "5",
                RawJson = JsonConvert.SerializeObject(monthlyActivity.[FinancialStatementType].[FinancialStatementType])
            };
            dbContext.Add(rawJson);
        }
        else if (existingRawJson.TraceNo <= (long)statement.TracingNo)
        {
            existingRawJson.TraceNo = (long)statement.TracingNo;
            existingRawJson.PublishDate = DateOnly.FromDateTime(statement.PublishDateMiladi);
            existingRawJson.RawJson = JsonConvert.SerializeObject(monthlyActivity.[FinancialStatementType].[FinancialStatementType]);
        }

        // 6. Create canonical entity
        var canonical = new Canonical[FinancialStatementType]
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = "5",
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            ReportMonth = reportMonth,
            // [Set other properties]
        };

        // 7. Map data sections
        // [Map each data section to owned collections]

        // 8. Handle existing records
        var existingCanonical = await dbContext.Canonical[FinancialStatementType]s
            .FirstOrDefaultAsync(x => x.Symbol.Isin == statement.Isin &&
                                    x.FiscalYear.Year == fiscalYear &&
                                    x.ReportMonth.Month == reportMonth, cancellationToken);

        if (existingCanonical == null)
        {
            dbContext.Add(canonical);
        }
        else if (existingCanonical.TraceNo <= statement.TracingNo)
        {
            UpdateCanonical[FinancialStatementType](existingCanonical, canonical);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void UpdateCanonical[FinancialStatementType](Canonical[FinancialStatementType] existing, Canonical[FinancialStatementType] updated)
    {
        existing.TraceNo = updated.TraceNo;
        existing.Uri = updated.Uri;
        // [Update other properties and collections]
    }
}
```

#### V4-V1 Processors

- Similar structure but with version-specific logic
- V4-V1: ReportMonth = 1 (annual reporting)
- Version-specific fiscal year calculations:
    - V5/V4: fiscalYear = extracted year
    - V3/V2: fiscalYear = extracted year + 1
    - V1: fiscalYear = extracted year + 2

### 5. Version Detection

Update
`src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Processors/[FinancialStatementType]Detector.cs`:

```csharp
public class [FinancialStatementType]Detector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        // V5: Has [specific_field] or [structure_indicator]
        if (json.Contains("\"[v5_indicator]\"")) return CodalVersion.V5;

        // V4: Has [v4_specific_field] but not [v5_indicator]
        if (json.Contains("\"[v4_indicator]\"")) return CodalVersion.V4;

        // V3: Has [v3_structure]
        if (json.Contains("\"[v3_indicator]\"")) return CodalVersion.V3;

        // V2: Has [v2_structure]
        if (json.Contains("\"[v2_indicator]\"")) return CodalVersion.V2;

        // V1: Default/fallback
        return CodalVersion.V1;
    }
}
```

### 6. Factory Registration

Update
`src/Infrastructure/Fundamental.Infrastructure/Extensions/Codals/Manufacturing/ServicesConfigurationExtensions.cs`:

```csharp
public static IServiceCollection Add[FinancialStatementType]Processors(this IServiceCollection serviceCollection)
{
    // Processors
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [FinancialStatementType]V5Processor>();
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [FinancialStatementType]V4Processor>();
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [FinancialStatementType]V3Processor>();
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [FinancialStatementType]V2Processor>();
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [FinancialStatementType]V1Processor>();

    // Version detector
    serviceCollection.AddScoped<ICodalVersionDetector, [FinancialStatementType]Detector>();

    return serviceCollection;
}
```

### 7. Database Context Updates

Update `src/Infrastructure/Fundamental.Infrastructure/Persistence/FundamentalDbContext.cs`:

```csharp
public DbSet<Canonical[FinancialStatementType]> Canonical[FinancialStatementType]s { get; set; } = null!;
public DbSet<Raw[FinancialStatementType]Json> Raw[FinancialStatementType]Jsons { get; set; } = null!;
```

And in `OnModelCreating()`:

```csharp
.ApplyConfiguration(new Canonical[FinancialStatementType]Configuration())
.ApplyConfiguration(new Raw[FinancialStatementType]JsonConfiguration())
```

### 8. Integration Tests

Create `tests/IntegrationTests/Codals/Manufacturing/[FinancialStatementType]IntegrationTests.cs`:

```csharp
public class [FinancialStatementType]IntegrationTests : FinancialStatementTestBase
{
    [Fact]
    public async Task Process[FinancialStatementType]V5_ShouldStoreCanonicalDataAndRawJson()
    {
        // Arrange
        await Clean[FinancialStatementType]Data();
        var symbol = CreateTestSymbol("IRO1TEST0001", 1000001UL);
        await _fixture.DbContext.Symbols.AddAsync(symbol);
        await _fixture.DbContext.SaveChangesAsync();

        string testJson = [FinancialStatementType]TestData.GetV5TestData();
        SetupApiResponse("[financial-statement-type]", testJson);

        // Act
        var processor = new [FinancialStatementType]V5Processor(_fixture.Services.GetRequiredService<IServiceScopeFactory>());
        var statement = CreateStatementResponse("IRO1TEST0001", 123456789UL);
        var jsonResponse = CreateJsonResponse(testJson);

        await processor.Process(statement, jsonResponse, CancellationToken.None);

        // Assert
        var storedEntity = await _fixture.DbContext.Canonical[FinancialStatementType]s
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == 123456789UL);

        storedEntity.Should().NotBeNull();
        storedEntity!.Version.Should().Be("5");
        storedEntity.FiscalYear.Year.Should().Be([expected_year]);
        storedEntity.ReportMonth.Month.Should().Be([expected_month]);

        // [Assert data sections are populated]
    }

    // [Similar tests for V4-V1]

    protected async Task Clean[FinancialStatementType]Data()
    {
        _fixture.DbContext.Canonical[FinancialStatementType]s.RemoveRange(_fixture.DbContext.Canonical[FinancialStatementType]s);
        _fixture.DbContext.Raw[FinancialStatementType]Jsons.RemoveRange(_fixture.DbContext.Raw[FinancialStatementType]Jsons);
        await _fixture.DbContext.SaveChangesAsync();
    }
}
```

### 9. Test Data

Create `tests/IntegrationTests/TestData/[FinancialStatementType]TestData.cs`:

```csharp
public static class [FinancialStatementType]TestData
{
    public static string GetV5TestData() => /* JSON string */;
    public static string GetV4TestData() => /* JSON string */;
    // [V3-V1 test data]
}
```

## Implementation Steps

1. **Analyze V5 Structure**: Use V5 as the canonical reference. Document all fields and sections.

2. **Create Domain Entities**: Implement canonical entity and owned collections based on V5 structure.

3. **EF Core Configuration**: Set up proper mappings for owned collections and value objects.

4. **V5 Implementation**: Create V5 DTOs, processor, and tests first.

5. **Version Analysis**: Analyze V4-V1 JSON structures and identify differences.

6. **Implement Remaining Versions**: Create DTOs and processors for V4-V1 with appropriate business rules.

7. **Version Detection**: Implement JSON structure-based detection logic.

8. **Integration**: Register processors and update database context.

9. **Testing**: Create comprehensive integration tests for all versions.

10. **Validation**: Ensure all tests pass and data is correctly normalized.

## Key Considerations

- **Version Business Rules**: Document fiscal year calculations and report month handling for each version
- **Data Completeness**: Ensure all V5 sections are captured in canonical model
- **Test Data**: Create realistic test data for all versions
- **Error Handling**: Implement proper error handling and logging
- **Performance**: Use efficient EF Core queries and avoid N+1 problems
- **Maintainability**: Follow existing patterns and conventions

## Validation Checklist

- [ ] All entities compile without errors
- [ ] EF Core configurations are correct
- [ ] All processors handle their respective versions
- [ ] Version detection works for all JSON structures
- [ ] Integration tests pass for all versions
- [ ] Raw JSON is stored correctly
- [ ] Canonical data is properly normalized
- [ ] No regressions in existing functionality</content>
  <parameter name="filePath">c:\Repos\Personal\Fundamental\Fundamental.Backend\Financial_Statement_JSON_Parser_Prompt.md
