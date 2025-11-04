# Angular UI Development Guide: Monthly Activity Detail Page

## Overview
You are building an Angular page to display detailed monthly activity data from the **Fundamental.Backend** API. This is Iranian financial disclosure data (CODAL) showing production, sales, raw materials, energy consumption, and currency exchange information for manufacturing companies.

---

## API Endpoint
**GET** `/Manufacturing/monthly-activity/{id}`

**Response Type:** `Response<GetMonthlyActivityDetailItem>`

---

## Data Structure & Entity Relationships

### Root Entity: `GetMonthlyActivityDetailItem`
```typescript
interface GetMonthlyActivityDetailItem {
  id: string;                    // GUID
  isin: string;                  // Stock symbol ISIN code
  symbol: string;                // Symbol name
  title: string;                 // Company full title
  uri: string;                   // CODAL report URI
  version: string;               // Data format version (e.g., "5")
  fiscalYear: number;            // Persian calendar year (e.g., 1404)
  yearEndMonth: number;          // Fiscal year-end month (usually 12)
  reportMonth: number;           // Report month (1-12)
  hasSubCompanySale: boolean;    // Whether company has subsidiary sales
  traceNo: number;               // CODAL trace number
  
  // Collections of detailed items
  productionAndSalesItems: ProductionAndSalesItem[];
  buyRawMaterialItems: BuyRawMaterialItem[];
  energyItems: EnergyItem[];
  currencyExchangeItems: CurrencyExchangeItem[];
  descriptions: MonthlyActivityDescription[];
}
```

---

## Critical UI Pattern: Data Rows vs Summary Rows

### ⚠️ IMPORTANT: All item collections contain BOTH data rows AND summary rows

Each collection (Production/Sales, Raw Materials, Energy, Currency Exchange) contains:
1. **Data Rows** - Individual detail records (products, materials, energy types, etc.)
2. **Summary Rows** - Calculated totals, subtotals, and aggregations

**You MUST distinguish between these types when rendering grids.**

### Detection Pattern
All item types have these properties:
```typescript
interface ItemWithRowType {
  rowCode: number;      // Enum value identifying row type
  category: number;     // Enum value for categorization
  isDataRow: boolean;   // Computed property (NOT in JSON, calculate client-side)
  isSummaryRow: boolean; // Computed property (NOT in JSON, calculate client-side)
}
```

**Client-side computation:**
```typescript
// For ProductionAndSalesItem
item.isDataRow = item.rowCode === -1;
item.isSummaryRow = item.rowCode !== -1;

// For BuyRawMaterialItem
item.isDataRow = item.rowCode === -1;
item.isSummaryRow = item.rowCode !== -1;

// For EnergyItem
item.isDataRow = item.rowCode === -1;
item.isSummaryRow = item.rowCode !== -1;

// For CurrencyExchangeItem
item.isDataRow = item.rowCode === -1;
item.isSummaryRow = item.rowCode !== -1;
```

---

## 1. Production & Sales Items

### Row Types (ProductionSalesRowCode)
```typescript
enum ProductionSalesRowCode {
  Data = -1,           // Individual product data
  InternalSale = 5,    // Sum of domestic sales (blue box in report)
  ExportSale = 8,      // Sum of export sales (red box in report)
  ServiceIncome = 11,  // Sum of service revenues
  ReturnSale = 14,     // Sum of returned sales
  Discount = 15,       // Sum of discounts
  TotalSum = 16        // Grand total (green box - most important)
}
```

### Categories (ProductionSalesCategory)
```typescript
enum ProductionSalesCategory {
  Sum = 0,        // Summary/Total row
  Internal = 1,   // Internal (domestic) sale
  Export = 2,     // Export sale
  Service = 3,    // Service income
  Return = 4,     // Return sale
  Discount = 5    // Discount
}
```

### Data Model
```typescript
interface ProductionAndSalesItem {
  productName: string;
  unit: string;
  
  // Year-to-date (up to previous period end)
  yearToDateProductionQuantity?: number;
  yearToDateSalesQuantity?: number;
  yearToDateSalesRate?: number;         // Rials
  yearToDateSalesAmount?: number;       // Million Rials
  
  // Corrections
  correctionProductionQuantity?: number;
  correctionSalesQuantity?: number;
  correctionSalesAmount?: number;
  
  // Corrected year-to-date
  correctedYearToDateProductionQuantity?: number;
  correctedYearToDateSalesQuantity?: number;
  correctedYearToDateSalesRate?: number;
  correctedYearToDateSalesAmount?: number;
  
  // Monthly (current month)
  monthlyProductionQuantity?: number;
  monthlySalesQuantity?: number;
  monthlySalesRate?: number;
  monthlySalesAmount?: number;
  
  // Cumulative to current period
  cumulativeToPeriodProductionQuantity?: number;
  cumulativeToPeriodSalesQuantity?: number;
  cumulativeToPeriodSalesRate?: number;
  cumulativeToPeriodSalesAmount?: number;
  
  // Previous year comparison
  previousYearProductionQuantity?: number;
  previousYearSalesQuantity?: number;
  previousYearSalesRate?: number;
  previousYearSalesAmount?: number;
  
  type: string;     // Product status (e.g., "تولید")
  
  // Classification (CRITICAL for UI)
  rowCode: ProductionSalesRowCode;
  category: ProductionSalesCategory;
}
```

