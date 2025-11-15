# Financial Statement JSON Parser Implementation Prompt

## Context
This is a .NET 9.0 backend service using Clean Architecture with CQRS/MediatR pattern. The service processes Iranian financial statements from the CODAL API, transforming versioned JSON reports into canonical domain entities.

## Architecture Layers
- **Domain**: Core business logic, entities, value objects, enums
- **Application**: CQRS commands/queries, DTOs, specifications  
- **Infrastructure**: EF Core, external API clients, processors, mapping services
- **Presentation**: ASP.NET Core Web API

## Task: Implement JSON Processor for [FINANCIAL_STATEMENT_TYPE]

### 🎯 Objective
Create a complete V[VERSION] processor for `[STATEMENT_NAME]` that:
1. Deserializes JSON from CODAL API
2. Maps to canonical domain entities
3. Stores both raw JSON and normalized data in PostgreSQL
4. Follows existing patterns (use `CanonicalMonthlyActivity` as reference)

### 📋 Prerequisites
**I will provide:**
1. Sample JSON file (e.g., `IRO1XXXX0001.json`)
2. Documentation describing the JSON structure and fields
3. Statement type: `[LetterType]`, Version: `V[X]`

### 🏗️ Implementation Checklist

#### Phase 1: Domain Layer
**Location**: `src/Domain/Fundamental.Domain/Codals/Manufacturing/`

1. **Create Row Code Enums** (`Enums/[SectionName]RowCode.cs`)
   - Analyze JSON `rowCode` values in each section
   - Create enum for each unique section type
   - Use XML doc comments with Persian descriptions
   - Example: `Data = -1`, `Total = 4`, `Subtotal = 2`

2. **Create Owned Entity Types** (`Entities/[StatementName]/`)
   - One class per JSON section (e.g., `ProductionAndSalesItem`)
   - Properties:
     - `RowCode` (enum from step 1)
     - `Category` (int)
     - `RowType` (RowType enum)
     - Period amounts (decimal?)
     - Text fields (string?)
   - **Helper methods**: `GetDataRows()`, `GetTotal()`, `GetSubtotal()`, etc.
   - Use **private setters** for immutability

3. **Create Canonical Entity** (`Entities/Canonical[StatementName].cs`)
   - Inherits `BaseEntity<Guid>`
   - Shadow FK: `symbol_id`
   - Properties:
     - `Symbol` (navigation property)
     - `TraceNo` (ulong)
     - `HtmlUrl` (Uri)
     - `FiscalYear` (FiscalYear value object)
     - `YearEndMonth` (StatementMonth value object)
     - `ReportMonth` (StatementMonth value object)
     - `PublishDate` (DateTime, UTC)
     - `Currency` (IsoCurrency enum - IRR, USD, EUR)
     - `Version` (string - "V2", "V5", etc.)
     - Collection properties for each section (ICollection<T>)
   - Constructor with required parameters
   - Collections should have **public setters** (required for mapping service)

#### Phase 2: Application Layer - DTOs
**Location**: `src/Application/Fundamental.Application/Codals/Dto/FinancialStatements/ManufacturingCompanies/[StatementName]/V[X]/`

1. **Create YearDataDto.cs**
   ```csharp
   - columnId, caption (Persian)
   - periodEndToDate (string "1404/07/30")
   - yearEndToDate (string "1404/12/29")
   - period (int)
   - isAudited (bool)
   // Computed properties:
   - FiscalYear (PersianDateTime → int year)
   - FiscalMonth (PersianDateTime → int month)
   ```

2. **Create Section DTOs** (one per JSON section)
   ```csharp
   - YearData (List<YearDataDto>)
   - RowItems (List<RowItemDto>)
   
   // RowItemDto properties:
   - rowCode (int)
   - category (int)
   - rowType (string)
   - value_XXXXX (string - one per column)
   
   // Helper method:
   - GetDescription() or GetAmount() to extract primary field
   ```

