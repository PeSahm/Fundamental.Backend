# Canonical Mapping Service Architecture

## Overview

Refactor the current processor architecture to separate mapping concerns from processing flow by introducing Canonical
Mapping Services.

## Proposed Architecture

### 1. Core Interfaces

#### ICanonicalMappingService<TCanonical, TDto>

```csharp
public interface ICanonicalMappingService<TCanonical, TDto>
    where TCanonical : BaseEntity<Guid>
    where TDto : class, ICodalMappingServiceMetadata
{
    Task<TCanonical> MapToCanonicalAsync(TDto dto, Symbol symbol, GetStatementResponse statement);
    void UpdateCanonical(TCanonical existing, TCanonical updated);
}
```

#### ICodalMappingServiceMetadata

```csharp
public interface ICodalMappingServiceMetadata
{
    ReportingType ReportingType { get; }
    LetterType LetterType { get; }
    CodalVersion CodalVersion { get; }
    LetterPart LetterPart { get; }
}
```

#### ICanonicalMappingServiceFactory

```csharp
public interface ICanonicalMappingServiceFactory
{
    ICanonicalMappingService<TCanonical, TDto> GetMappingService<TCanonical, TDto>()
        where TCanonical : BaseEntity<Guid>
        where TDto : class, ICodalMappingServiceMetadata;
}
```

### 2. Concrete Implementation for Monthly Activity

#### DTO with Metadata

```csharp
public class CodalMonthlyActivityV5 : ICodalMappingServiceMetadata
{
    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V5;
    public LetterPart LetterPart => LetterPart.NotSpecified;

    [JsonProperty("monthlyActivity")]
    public MonthlyActivityDtoV5 MonthlyActivity { get; set; } = null!;
    
    // ... other properties
}
```

#### IMonthlyActivityMappingService

```csharp
public interface IMonthlyActivityMappingService : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivity>
{
    // Monthly Activity specific methods if needed
}
```

#### MonthlyActivityMappingServiceV5

```csharp
public class MonthlyActivityMappingServiceV5 : IMonthlyActivityMappingService
{
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivity dto,
        Symbol symbol,
        GetStatementResponse statement)
    {
        // Extract fiscal year and report month
        var yearDatum = dto.MonthlyActivity.ProductionAndSales?.YearData.FirstOrDefault();
        int fiscalYear = yearDatum?.FiscalYear ?? DateTime.Now.Year;
        int reportMonth = yearDatum?.ReportMonth ?? 1;

        // Create canonical entity
        var canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = dto.CodalVersion.ToString(), // Use DTO's metadata
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = reportMonth,
            HasSubCompanySale = false
        };

        // Map all sections
        canonical.BuyRawMaterialItems = MapBuyRawMaterial(dto.MonthlyActivity.BuyRawMaterial);
        canonical.ProductionAndSalesItems = MapProductionAndSales(dto.MonthlyActivity.ProductionAndSales);
        canonical.EnergyItems = MapEnergy(dto.MonthlyActivity.Energy);
        canonical.CurrencyExchangeItems = MapCurrencyExchange(dto.MonthlyActivity.SourceUsesCurrency);
        canonical.Descriptions = MapDescriptions(dto.MonthlyActivity.ProductMonthlyActivityDesc1);

        return canonical;
    }

    public void UpdateCanonical(CanonicalMonthlyActivity existing, CanonicalMonthlyActivity updated)
    {
        existing.TraceNo = updated.TraceNo;
        existing.Uri = updated.Uri;
        existing.Currency = updated.Currency;
        existing.HasSubCompanySale = updated.HasSubCompanySale;

        existing.BuyRawMaterialItems = updated.BuyRawMaterialItems;
        existing.ProductionAndSalesItems = updated.ProductionAndSalesItems;
        existing.EnergyItems = updated.EnergyItems;
        existing.CurrencyExchangeItems = updated.CurrencyExchangeItems;
        existing.Descriptions = updated.Descriptions;
    }

    private List<BuyRawMaterialItem> MapBuyRawMaterial(BuyRawMaterialV5? buyRawMaterial)
    {
        if (buyRawMaterial?.RowItems == null) return new();

        return buyRawMaterial.RowItems
            .Where(x => !string.IsNullOrEmpty(x.Value34641))
            .Select(x => new BuyRawMaterialItem
            {
                MaterialName = x.Value34641,
                Unit = x.Value34642,
                YearToDateQuantity = x.Value34643,
                YearToDateRate = x.Value34644,
                YearToDateAmount = x.Value34645,
                CorrectedYearToDateQuantity = x.Value34649,
                CorrectedYearToDateRate = x.Value346410,
                CorrectedYearToDateAmount = x.Value346411
            })
            .ToList();
    }

    // Similar private methods for other sections...
}
```