### UI Rendering Strategy
```typescript
// Separate data and summary rows
const dataRows = items.filter(i => i.rowCode === -1);
const summaryRows = items.filter(i => i.rowCode !== -1);

// Group data by category
const internalProducts = dataRows.filter(i => i.category === 1);
const exportProducts = dataRows.filter(i => i.category === 2);

// Get specific summary rows
const internalSum = summaryRows.find(i => i.rowCode === 5 && i.category === 0);
const exportSum = summaryRows.find(i => i.rowCode === 8 && i.category === 0);
const grandTotal = summaryRows.find(i => i.rowCode === 16 && i.category === 0);
```

### Recommended Grid Layout
1. **Section 1: Internal Sales**
   - Data rows (category = 1, rowCode = -1)
   - Summary row (rowCode = 5) - styled as **blue** subtotal
2. **Section 2: Export Sales**
   - Data rows (category = 2, rowCode = -1)
   - Summary row (rowCode = 8) - styled as **red** subtotal
3. **Section 3: Grand Total**
   - Summary row (rowCode = 16) - styled as **green** grand total

**Styling:**
- Data rows: Normal styling
- Summary rows: Bold, colored background (blue/red/green per Iranian CODAL conventions)
- Use `productName` for row labels (summary rows contain Persian text like "جمع فروش داخلی")

---

## 2. Buy Raw Material Items

### Row Types (BuyRawMaterialRowCode)
```typescript
enum BuyRawMaterialRowCode {
  Data = -1,         // Individual material data
  DomesticSum = 21,  // Sum of domestic purchases
  ImportedSum = 24,  // Sum of imported purchases
  TotalSum = 25      // Grand total of all purchases
}
```

### Categories (BuyRawMaterialCategory)
```typescript
enum BuyRawMaterialCategory {
  Total = 0,      // Total/Sum row
  Domestic = 1,   // Domestic material (خرید داخلی)
  Imported = 2    // Imported material (خرید وارداتی)
}
```

### Data Model
```typescript
interface BuyRawMaterialItem {
  materialName: string;   // Material description (e.g., "مارل", "سنگ آهک")
  unit: string;
  
  // Year-to-date
  yearToDateQuantity?: number;
  yearToDateRate?: number;        // Rials
  yearToDateAmount?: number;      // Million Rials
  
  // Corrections
  correctionQuantity?: number;
  correctionRate?: number;
  correctionAmount?: number;
  
  // Corrected year-to-date
  correctedYearToDateQuantity?: number;
  correctedYearToDateRate?: number;
  correctedYearToDateAmount?: number;
  
  // Monthly purchase
  monthlyPurchaseQuantity?: number;
  monthlyPurchaseRate?: number;
  monthlyPurchaseAmount?: number;
  
  // Cumulative to period
  cumulativeToPeriodQuantity?: number;
  cumulativeToPeriodRate?: number;
  cumulativeToPeriodAmount?: number;
  
  // Previous year
  previousYearQuantity?: number;
  previousYearRate?: number;
  previousYearAmount?: number;
  
  // Classification
  rowCode: BuyRawMaterialRowCode;
  category: BuyRawMaterialCategory;
}
```

### UI Rendering Strategy
```typescript
// Group by category
const domesticMaterials = items.filter(i => i.category === 1 && i.rowCode === -1);
const importedMaterials = items.filter(i => i.category === 2 && i.rowCode === -1);

// Get sums
const domesticSum = items.find(i => i.rowCode === 21);
const importedSum = items.find(i => i.rowCode === 24);
const totalSum = items.find(i => i.rowCode === 25);
```

### Recommended Grid Layout
1. **Domestic Materials Section**
   - Data rows (category = 1, rowCode = -1)
   - Domestic sum row (rowCode = 21)
2. **Imported Materials Section**
   - Data rows (category = 2, rowCode = -1)
   - Imported sum row (rowCode = 24)