3. **Create Root DTO** (`Codal[StatementName]V[X].cs`)
   - Implements `ICodalMappingServiceMetadata`
   - Static properties: `ReportingType`, `LetterType`, `CodalVersion`, `LetterPart`
   - Section properties (all 14+ sections from JSON)
   - Method: `IsValidReport()` - checks if required sections exist

4. **Create Wrapper DTO** (`Root[StatementName]V[X].cs`)
   ```csharp
   [JsonProperty("statementName")]
   public Codal[StatementName]V[X]? StatementSection { get; set; }
   
   [JsonProperty("listedCapital")]
   public string? ListedCapital { get; set; }
   
   [JsonProperty("unauthorizedCapital")]  
   public string? UnauthorizedCapital { get; set; }
   ```

#### Phase 3: Infrastructure Layer - Processing
**Location**: `src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Processors/[StatementName]/`

1. **Create Mapping Service** (`[StatementName]MappingServiceV[X].cs`)
   - Implements `ICanonicalMappingService<Canonical[StatementName], Codal[StatementName]V[X]>`
   - `MapToCanonicalAsync()`:
     ```csharp
     // Extract fiscal year from FIRST yearData
     YearDataDto firstYear = dto.Section1.YearData.First();
     int fiscalYear = firstYear.FiscalYear.Value;
     int reportMonth = firstYear.ReportMonth.Value;
     int yearEndMonth = firstYear.FiscalMonth ?? 12;
     
     // Create canonical entity
     Canonical entity = new(
         Guid.NewGuid(),
         symbol,
         statement.TracingNo,
         statement.HtmlUrl,
         new FiscalYear(fiscalYear),
         new StatementMonth(yearEndMonth),
         new StatementMonth(reportMonth),
         statement.PublishDateMiladi.ToUniversalTime(), // CRITICAL: UTC
         "V[X]"
     );
     
     // Map all sections
     entity.Section1Items = MapSection1(dto.Section1);
     // ... repeat for all sections
     ```
   - `UpdateCanonical()`: Copy all collection properties
   - Private mapping methods: `MapSection1()`, `MapSection2()`, etc.
   - Helper methods:
     ```csharp
     ParseDecimal(string? value) // handles null, empty, commas
     ParseRowType(string? rowType) // converts to RowType enum
     ```

2. **Create Processor** (`[StatementName]V[X]Processor.cs`)
   - Implements `ICodalProcessor`
   - Static properties: `ReportingType`, `LetterType`, `CodalVersion`, `LetterPart`
   - Constructor: `(IServiceScopeFactory, ICanonicalMappingServiceFactory)`
   - `Process()` method:
     ```csharp
     // 1. Filter JSON (keep only needed properties)
     JObject jObject = JObject.Parse(model.Json);
     HashSet<string> propertiesToKeep = ["listedCapital", "unauthorizedCapital", "statementName"];
     // Remove unwanted properties
     
     // 2. Deserialize
     Root? root = jObject.ToObject<Root>();
     if (root?.StatementSection is null) return;
     
     // 3. Validate
     if (!dto.IsValidReport()) return;
     
     // 4. Get symbol from DB
     Symbol symbol = await dbContext.Symbols.FirstAsync(x => x.Isin == statement.Isin);
     
     // 5. Get mapping service
     var mappingService = mappingServiceFactory.GetMappingService<Canonical, Dto>();
     
     // 6. Map to canonical
     Canonical canonical = await mappingService.MapToCanonicalAsync(dto, symbol, statement);
     
     // 7. Check for existing record
     Canonical? existing = await dbContext.CanonicalEntities
         .FirstOrDefaultAsync(x => 
             x.Symbol.Isin == statement.Isin &&
             x.FiscalYear.Year == canonical.FiscalYear.Year &&
             x.ReportMonth.Month == canonical.ReportMonth.Month &&
             x.TraceNo == statement.TracingNo);
     
     // 8. Update or insert
     if (existing != null) {
         mappingService.UpdateCanonical(existing, canonical);
     } else {
         dbContext.CanonicalEntities.Add(canonical);
     }
     
     // 9. Save
     await dbContext.SaveChangesAsync(cancellationToken);
     ```

