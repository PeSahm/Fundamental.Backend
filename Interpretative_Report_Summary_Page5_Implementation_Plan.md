# Interpretative Report Summary Page 5 (V2) Implementation Plan

## Overview
Implementation of canonical entities and processors for parsing Iranian financial data from `interpretativeReportSummaryPage5` section of CODAL API responses. This follows the established canonical pattern used in `CanonicalMonthlyActivity`.

**Issue**: #14  
**Version**: V2 only (for now)  
**Branch**: `feature/14-interpretative-report-summary-page5-v2`

## JSON Structure Analysis

### Source Data Location
- JSON Path: `interpretativeReportSummaryPage5`
- Test Data: `tests/IntegrationTests/Data/InterpretativeReportSummaryPage5/V2/IRO1SEPP0001/IRO1SEPP0001.json`

### Sections to Process

1. **p5Desc1** - Description/Header
2. **otherOperatingIncome** (سایر درآمدهای عملیاتی) - Other Operating Income
3. **otherNonOperatingExpenses** (سایر هزینه‌های غیرعملیاتی) - Other Non-Operating Expenses
4. **detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod** - Financing Details (Current Period)
5. **detailsOfTheFinancingOfTheCompany-Est1** - Financing Details (Estimated)
6. **detailsOfTheFinancingOfTheCompany-Est2** - Financing Details (Estimated 2)
7. **descriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod** - Financing Description
8. **companyEstimatesOfFinancingProgramsAndCompanyFinanceChanges** - Financing Estimates
9. **nonOperationIncomeAndExpensesInvestmentIncome** - Investment Income
10. **nonOperationIncomeAndExpensesMiscellaneousItems** - Miscellaneous Items
11. **corporateIncomeProgram** - Corporate Income Program
12. **otherImportantPrograms** - Other Important Programs
13. **otherImportantNotes** - Other Important Notes
14. **p5Desc2** - Footer Description

## Data Structure Pattern

### YearData Structure
Each section contains `yearData` array with period definitions:
```json
{
  "columnId": "2422",
  "caption": "سال مالی منتهی به 1403/12/30",
  "periodEndToDate": "1403/12/30",
  "yearEndToDate": "1403/12/30",
  "period": 12,
  "isAudited": null
}
```

### RowItems Structure
Each section contains `rowItems` array with data:
```json
{
  "rowCode": -1,  // -1 = data row, others = summary/total rows
  "oldFieldName": "",
  "category": 0,
  "rowType": "CustomRow",
  "id": "3475",
  "value_2421": "سود ناشی از تسعیر ارز",  // Description column
  "value_2422": "1538038",  // Maps to columnId 2422
  "value_2423": "5939326",  // Maps to columnId 2423
  "value_2424": "0"         // Maps to columnId 2424
}
```

### Period Mapping Strategy
- Extract fiscal year, year-end month, and report month from the first section's `yearData`
- Use `periodEndToDate` and `yearEndToDate` to determine:
  - **FiscalYear**: Year from `yearEndToDate`
  - **YearEndMonth**: Month from `yearEndToDate`
  - **ReportMonth**: Month from `periodEndToDate`

## RowCode Patterns by Section

### otherOperatingIncome (سایر درآمدهای عملیاتی)
- `rowCode: -1` = Data rows (individual income items)
- `rowCode: 4` = Total sum

### otherNonOperatingExpenses (سایر هزینه‌های غیرعملیاتی)
- `rowCode: -1` = Data rows (individual expense items)
- `rowCode: 7` = Total sum

### detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod
- `rowCode: 11` = Bank facilities received
- `rowCode: 13` = Facilities settled
- `rowCode: 14` = Total
- `rowCode: 15` = Transfer to assets
- `rowCode: 16` = Financial expense for period

### detailsOfTheFinancingOfTheCompany-Est1
- `rowCode: 56` = Bank facilities received (estimated)
- `rowCode: 58` = Facilities settled (estimated)
- `rowCode: 59` = Total (estimated)
- `rowCode: 60` = Transfer to assets (estimated)
- `rowCode: 61` = Financial expense for period (estimated)

### nonOperationIncomeAndExpensesInvestmentIncome
- `rowCode: -1` = Data rows (investment income items)
- `rowCode: 22` = Total sum

### nonOperationIncomeAndExpensesMiscellaneousItems
- `rowCode: -1` = Data rows (miscellaneous items)
- `rowCode: 25` = Total sum