3. **Total Section**
   - Total sum row (rowCode = 25) - bold styling

---

## 3. Energy Items

### Row Types (EnergyRowCode)
```typescript
enum EnergyRowCode {
  Data = -1,      // Individual energy type data
  TotalSum = 1    // Sum of all energy consumption
}
```

### Data Model
```typescript
interface EnergyItem {
  industry: string;           // Industry classification
  classification: string;     // Energy classification
  energyType: string;         // Type of energy (e.g., "برق", "گاز")
  unit: string;
  
  // Year-to-date consumption
  yearToDateConsumption?: number;
  yearToDateRate?: number;      // Rials
  yearToDateCost?: number;      // Million Rials
  
  // Corrected year-to-date
  correctedYearToDateConsumption?: number;
  correctedYearToDateRate?: number;
  correctedYearToDateCost?: number;
  
  // Monthly
  monthlyConsumption?: number;
  monthlyRate?: number;
  monthlyCost?: number;
  
  // Cumulative to period
  cumulativeToPeriodConsumption?: number;
  cumulativeToPeriodRate?: number;
  cumulativeToPeriodCost?: number;
  
  // Previous year
  previousYearConsumption?: number;
  previousYearRate?: number;
  previousYearCost?: number;
  
  // Forecast
  forecastYearConsumption?: number;
  consumptionChangeExplanation: string;
  
  // Classification
  rowCode: EnergyRowCode;
  category: number;
}
```

### UI Rendering Strategy
```typescript
const dataRows = items.filter(i => i.rowCode === -1);
const totalRow = items.find(i => i.rowCode === 1);
```

---

## 4. Currency Exchange Items

### Row Types (CurrencyExchangeRowCode)
```typescript
enum CurrencyExchangeRowCode {
  Data = -1,        // Individual transaction data
  SourcesSum = 1,   // Sum of all currency sources
  UsesSum = 2       // Sum of all currency uses
}
```

### Categories (CurrencyExchangeCategory)
```typescript
enum CurrencyExchangeCategory {
  Sources = 1,  // Currency sources (e.g., export sales, foreign loans)
  Uses = 2      // Currency uses (e.g., imports, payments)
}
```

### Data Model
```typescript
interface CurrencyExchangeItem {
  description: string;    // Transaction description
  currency: string;       // Currency code (e.g., "USD", "EUR")
  
  // Year-to-date
  yearToDateForeignAmount?: number;
  yearToDateExchangeRate?: number;
  yearToDateRialAmount?: number;
  
  // Corrected year-to-date
  correctedYearToDateForeignAmount?: number;
  correctedYearToDateExchangeRate?: number;
  correctedYearToDateRialAmount?: number;
  
  // Monthly
  monthlyForeignAmount?: number;
  monthlyExchangeRate?: number;
  monthlyRialAmount?: number;
  
  // Cumulative to period
  cumulativeToPeriodForeignAmount?: number;
  cumulativeToPeriodExchangeRate?: number;
  cumulativeToPeriodRialAmount?: number;
  
  // Previous year
  previousYearForeignAmount?: number;
  previousYearExchangeRate?: number;
  previousYearRialAmount?: number;
  
  // Forecast remaining
  forecastRemainingForeignAmount?: number;
  forecastRemainingExchangeRate?: number;
  forecastRemainingRialAmount?: number;
  
  // Classification
  rowCode: CurrencyExchangeRowCode;
  category: CurrencyExchangeCategory;
}
```

### UI Rendering Strategy
```typescript
// Sources section
const sourcesData = items.filter(i => i.category === 1 && i.rowCode === -1);
const sourcesSum = items.find(i => i.rowCode === 1);

// Uses section
const usesData = items.filter(i => i.category === 2 && i.rowCode === -1);
const usesSum = items.find(i => i.rowCode === 2);
```

### Recommended Grid Layout
1. **Currency Sources Section** (منابع ارزی)
   - Data rows (category = 1, rowCode = -1)
   - Sources sum row (rowCode = 1)
2. **Currency Uses Section** (مصارف ارزی)
   - Data rows (category = 2, rowCode = -1)
   - Uses sum row (rowCode = 2)

---

## 5. Monthly Activity Descriptions

Simple text descriptions and notes:
```typescript
interface MonthlyActivityDescription {
  rowCode: number;
  description: string;
  category: number;
  rowType: string;
}
```

Display as formatted text blocks or expandable sections.

---

## UI Component Architecture Recommendations

### Component Structure
```
MonthlyActivityDetailComponent
├── MonthlyActivityHeaderComponent (metadata)
├── ProductionSalesGridComponent (with data/summary separation)
├── RawMaterialsGridComponent (with data/summary separation)
├── EnergyConsumptionGridComponent (with data/summary separation)
├── CurrencyExchangeGridComponent (with data/summary separation)
└── DescriptionsComponent
```