3. **Create Version Detector** (`[StatementName]Detector.cs`)
   - Implements `ICodalVersionDetector`
   - Static properties: `ReportingType`, `LetterType`, `LetterPart`
   - `DetectVersion(string json)`:
     ```csharp
     JObject jObject = JObject.Parse(json);
     
     // V[X]: Check unique characteristics
     if (jObject["statementName"]?["version"]?.ToString() == "[X]") {
         return CodalVersion.V[X];
     }
     
     // Fallback
     return CodalVersion.None;
     ```

#### Phase 4: Infrastructure - EF Core Configuration
**Location**: `src/Infrastructure/Fundamental.Infrastructure/Configuration/Fundamental/Codals/Manufacturing/`

1. **Create Configuration** (`Canonical[StatementName]Configuration.cs`)
   ```csharp
   public class CanonicalConfiguration : IEntityTypeConfiguration<Canonical> {
       public void Configure(EntityTypeBuilder<Canonical> builder) {
           // Table
           builder.ToTable("canonical_statement_name", "manufacturing");
           
           // Primary Key
           builder.HasKey(x => x.Id);
           
           // Shadow FK
           builder.Property<Guid>("symbol_id");
           builder.HasOne(x => x.Symbol)
               .WithMany()
               .HasForeignKey("symbol_id");
           
           // Value Objects
           builder.OwnsOne(x => x.FiscalYear, ...);
           builder.OwnsOne(x => x.YearEndMonth, ...);
           builder.OwnsOne(x => x.ReportMonth, ...);
           
           // Enums
           builder.Property(x => x.Currency).HasConversion<int>();
           
           // Collections as JSONB
           builder.OwnsMany(x => x.Section1Items, owned => {
               owned.ToJson("section1_items");
               owned.Property(x => x.RowCode).HasConversion<int>();
               owned.Property(x => x.RowType).HasConversion<int>();
           });
           // Repeat for all 7+ collections
       }
   }
   ```

2. **Update DbContext** (`FundamentalDbContext.cs`)
   ```csharp
   public DbSet<Canonical[StatementName]> Canonical[StatementName]s { get; set; }
   ```

#### Phase 5: DI Registration
**Location**: `src/Infrastructure/Fundamental.Infrastructure/Extensions/Codals/Manufacturing/ServicesConfigurationExtensions.cs`

```csharp
// In AddCodalMonthlyActivityMappingServices():
serviceCollection.AddKeyedScopedCanonicalMappingService<
    ICanonicalMappingService<Canonical[StatementName], Codal[StatementName]V[X]>,
    [StatementName]MappingServiceV[X],
    Codal[StatementName]V[X]>();

// In AddCodalProcessorServices():
serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, [StatementName]V[X]Processor>();

// In AddCodalServiceDetectorServices():
serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, [StatementName]Detector>();
```

#### Phase 6: API Layer (Queries & Controller)
**Location**: `src/Application/Fundamental.Application/Codals/Manufacturing/`

1. **Create Repository Interface** (`Repositories/I[StatementName]Repository.cs`)
   ```csharp
   public interface I[StatementName]Repository
   {
       Task<Paginated<Get[StatementName]ListItem>> Get[StatementName]sAsync(
           Get[StatementName]sRequest request,
           CancellationToken cancellationToken
       );
   }
   ```