### 3. Factory Implementation

#### CanonicalMappingServiceFactory

```csharp
public class CanonicalMappingServiceFactory : ICanonicalMappingServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CanonicalMappingServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICanonicalMappingService<TCanonical, TDto> GetMappingService<TCanonical, TDto>()
        where TCanonical : BaseEntity<Guid>
        where TDto : class, ICodalMappingServiceMetadata
    {
        if (_serviceProvider is not IKeyedServiceProvider keyedServiceProvider)
        {
            throw new InvalidOperationException("Keyed Services Not Supported");
        }

        // Get metadata from the DTO type using reflection
        var metadata = GetMetadataFromDtoType(typeof(TDto));
        
        return (ICanonicalMappingService<TCanonical, TDto>)keyedServiceProvider.GetRequiredKeyedService(
            typeof(ICanonicalMappingService<TCanonical, TDto>), 
            MappingServiceKey(metadata.ReportingType, metadata.LetterType, metadata.CodalVersion, metadata.LetterPart));
    }

    private static ICodalMappingServiceMetadata GetMetadataFromDtoType(Type dtoType)
    {
        // Create a dummy instance to access metadata properties
        // In practice, you might want to cache this or use a different approach
        var instance = Activator.CreateInstance(dtoType) as ICodalMappingServiceMetadata;
        if (instance == null)
        {
            throw new InvalidOperationException($"Cannot create metadata instance for DTO type {dtoType.Name}");
        }
        return instance;
    }

    private static string MappingServiceKey(ReportingType reportingType, LetterType letterType, CodalVersion version, LetterPart letterPart)
    {
        return $"{reportingType}-{letterType}-{version}-{letterPart}";
    }
}
```

#### Service Registration Extensions

```csharp
public static class MappingServiceExtensions
{
    public static IServiceCollection AddKeyedScopedCanonicalMappingService<TService, TImplementation, TDto>(
        this IServiceCollection services)
        where TService : ICanonicalMappingService
        where TImplementation : class, TService
        where TDto : class, ICodalMappingServiceMetadata
    {
        return services.AddKeyedScoped(
            typeof(TService),
            MappingServiceKeyFromDto<TDto>(),
            typeof(TImplementation));
    }

    public static T GetRequiredKeyedService<T>(
        this IServiceProvider provider,
        ReportingType reportingType,
        LetterType letterType,
        CodalVersion version,
        LetterPart letterPart)
        where T : ICanonicalMappingService
    {
        if (provider is not IKeyedServiceProvider keyedServiceProvider)
        {
            throw new InvalidOperationException("Keyed Services Not Supported");
        }

        var key = MappingServiceKey(reportingType, letterType, version, letterPart);
        return (T)keyedServiceProvider.GetRequiredKeyedService(typeof(T), key);
    }

    private static string MappingServiceKeyFromDto<TDto>() where TDto : class, ICodalMappingServiceMetadata
    {
        var metadata = GetMetadataFromDtoType(typeof(TDto));
        return MappingServiceKey(metadata.ReportingType, metadata.LetterType, metadata.CodalVersion, metadata.LetterPart);
    }

    private static ICodalMappingServiceMetadata GetMetadataFromDtoType(Type dtoType)
    {
        var instance = Activator.CreateInstance(dtoType) as ICodalMappingServiceMetadata;
        if (instance == null)
        {
            throw new InvalidOperationException($"Cannot create metadata instance for DTO type {dtoType.Name}");
        }
        return instance;
    }

    private static string MappingServiceKey(ReportingType reportingType, LetterType letterType, CodalVersion version, LetterPart letterPart)
    {
        return $"{reportingType}-{letterType}-{version}-{letterPart}";
    }
}
```

### 4. Refactored Processor

#### MonthlyActivityV5Processor (Simplified)