### Description Sections
- `rowCode: 1, 17, 18, 27, 28, 30, 51` = Fixed text descriptions

## Canonical Entity Design

### Main Entity: CanonicalInterpretativeReportSummaryPage5

```csharp
public class CanonicalInterpretativeReportSummaryPage5 : BaseEntity<Guid>
{
    // Constructor with required properties
    public CanonicalInterpretativeReportSummaryPage5(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        DateTime publishDate,
        string version
    )
    
    // Properties
    public string Version { get; private set; }
    public DateTime? PublishDate { get; set; }
    public Symbol Symbol { get; set; }
    public ulong TraceNo { get; set; }
    public string Uri { get; set; }
    public FiscalYear FiscalYear { get; set; }
    public IsoCurrency Currency { get; set; } = IsoCurrency.IRR;
    public StatementMonth YearEndMonth { get; set; }
    public StatementMonth ReportMonth { get; set; }
    
    // Owned Collections (stored as JSONB)
    public ICollection<OtherOperatingIncomeItem> OtherOperatingIncomes { get; set; }
    public ICollection<OtherNonOperatingExpenseItem> OtherNonOperatingExpenses { get; set; }
    public ICollection<FinancingDetailItem> FinancingDetails { get; set; }
    public ICollection<FinancingDetailItem> FinancingDetailsEstimated { get; set; }
    public ICollection<InvestmentIncomeItem> InvestmentIncomes { get; set; }
    public ICollection<MiscellaneousExpenseItem> MiscellaneousExpenses { get; set; }
    public ICollection<InterpretativeReportDescription> Descriptions { get; set; }
}
```

### Owned Entity: OtherOperatingIncomeItem

```csharp
public class OtherOperatingIncomeItem
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    public string? Id { get; set; }
    
    // Description column (value_2421 in JSON)
    public string? ItemDescription { get; set; }
    
    // Period values (value_2422, value_2423, value_2424)
    public decimal? PreviousPeriodAmount { get; set; }
    public decimal? CurrentPeriodAmount { get; set; }
    public decimal? EstimatedPeriodAmount { get; set; }
    
    // Helper properties
    public bool IsDataRow => RowCode == -1;
    public bool IsTotalRow => RowCode == 4;
    
    // Helper methods
    public static List<OtherOperatingIncomeItem> GetDataRows(
        IEnumerable<OtherOperatingIncomeItem> items)
    public static OtherOperatingIncomeItem? GetTotal(
        IEnumerable<OtherOperatingIncomeItem> items)
}
```

### Owned Entity: OtherNonOperatingExpenseItem

```csharp
public class OtherNonOperatingExpenseItem
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    public string? Id { get; set; }
    
    public string? ItemDescription { get; set; }
    public decimal? PreviousPeriodAmount { get; set; }
    public decimal? CurrentPeriodAmount { get; set; }
    public decimal? EstimatedPeriodAmount { get; set; }
    
    public bool IsDataRow => RowCode == -1;
    public bool IsTotalRow => RowCode == 7;
    
    public static List<OtherNonOperatingExpenseItem> GetDataRows(...)
    public static OtherNonOperatingExpenseItem? GetTotal(...)
}
```

### Owned Entity: FinancingDetailItem

```csharp
public class FinancingDetailItem
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    
    // Column: محل تامین (Source of financing)
    public string? FinancingSource { get; set; }
    
    // Column: نرخ سود (Interest rate)
    public decimal? InterestRate { get; set; }
    
    // Column: مانده اول دوره (Beginning balance)
    public decimal? BeginningBalance { get; set; }
    
    // Column: مانده پایان دوره ریالی (Ending balance - Rial)
    public decimal? EndingBalanceRial { get; set; }
    
    // Column: نوع ارز (Currency type)
    public string? CurrencyType { get; set; }
    
    // Column: مبلغ ارزی (Foreign amount)
    public decimal? ForeignAmount { get; set; }
    
    // Column: معادل ریالی ارزی (Rial equivalent of foreign)
    public decimal? ForeignRialEquivalent { get; set; }
    
    // Column: کوتاه مدت (Short-term)
    public decimal? ShortTerm { get; set; }
    
    // Column: بلند مدت (Long-term)
    public decimal? LongTerm { get; set; }
    
    // Column: هزینه مالی (Financial expense)
    public decimal? FinancialExpense { get; set; }
    
    // Column: سایر توضیحات (Other descriptions)
    public string? OtherDescriptions { get; set; }
    
    // Helper properties
    public bool IsBankFacilities => RowCode == 11 || RowCode == 56;
    public bool IsSettledFacilities => RowCode == 13 || RowCode == 58;
    public bool IsTotal => RowCode == 14 || RowCode == 59;
    public bool IsTransferToAssets => RowCode == 15 || RowCode == 60;
    public bool IsFinancialExpense => RowCode == 16 || RowCode == 61;
    
    // Helper methods
    public static FinancingDetailItem? GetBankFacilities(...)
    public static FinancingDetailItem? GetSettledFacilities(...)
    public static FinancingDetailItem? GetTotal(...)
    public static FinancingDetailItem? GetTransferToAssets(...)
    public static FinancingDetailItem? GetFinancialExpense(...)
}
```