2. **Create Repository Implementation** (`Infrastructure/Repositories/.../[StatementName]Repository.cs`)
   ```csharp
   public class [StatementName]Repository(FundamentalDbContext dbContext) : I[StatementName]Repository
   {
       public async Task<Paginated<Get[StatementName]ListItem>> Get[StatementName]sAsync(
           Get[StatementName]sRequest request,
           CancellationToken cancellationToken)
       {
           IQueryable<Canonical[StatementName]> query = dbContext.Canonical[StatementName]s
               .AsNoTracking();

           // Apply filters (Isin, FiscalYear, ReportMonth, etc.)
           if (!string.IsNullOrWhiteSpace(request.Isin))
               query = query.Where(x => x.Symbol.Isin == request.Isin);

           if (request.FiscalYear.HasValue)
               query = query.Where(x => x.FiscalYear.Year == request.FiscalYear);

           // Project and paginate
           return await query.Select(x => new Get[StatementName]ListItem(
                   x.Id,
                   x.Symbol.Isin,
                   x.Symbol.Name,
                   x.Symbol.Title,
                   x.Uri,
                   x.Version,
                   x.FiscalYear.Year,
                   x.YearEndMonth.Month,
                   x.ReportMonth.Month,
                   x.TraceNo,
                   x.PublishDate
               ))
               .ToPagingListAsync(request, "FiscalYear desc, ReportMonth desc", cancellationToken);
       }
   }
   ```

3. **Create List Query** (`Queries/Get[StatementName]s/`)
   - **Request**: Inherits from `PagingRequest`, includes filter properties
   - **ListItem**: DTO with basic metadata (Id, Isin, Symbol, FiscalYear, etc.)
   - **Handler**: Uses repository to fetch paginated data

4. **Create Detail Query** (`Queries/Get[StatementName]ById/`)
   - **Request**: Single `Guid Id` parameter
   - **DetailItem**: Complete DTO with all collections
   - **Specification**: Uses Ardalis.Specification with `.Select()` projection
   - **Handler**: Uses `IRepository` with specification pattern
   - **ErrorCodes**: Enum with `[HandlerCode]` and `[ErrorType]` attributes

5. **Create Specifications** (`Specifications/`)
   ```csharp
   // Detail specification with full projection
   public sealed class [StatementName]DetailItemSpec : Specification<Canonical[StatementName], Get[StatementName]DetailItem>
   {
       public [StatementName]DetailItemSpec()
       {
           Query
               .AsNoTracking()
               .Include(x => x.Symbol)
               .Select(x => new Get[StatementName]DetailItem(
                   x.Id,
                   x.Symbol.Isin,
                   x.Symbol.Name,
                   // ... all properties
                   x.Section1Items.Select(item => new Section1ItemDto(...)).ToList(),
                   // ... all collections
               ));
       }

       public [StatementName]DetailItemSpec WhereId(Guid id)
       {
           Query.Where(x => x.Id == id);
           return this;
       }
   }
   ```

6. **Create Controller** (`Presentation/WebApi/Controllers/.../[StatementName]Controller.cs`)
   ```csharp
   [ApiController]
   [Route("[area]/[statement-name]")]
   [ApiVersion("1.0")]
   [TranslateResultToActionResult]
   [Area("Manufacturing")]
   public class [StatementName]Controller : ControllerBase
   {
       private readonly IMediator _mediator;

       public [StatementName]Controller(IMediator mediator)
       {
           _mediator = mediator;
       }

       [HttpGet]
       public async Task<Response<Paginated<Get[StatementName]ListItem>>> Get[StatementName]s(
           [FromQuery] Get[StatementName]sRequest request
       )
       {
           return await _mediator.Send(request);
       }

       [HttpGet("{id}")]
       [SwaggerRequestType(typeof(Get[StatementName]ByIdRequest))]
       public async Task<Response<Get[StatementName]DetailItem>> Get[StatementName](
           [FromRoute] Guid id
       )
       {
           return await _mediator.Send(new Get[StatementName]ByIdRequest(id));
       }
   }
   ```

7. **Register in DI** (`ServicesConfigurationExtensions.cs`)
   ```csharp
   // In AddManufacturingReadRepositories():
   serviceCollection.AddScoped<I[StatementName]Repository, [StatementName]Repository>();
   ```

#### Phase 7: Testing
**Location**: `tests/IntegrationTests/`