### Grid Component Pattern (Reusable)
Each grid component should:
1. Accept an `items` array input
2. Automatically detect and separate data rows from summary rows
3. Apply conditional styling based on `rowCode`
4. Group by `category` where applicable
5. Display summary rows with visual emphasis (bold, colored backgrounds)

```typescript
// Example ProductionSalesGridComponent logic
export class ProductionSalesGridComponent {
  @Input() items: ProductionAndSalesItem[] = [];
  
  dataRows: ProductionAndSalesItem[] = [];
  summaryRows: ProductionAndSalesItem[] = [];
  
  ngOnInit() {
    this.dataRows = this.items.filter(i => i.rowCode === -1);
    this.summaryRows = this.items.filter(i => i.rowCode !== -1);
  }
  
  getRowClass(item: ProductionAndSalesItem): string {
    if (item.rowCode === 16) return 'total-sum-row';  // Green
    if (item.rowCode === 5) return 'internal-sum-row'; // Blue
    if (item.rowCode === 8) return 'export-sum-row';   // Red
    return 'data-row';
  }
}
```

---

## Styling Guidelines

### CODAL Iranian Convention Colors
- **Internal Sales Sum**: Light blue background (`#E3F2FD`)
- **Export Sales Sum**: Light red background (`#FFEBEE`)
- **Grand Total**: Light green background (`#E8F5E9`)
- **Summary rows**: Bold text, slightly larger font
- **Data rows**: Normal styling

### Persian (RTL) Layout
- Use `dir="rtl"` for proper Persian text rendering
- All numeric columns should align right
- Use Persian numerals if required by your localization

---

## Column Display Order (Typical CODAL Report)

For all grids, column order should follow this pattern:
1. Name/Description (product, material, energy type, etc.)
2. Unit
3. Year-to-date values (up to previous period)
4. Corrections (if applicable)
5. Corrected year-to-date
6. Monthly values (current period)
7. Cumulative to current period
8. Previous year comparison

---

## Data Validation & Edge Cases

1. **Null/Missing Values**: All numeric fields are nullable - display as empty or "—"
2. **Version Differences**: `version` field indicates data format version (V1-V5). Older versions may have fewer columns.
3. **Empty Collections**: Handle cases where collections are empty (no production, no energy data, etc.)
4. **Summary Row Missing**: Some reports may not have all summary rows - handle gracefully

---

## API Error Handling

Expected error scenarios:
- 404: Monthly activity not found
- 403: Unauthorized access
- 500: Server error

Handle with user-friendly messages in Persian.

---

## Performance Considerations

1. **Large datasets**: Some reports have 100+ product lines. Use virtual scrolling for grids.
2. **Lazy loading**: Consider tabs or accordions to load sections on demand.
3. **Caching**: Cache the detail response for navigation back/forward.

---

## Example Angular Service

```typescript
@Injectable({ providedIn: 'root' })
export class MonthlyActivityService {
  private apiUrl = 'Manufacturing/monthly-activity';
  
  constructor(private http: HttpClient) {}
  
  getDetail(id: string): Observable<Response<GetMonthlyActivityDetailItem>> {
    return this.http.get<Response<GetMonthlyActivityDetailItem>>(
      `${this.apiUrl}/${id}`
    );
  }
}
```

---

## Testing Checklist

- [ ] Data rows render correctly
- [ ] Summary rows render with proper styling
- [ ] Grid grouping by category works
- [ ] RTL layout displays correctly
- [ ] Empty collections show appropriate messages
- [ ] All numeric fields handle nulls
- [ ] Print/export functionality preserves structure
- [ ] Mobile responsive layout

---

## Summary: Key Takeaways

1. **Always separate data rows (rowCode = -1) from summary rows (rowCode ≠ -1)**
2. **Use `rowCode` and `category` enums for filtering and styling**
3. **Summary rows contain aggregated data and Persian labels in the name fields**
4. **Apply CODAL color conventions (blue/red/green) to summary rows**
5. **Handle nullable numeric fields gracefully**
6. **Support RTL layout for Persian text**
7. **Use virtual scrolling for large datasets**

---

## Questions or Clarifications?

If the data structure is unclear or you need specific column mappings, refer to:
- Backend entity: `CanonicalMonthlyActivity` (domain model)
- DTO: `GetMonthlyActivityDetailItem` (API response)
- Controller: `MonthlyActivityController.GetMonthlyActivity(id)` endpoint

Good luck building the UI! 🎯