### Owned Entity: InvestmentIncomeItem

```csharp
public class InvestmentIncomeItem
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    public string? Id { get; set; }
    
    public string? ItemDescription { get; set; }
    public decimal? PreviousPeriodAmount { get; set; }
    public decimal? CurrentPeriodAmount { get; set; }
    public decimal? EstimatedPeriodAmount { get; set; }
    
    public bool IsDataRow => RowCode == -1;
    public bool IsTotalRow => RowCode == 22;
    
    public static List<InvestmentIncomeItem> GetDataRows(...)
    public static InvestmentIncomeItem? GetTotal(...)
}
```

### Owned Entity: MiscellaneousExpenseItem

```csharp
public class MiscellaneousExpenseItem
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    public string? Id { get; set; }
    
    public string? ItemDescription { get; set; }
    public decimal? PreviousPeriodAmount { get; set; }
    public decimal? CurrentPeriodAmount { get; set; }
    public decimal? EstimatedPeriodAmount { get; set; }
    
    public bool IsDataRow => RowCode == -1;
    public bool IsTotalRow => RowCode == 25;
    
    public static List<MiscellaneousExpenseItem> GetDataRows(...)
    public static MiscellaneousExpenseItem? GetTotal(...)
}
```

### Owned Entity: InterpretativeReportDescription

```csharp
public class InterpretativeReportDescription
{
    public int RowCode { get; set; }
    public string? Category { get; set; }
    public string? RowType { get; set; }
    public string? Description { get; set; }
    public string? SectionName { get; set; }  // To identify which section
}
```

## Implementation Files

### Domain Layer
1. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/CanonicalInterpretativeReportSummaryPage5.cs`
2. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/OtherOperatingIncomeItem.cs`
3. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/OtherNonOperatingExpenseItem.cs`
4. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/FinancingDetailItem.cs`
5. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/InvestmentIncomeItem.cs`
6. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/MiscellaneousExpenseItem.cs`
7. `src/Domain/Fundamental.Domain/Codals/Manufacturing/Entities/InterpretativeReportDescription.cs`

### Application Layer - DTOs
1. `src/Application/Fundamental.Application/Codals/Dto/InterpretativeReportSummaryPage5/V2/RootInterpretativeReportSummaryPage5V2.cs`
2. `src/Application/Fundamental.Application/Codals/Dto/InterpretativeReportSummaryPage5/V2/InterpretativeReportSummaryPage5V2.cs`
3. `src/Application/Fundamental.Application/Codals/Dto/InterpretativeReportSummaryPage5/V2/OtherOperatingIncomeV2.cs`
4. `src/Application/Fundamental.Application/Codals/Dto/InterpretativeReportSummaryPage5/V2/OtherNonOperatingExpensesV2.cs`
5. `src/Application/Fundamental.Application/Codals/Dto/InterpretativeReportSummaryPage5/V2/FinancingDetailsV2.cs`
6. (Additional DTOs for other sections...)

### Infrastructure Layer
1. `src/Infrastructure/Fundamental.Infrastructure/Configuration/Fundamental/Codals/Manufacturing/CanonicalInterpretativeReportSummaryPage5Configuration.cs`
2. `src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Processors/InterpretativeReportSummaryPage5/InterpretativeReportSummaryPage5V2Processor.cs`
3. `src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Processors/InterpretativeReportSummaryPage5/InterpretativeReportSummaryPage5MappingServiceV2.cs`
4. `src/Infrastructure/Fundamental.Infrastructure/Services/Codals/Manufacturing/Detectors/InterpretativeReportSummaryPage5Detector.cs`