1. **Create Test Data Helper** (`TestData/[StatementName]TestData.cs`)
   ```csharp
   public static class [StatementName]TestData {
       public static string GetV[X]TestData() {
           string filePath = Path.Combine(
               AppDomain.CurrentDomain.BaseDirectory,
               "Data",
               "[StatementName]",
               "V[X]",
               "[ISIN]",
               "[ISIN].json");
           
           if (!File.Exists(filePath)) {
               throw new FileNotFoundException($"Test data file not found: {filePath}");
           }
           
           return File.ReadAllText(filePath);
       }
   }
   ```

2. **Create Processor Integration Tests** (`Codals/Manufacturing/[StatementName]IntegrationTests.cs`)
   - Test 1: `ShouldStoreCanonicalData` - verify entity saved
   - Test 2: `ShouldMapAllSections` - verify all collections populated
   - Test 3: `ShouldCorrectlyMapRowCodes` - verify enum conversions
   - Test 4: `ShouldExtractCorrectPeriodData` - verify fiscal year/month
   - Test 5: `ShouldHandleUpdate` - verify upsert logic
   - Test 6: `Detector_ShouldDetectV[X]` - verify version detection
   - Test 7: `Detector_ShouldReturnNoneForInvalidVersion`
   - Test 8: `Detector_ShouldReturnNoneForMissingSection`

3. **Create API Integration Tests** (`Codals/Manufacturing/[StatementName]ApiTests.cs`)
   - Test 1: `Get[StatementName]s_WithoutFilters_ShouldReturnPaginatedList`
   - Test 2: `Get[StatementName]s_WithIsinFilter_ShouldReturnFilteredResults`
   - Test 3: `Get[StatementName]s_WithFiscalYearFilter_ShouldReturnFilteredResults`
   - Test 4: `Get[StatementName]s_WithReportMonthFilter_ShouldReturnFilteredResults`
   - Test 5: `Get[StatementName]s_WithPagination_ShouldReturnCorrectPage`
   - Test 6: `Get[StatementName]s_ShouldOrderByFiscalYearDescThenReportMonthDesc`
   - Test 7: `Get[StatementName]ById_WithValidId_ShouldReturnDetailItem`
   - Test 8: `Get[StatementName]ById_ShouldIncludeAllCollections`
   - Test 9: `Get[StatementName]ById_WithInvalidId_ShouldReturnNotFound`
   - Test 10: `Get[StatementName]ById_ShouldReturnCorrectRowCodeEnums`
   - Test 11: `Get[StatementName]s_WithNoData_ShouldReturnEmptyList`

4. **Update .csproj** (`IntegrationTests.csproj`)
   ```xml
   <ItemGroup>
     <None Update="Data\**\*.json">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </None>
   </ItemGroup>
   ```

### 🎨 Code Quality Requirements

1. **Enums**: Use XML doc comments with Persian descriptions
2. **Private Setters**: Domain entities should have private setters (except collections in canonical entities)
3. **UTC Dates**: Always use `ToUniversalTime()` for DateTime passed to PostgreSQL
4. **Null Handling**: Use `?` for nullable decimals, strings; check `IsNullOrEmpty()`
5. **Magic Numbers**: No magic numbers - use enums for all codes
6. **JSONB Storage**: All collections stored as JSONB in PostgreSQL
7. **Shadow FK**: Use shadow foreign key for `symbol_id`
8. **Value Objects**: Use `FiscalYear`, `StatementMonth`, `IsoCurrency`
9. **Warnings**: Code must compile warning-free (except StyleCop/Sonar analyzer suggestions)

### 📚 Key Patterns to Follow

**Period Extraction (CRITICAL):**
```csharp
// ALWAYS extract from FIRST yearData in FIRST section
YearDataDto firstYear = dto.FirstSection.YearData.First();
int fiscalYear = firstYear.FiscalYear.Value; // e.g., 1404
int reportMonth = firstYear.ReportMonth.Value; // e.g., 7  
int yearEndMonth = firstYear.FiscalMonth ?? 12; // e.g., 12
```