```csharp
public class MonthlyActivityV5Processor : ICodalProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ICanonicalMappingServiceFactory _mappingServiceFactory;

    public MonthlyActivityV5Processor(
        IServiceScopeFactory serviceScopeFactory,
        ICanonicalMappingServiceFactory mappingServiceFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mappingServiceFactory = mappingServiceFactory;
    }

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        // 1. Deserialize JSON
        var monthlyActivity = JsonConvert.DeserializeObject<CodalMonthlyActivityV5>(model.Json, settings);

        // 2. Get symbol
        using var scope = _serviceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        var symbol = await dbContext.Symbols.FirstAsync(x => x.Isin == statement.Isin, cancellationToken);

        // 3. Get mapping service - DTO knows its own metadata
        var mappingService = _mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>();

        var canonical = await mappingService.MapToCanonicalAsync(monthlyActivity, symbol, statement);

        // 4. Store raw JSON (same as before)
        // ... raw JSON storage logic ...

        // 5. Handle canonical entity (simplified)
        var existingCanonical = await dbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(x => x.Symbol.Isin == statement.Isin &&
                                    x.FiscalYear.Year == canonical.FiscalYear.Year &&
                                    x.ReportMonth.Month == canonical.ReportMonth.Month, cancellationToken);

        if (existingCanonical == null)
        {
            dbContext.Add(canonical);
        }
        else if (existingCanonical.TraceNo <= statement.TracingNo)
        {
            mappingService.UpdateCanonical(existingCanonical, canonical);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
```

## Using CodalVersion Enum

The architecture leverages the existing `CodalVersion` enum instead of hardcoded strings:

```csharp
public enum CodalVersion
{
    V1,
    V2,
    V3,
    V3O1,
    V4,
    V5,
    V7
}
```

And the `LetterPart` enum for different parts of reports:

```csharp
public enum LetterPart
{
    NotSpecified = -1,
    BalanceSheet = 1,
    IncomeStatement = 2,
    InterpretativeReportSummaryPage5 = 3,
    NonOperationIncomeAndExpenses = 4,

    /// <summary>
    ///     شرکت های سرمایه پذیر
    /// </summary>
    TheStatusOfViableCompanies = 5
}
```

This provides:

- **Type Safety**: Compile-time validation of version values
- **Maintainability**: Centralized version management
- **Consistency**: Same enum used across processors, detectors, and mapping services

### Version String Storage

When storing version information in canonical entities, convert the enum to string:

```csharp
Version = codalVersion.ToString() // "V5", "V4", etc.
```

Or use a custom extension method for more control over the format.

## Benefits of This Architecture

### 1. **Open-Closed Principle Compliance**

- **Extension**: New versions can be added by registering new keyed services without modifying existing code
- **No Switch Statements**: Factory uses DI container resolution instead of hardcoded version checks
- **Type Safety**: Compile-time validation through enum usage and keyed service registration

### 2. **Single Responsibility Principle**

- **Processors**: Handle workflow, JSON deserialization, database transactions
- **Mapping Services**: Handle pure DTO-to-entity transformation logic

### 3. **Testability**

- Mapping services can be unit tested independently
- Complex mapping logic tested independently of database/JSON concerns
- Easier mocking and test data setup

### 4. **Reusability**

- Mapping services can be reused across different processors
- Follows the established factory pattern (`ICodalProcessorFactory`)
- Standardized interface for all financial statement mappings

### 5. **Maintainability**

- Mapping logic is centralized and organized by version
- Changes to mapping logic don't affect processing flow
- Easier to add new financial statement types or versions

### 6. **Consistency**

- Follows the existing keyed service pattern used in the codebase
- Consistent with `ICodalProcessorFactory` architecture
- Standardized interface for all mapping operations

## Implementation Steps

1. **Create Interfaces** (`ICanonicalMappingService`, `ICodalMappingServiceMetadata`, `ICanonicalMappingServiceFactory`)
2. **Create Service Registration Extensions** (`MappingServiceExtensions` with keyed service methods)
3. **Implement Metadata on DTOs** - Each version-specific DTO (e.g., `CodalMonthlyActivityV5`) implements
   `ICodalMappingServiceMetadata`
4. **Implement Base Mapping Services** for each financial statement type
5. **Create Version-Specific Services** (V1-V5 for MonthlyActivity)
6. **Implement Factory** using reflection to get metadata from DTO types
7. **Register Keyed Services** in DI container using
   `AddKeyedScopedCanonicalMappingService<TService, TImplementation, TDto>()`
8. **Refactor Processors** to use mapping services via factory with DTO type generics
9. **Update DI Registration** in `ServicesConfigurationExtensions`
10. **Add Unit Tests** for mapping services
11. **Update Integration Tests** if needed

This architecture provides a clean separation of concerns while maintaining the existing patterns and making the
codebase more maintainable and testable.</content>
<parameter name="filePath">c:\Repos\Personal\Fundamental\Fundamental.Backend\Canonical_Mapping_Service_Architecture.md