### Test Layer
1. `tests/IntegrationTests/Codals/Manufacturing/InterpretativeReportSummaryPage5IntegrationTests.cs`
2. `tests/UnitTests/Domain.UnitTests/Codals/Manufacturing/OtherOperatingIncomeItemTests.cs`
3. `tests/UnitTests/Domain.UnitTests/Codals/Manufacturing/FinancingDetailItemTests.cs`

## Mapping Strategy

### Period Identification
From `yearData`, identify three periods:
1. **Previous Period** (PreviousPeriodAmount): Typically full fiscal year (period=12)
2. **Current Period** (CurrentPeriodAmount): Typically 6-month period (period=6)
3. **Estimated Period** (EstimatedPeriodAmount): Typically estimated 6-month period (period=12 or 6)

### Column Mapping Example (otherOperatingIncome)
- `value_2421` → ItemDescription
- `value_2422` → PreviousPeriodAmount (columnId: 2422)
- `value_2423` → CurrentPeriodAmount (columnId: 2423)
- `value_2424` → EstimatedPeriodAmount (columnId: 2424)

## XML Documentation Standards

Each property must have XML comments including:
- Persian caption from JSON
- Column ID reference
- Example: 
```csharp
/// <summary>
/// Item description.
/// Persian: عنوان قلم
/// Maps to: value_2421 (columnId: N/A - description column)
/// </summary>
public string? ItemDescription { get; set; }

/// <summary>
/// Amount for previous fiscal year.
/// Persian: سال مالی منتهی به 1403/12/30
/// Maps to: value_2422 (columnId: 2422)
/// </summary>
public decimal? PreviousPeriodAmount { get; set; }
```

## Testing Strategy

### Integration Tests Coverage
1. **Test V2 Processing**: Process complete IRO1SEPP0001.json
2. **Verify All Sections**: Assert all 14 sections are populated correctly
3. **Verify Helper Methods**: Test GetDataRows(), GetTotal(), etc.
4. **Verify Period Extraction**: FiscalYear, YearEndMonth, ReportMonth
5. **Verify Row Code Detection**: Data rows vs. summary rows
6. **Verify Raw JSON Storage**: Ensure original data is preserved

### Unit Tests Coverage
1. **Helper Methods**: Test static helper methods in owned entities
2. **IsDataRow/IsTotalRow**: Test property calculations
3. **Specific Getters**: Test financing detail getters

## Database Configuration

### EF Core Configuration
- Table: `canonical_interpretative_report_summary_page5`
- Schema: `manufacturing`
- Shadow FK: `symbol_id`
- JSONB columns for all owned collections
- Value objects: FiscalYear, YearEndMonth, ReportMonth

## Registration & DI

### Processor Registration
```csharp
public static IServiceCollection AddInterpretativeReportSummaryPage5Processors(
    this IServiceCollection serviceCollection)
{
    // Processor
    serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, 
        InterpretativeReportSummaryPage5V2Processor>();
    
    // Mapping service
    serviceCollection.AddKeyedScopedCanonicalMappingService<
        ICanonicalMappingService<CanonicalInterpretativeReportSummaryPage5, 
            CodalInterpretativeReportSummaryPage5V2>,
        InterpretativeReportSummaryPage5MappingServiceV2,
        CodalInterpretativeReportSummaryPage5V2>();
    
    // Version detector
    serviceCollection.AddScoped<ICodalVersionDetector, 
        InterpretativeReportSummaryPage5Detector>();
    
    return serviceCollection;
}
```

## Implementation Checklist

- [ ] Create domain entities (7 files)
- [ ] Create DTOs for V2 (10+ files)
- [ ] Create EF Core configuration
- [ ] Create mapping service V2
- [ ] Create processor V2
- [ ] Create version detector
- [ ] Update DbContext with new DbSet
- [ ] Register services in DI
- [ ] Create integration tests
- [ ] Create unit tests
- [ ] Update .github/copilot-instructions.md
- [ ] Test with real data
- [ ] Create EF migration (user will do this)

## Notes

- Keep `NonOperationIncomeAndExpensesV2Processor.cs` untouched (will be deleted by user later)
- Focus on V2 only
- Helper methods are in owned entities, not canonical entity
- Use first section with `yearData` to extract fiscal year info
- All sections share the same fiscal year/period metadata