**Decimal Parsing:**
```csharp
private static decimal? ParseDecimal(string? value)
{
    if (string.IsNullOrWhiteSpace(value)) return null;
    
    string cleaned = value.Replace(",", "").Trim();
    if (decimal.TryParse(cleaned, out decimal result)) {
        return result;
    }
    
    return null;
}
```

**Row Type Parsing:**
```csharp
private static RowType ParseRowType(string? rowType)
{
    return rowType?.ToLowerInvariant() switch
    {
        "productrow" => RowType.ProductRow,
        "fixedrow" => RowType.FixedRow,
        "descriptionrow" => RowType.DescriptionRow,
        _ => RowType.FixedRow
    };
}
```

**Collection Mapping:**
```csharp
private List<SectionItem> MapSection(SectionDto? dto)
{
    if (dto?.RowItems == null) {
        return new List<SectionItem>();
    }
    
    return dto.RowItems
        .Select(x => new SectionItem {
            RowCode = (SectionRowCode)x.RowCode,
            Category = x.Category,
            RowType = ParseRowType(x.RowType),
            Description = x.GetDescription(),
            Amount1 = ParseDecimal(x.Value_XXXXX),
            Amount2 = ParseDecimal(x.Value_YYYYY)
        })
        .ToList();
}
```

### ✅ Validation Checklist

Before considering implementation complete:

- [ ] All enums created with XML doc comments
- [ ] All owned entity types have helper methods
- [ ] Canonical entity uses private setters (except collections)
- [ ] All DTOs deserialize correctly from JSON
- [ ] Mapping service extracts period from first yearData
- [ ] DateTime converted to UTC before saving
- [ ] EF Core configuration maps all collections as JSONB
- [ ] All services registered in DI container
- [ ] 8+ integration tests created and passing
- [ ] Test data JSON file copied to output directory
- [ ] Build succeeds with zero errors
- [ ] No magic numbers in code

### 🚀 Example Reference Implementation

**Study these files for complete pattern:**
- `CanonicalMonthlyActivity.cs` - Domain entity structure
- `MonthlyActivityV5Processor.cs` - Processor pattern
- `MonthlyActivityMappingServiceV5.cs` - Mapping pattern
- `MonthlyActivityIntegrationTests.cs` - Testing pattern
- `CanonicalMonthlyActivityConfiguration.cs` - EF Core configuration

### 📝 Naming Conventions

- **Canonical Entity**: `Canonical[StatementName]` (e.g., `CanonicalBalanceSheet`)
- **DTO**: `Codal[StatementName]V[X]` (e.g., `CodalBalanceSheetV5`)
- **Processor**: `[StatementName]V[X]Processor` (e.g., `BalanceSheetV5Processor`)
- **Mapping Service**: `[StatementName]MappingServiceV[X]` (e.g., `BalanceSheetMappingServiceV5`)
- **Detector**: `[StatementName]Detector` (e.g., `BalanceSheetDetector`)
- **Enums**: `[SectionName]RowCode` (e.g., `AssetsRowCode`, `LiabilitiesRowCode`)
- **Owned Entities**: `[SectionName]Item` (e.g., `AssetItem`, `LiabilityItem`)

---

## 🚨 Clean Architecture Best Practices & Common Pitfalls

### Layer Dependencies (What Can Reference What)

| Layer | Can Reference | Cannot Reference | Reasoning |
|-------|--------------|-----------------|-----------|
| **Domain** | Nothing | Application, Infrastructure, Presentation | Core business logic must be independent |
| **Application** | Domain only | Infrastructure, Presentation | Business rules should not depend on implementation details |
| **Infrastructure** | Domain, Application | Presentation | Can implement interfaces from Application |
| **Presentation** | Domain, Application, Infrastructure | Nothing above it | Top layer orchestrates everything |

### Query Handler Patterns

