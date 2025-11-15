# ProportionedRetainedEarning Helper Methods Usage

## Overview
The `ProportionedRetainedEarning` entity now includes an enum-based field name (`ProportionedRetainedEarningFieldName`) and helper methods to easily extract specific values from a collection.

## Enum: ProportionedRetainedEarningFieldName

All possible field names with Persian descriptions:

```csharp
public enum ProportionedRetainedEarningFieldName
{
    NetIncomeLoss,                                      // سود (زیان) خالص
    BeginingRetainedEarnings,                          // سود (زیان) انباشته ابتدای دوره
    AnnualAdjustment,                                   // تعدیلات سنواتی
    AdjustedBeginingRetainedEarnings,                  // سود (زیان) انباشته ابتدای دوره تعدیل‌شده
    PreYearDevidedRetainedEarning,                     // سود سهام مصوب (مجمع سال قبل)
    TransferToCapital,                                  // تغییرات سرمایه از محل سود (زیان) انباشته
    UnallocatedRetainedEarningsAtTheBeginningOfPeriod, // سود انباشته ابتدای دوره تخصیص نیافته
    TransfersFromOtherEquityItems,                     // انتقال از سایر اقلام حقوق صاحبان سهام
    ProportionableRetainedEarnings,                    // سود قابل تخصیص
    LegalReserve,                                       // انتقال به اندوخته‌ قانونی
    ExtenseReserve,                                     // انتقال به سایر اندوخته‌ها
    EndingRetainedEarnings,                            // سود (زیان) انباشته پايان دوره
    DividedRetainedEarning,                            // سود سهام مصوب (مجمع سال جاری)
    TotalEndingRetainedEarnings,                       // سود (زیان) انباشته پایان دوره (با لحاظ نمودن مصوبات مجمع)
    EarningsPerShareAfterTax,                          // سود (زیان) خالص هر سهم- ریال
    DividendPerShare,                                   // سود نقدی هر سهم (ریال)
    ListedCapital,                                      // سرمایه
}
```

## Helper Methods

### 1. GetByFieldName - Get the entire item by field name

```csharp
public static ProportionedRetainedEarning? GetByFieldName(
    IEnumerable<ProportionedRetainedEarning> items,
    ProportionedRetainedEarningFieldName fieldName)
```

**Example:**
```csharp
// Get the complete item for "Approved dividend from previous year's assembly"
var dividendItem = ProportionedRetainedEarning.GetByFieldName(
    assembly.ProportionedRetainedEarnings,
    ProportionedRetainedEarningFieldName.PreYearDevidedRetainedEarning
);

if (dividendItem != null)
{
    Console.WriteLine($"Description: {dividendItem.Description}");
    Console.WriteLine($"Value: {dividendItem.YearEndToDateValue}");
    Console.WriteLine($"Row Class: {dividendItem.RowClass}");
}
```

### 2. GetValue - Get just the value for a specific field

```csharp
public static decimal? GetValue(
    IEnumerable<ProportionedRetainedEarning> items,
    ProportionedRetainedEarningFieldName fieldName)
```

**Example:**
```csharp
// Extract "سود سهام مصوب مجمع سال قبل" (Approved dividend from previous year)
decimal? approvedDividend = ProportionedRetainedEarning.GetValue(
    assembly.ProportionedRetainedEarnings,
    ProportionedRetainedEarningFieldName.PreYearDevidedRetainedEarning
);

if (approvedDividend.HasValue)
{
    Console.WriteLine($"Approved Dividend: {approvedDividend.Value:N0} Rials");
}
```

## Complete Usage Example

```csharp
// Assume we have a CanonicalAnnualAssembly entity
var assembly = await repository.GetByIdAsync(assemblyId);

if (assembly?.ProportionedRetainedEarnings == null)
{
    return;
}

var earnings = assembly.ProportionedRetainedEarnings;

// Extract multiple values at once
var netIncome = ProportionedRetainedEarning.GetValue(
    earnings, 
    ProportionedRetainedEarningFieldName.NetIncomeLoss
);

var approvedDividendPreviousYear = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.PreYearDevidedRetainedEarning
);

var approvedDividendCurrentYear = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.DividedRetainedEarning
);

var earningsPerShare = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.EarningsPerShareAfterTax
);

var dividendPerShare = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.DividendPerShare
);

var capital = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.ListedCapital
);

// Display results
Console.WriteLine("=== Financial Summary ===");
Console.WriteLine($"Net Income: {netIncome:N0} Rials");
Console.WriteLine($"Previous Year Dividend: {approvedDividendPreviousYear:N0} Rials");
Console.WriteLine($"Current Year Dividend: {approvedDividendCurrentYear:N0} Rials");
Console.WriteLine($"Earnings Per Share: {earningsPerShare:N0} Rials");
Console.WriteLine($"Dividend Per Share: {dividendPerShare:N0} Rials");
Console.WriteLine($"Capital: {capital:N0} Rials");
```

## From the JSON Sample (IRO1BAHN0001.json)

Based on the test data, here's what you would extract:

```csharp
// NetIncomeLoss: 25,528,570
var netIncome = ProportionedRetainedEarning.GetValue(
    earnings, 
    ProportionedRetainedEarningFieldName.NetIncomeLoss
); // Returns: 25528570

// PreYearDevidedRetainedEarning: -14,135,416
var prevYearDividend = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.PreYearDevidedRetainedEarning
); // Returns: -14135416

// DividedRetainedEarning: -25,197,915
var currentYearDividend = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.DividedRetainedEarning
); // Returns: -25197915

// EarningsPerShareAfterTax: 1,038
var eps = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.EarningsPerShareAfterTax
); // Returns: 1038

// DividendPerShare: 1,025
var dps = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.DividendPerShare
); // Returns: 1025

// ListedCapital: 24,583,332
var capital = ProportionedRetainedEarning.GetValue(
    earnings,
    ProportionedRetainedEarningFieldName.ListedCapital
); // Returns: 24583332
```

## Key Benefits

1. **Type Safety**: No more string matching - use the enum
2. **IntelliSense**: IDE autocomplete shows all available fields
3. **Persian Documentation**: Each enum value has Persian description in XML comments
4. **Null Safety**: Methods return nullable values when item not found
5. **Convenience**: Get just the value without dealing with the full object