#### ❌ WRONG: Application Layer Using DbContext Directly
```csharp
// In Application/Queries/Get[Statement]ByIdQueryHandler.cs
public class Get[Statement]ByIdQueryHandler(FundamentalDbContext dbContext) // ❌ Infrastructure dependency!
{
    public async Task<Response<Get[Statement]DetailItem>> Handle(...)
    {
        var result = await dbContext.[Statements]
            .Include(x => x.Symbol)
            .Include(x => x.Section1Items)
            .FirstOrDefaultAsync(...); // ❌ Direct EF Core usage in Application layer
    }
}
```
**Problem**: Application layer has hard dependency on Infrastructure (FundamentalDbContext). Violates Clean Architecture.

#### ✅ CORRECT: Using IRepository with Specification Pattern
```csharp
// In Application/Queries/Get[Statement]ByIdQueryHandler.cs
public class Get[Statement]ByIdQueryHandler(
    IRepository<Canonical[Statement]> repository) // ✅ Domain interface
{
    public async Task<Response<Get[Statement]DetailItem>> Handle(...)
    {
        Get[Statement]DetailItem? result = await repository.FirstOrDefaultAsync(
            new [Statement]DetailItemSpec().WhereId(request.Id), // ✅ Specification pattern
            cancellationToken);
        
        if (result is null)
            return Get[Statement]ByIdRequestErrorCodes.NotFound; // ✅ Error enum
        
        return result; // ✅ Implicit cast to Response
    }
}
```
**Benefits**:
- Application layer depends only on Domain abstractions
- Query logic stays in Application (Specification)
- Infrastructure concerns (EF Core) isolated in Infrastructure layer
- Easy to test with mock repositories

### Repository Patterns

#### Pattern 1: IRepository + Specification (for Detail Queries)
**When to use**: Single entity retrieval with complex projections

```csharp
// Application/Specifications/[Statement]DetailItemSpec.cs
public sealed class [Statement]DetailItemSpec 
    : Specification<Canonical[Statement], Get[Statement]DetailItem>
{
    public [Statement]DetailItemSpec()
    {
        Query
            .AsNoTracking()
            .Include(x => x.Symbol)
            .Select(x => new Get[Statement]DetailItem(
                x.Id,
                x.Symbol.Isin,
                x.Section1Items.Select(i => new Section1ItemDto(...)).ToList(),
                // ... all properties and collections
            ));
    }

    public [Statement]DetailItemSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}
```

#### Pattern 2: Custom Repository Interface (for List Queries with Pagination)
**When to use**: Complex filtering, sorting, and pagination

```csharp
// Application/Repositories/I[Statement]Repository.cs
public interface I[Statement]Repository
{
    Task<Paginated<Get[Statement]ListItem>> Get[Statement]sAsync(
        Get[Statement]sRequest request,
        CancellationToken cancellationToken);
}

// Infrastructure/Repositories/.../[Statement]Repository.cs
public class [Statement]Repository(FundamentalDbContext dbContext) 
    : I[Statement]Repository
{
    public async Task<Paginated<Get[Statement]ListItem>> Get[Statement]sAsync(
        Get[Statement]sRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<Canonical[Statement]> query = dbContext.[Statements]
            .AsNoTracking();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.Isin))
            query = query.Where(x => x.Symbol.Isin == request.Isin);

        // Project and paginate
        return await query
            .Select(x => new Get[Statement]ListItem(...))
            .ToPagingListAsync(request, "FiscalYear desc, ReportMonth desc", cancellationToken);
    }
}
```

### Common Mistakes & Solutions

| Mistake | Problem | Solution |
|---------|---------|----------|
| Using `FundamentalDbContext` in Application layer | Clean Architecture violation | Use `IRepository<T>` or create custom repository interface in Application |
| `Symbol.SymbolName` property | Property doesn't exist | Use `Symbol.Name` (correct property) |
| `ToPaginatedListAsync()` | Method doesn't exist | Use `ToPagingListAsync(request, defaultSort, cancellationToken)` |
| Request with `PageNumber`/`PageSize` properties | Doesn't inherit `PagingRequest` | Make request inherit from `PagingRequest` |
| Returning `null` or throwing exceptions | Not using Result pattern | Return error enum directly: `return ErrorCodes.NotFound;` |
| Including collections without `.Select()` | Circular reference serialization errors | Use `.Select()` projection in Specification |

### Integration Testing Strategies

#### Testing Approach
- **Processor Tests**: Verify JSON processing and entity mapping
- **API Tests**: Verify query handlers and DTOs via MediatR (not HTTP)
- **No Mocking**: Use real database (TestFixture) and WireMock for external APIs

#### API Test Structure
```csharp
public class [Statement]ApiTests : IAsyncLifetime
{
    private readonly TestFixture _fixture;

    public [Statement]ApiTests()
    {
        _fixture = new TestFixture();
    }

    // Helper: Seed data using processor
    private async Task SeedTestData()
    {
        string json = [Statement]TestData.GetV2TestData();
        await SetupApiResponse(json);

        var processor = _fixture.GetKeyedProcessor<I[Statement]Processor>(ProcessorVersion.V2);
        await processor.ProcessAsync(json, CancellationToken.None);
    }

    // Helper: Clean database
    private async Task Clean[Statement]Data()
    {
        await _fixture.DatabaseHelper.ExecuteSqlAsync(
            "DELETE FROM codals.canonical_[statement]s");
    }

    // Test: Verify pagination
    [Fact]
    public async Task Get[Statement]s_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await SeedTestData();
        var request = new Get[Statement]sRequest { PageNumber = 1, PageSize = 5 };

        // Act
        Response<Paginated<Get[Statement]ListItem>> response = 
            await _fixture.Mediator.Send(request);

        // Assert
        response.Should().BeSuccessful();
        response.Data!.PageSize.Should().Be(5);
        response.Data.Items.Should().HaveCount(5);
    }

    // Test: Verify detail with all collections
    [Fact]
    public async Task Get[Statement]ById_ShouldIncludeAllCollections()
    {
        // Arrange
        await SeedTestData();
        Guid id = await GetFirstEntityId();

        // Act
        Response<Get[Statement]DetailItem> response = 
            await _fixture.Mediator.Send(new Get[Statement]ByIdRequest(id));

        // Assert
        response.Should().BeSuccessful();
        response.Data!.Section1Items.Should().NotBeEmpty();
        response.Data.Section2Items.Should().NotBeEmpty();
        // ... verify all collections
    }

    // Test: Verify NotFound error
    [Fact]
    public async Task Get[Statement]ById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        // Act
        Response<Get[Statement]DetailItem> response = 
            await _fixture.Mediator.Send(new Get[Statement]ByIdRequest(invalidId));

        // Assert
        response.Should().BeUnsuccessful();
        response.ErrorType.Should().Be(ErrorType.NotFound);
        response.ErrorCode.Should().Be((int)Get[Statement]ByIdRequestErrorCodes.NotFound);
    }
}
```

### Key Takeaways
1. **Always** check layer dependencies - Application should never reference Infrastructure types
2. **Use** IRepository (Domain) or custom interfaces (Application) instead of DbContext
3. **Apply** Specification pattern for complex queries with projections
4. **Inherit** PagingRequest for paginated queries
5. **Return** error enums directly, not null or exceptions
6. **Test** with real database and MediatR, not mocked HTTP calls
7. **Verify** all entity property names match (e.g., `Symbol.Name` not `Symbol.SymbolName`)

## 🎯 Now Provide Me:

1. **JSON Sample File** - Full CODAL JSON response
2. **Documentation** - Field descriptions, structure explanation
3. **Statement Details**:
   - Statement name (e.g., "Balance Sheet", "Income Statement")
   - Letter type (e.g., `LetterType.FinancialStatement`)
   - Version number (e.g., V5, V7)
   - Letter part (e.g., `LetterPart.BalanceSheet`)

**I will implement the complete processor following this exact pattern!**
