using Fundamental.Application.Codals.Dto.MonthlyActivities.V2;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V2 data.
/// </summary>
public class MonthlyActivityMappingServiceV2 : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV2>
{
    /// <summary>
    /// Maps a V2 Monthly Activity DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivityV2 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        // Extract fiscal year and report month from financial year data
        int fiscalYear = ExtractFiscalYear(dto.ProductAndSales.FinancialYear);
        int reportMonth = ExtractReportMonth(dto.ProductAndSales.FinancialYear);

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = "2",
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = reportMonth,
            HasSubCompanySale = false // V2 doesn't have this field, default to false
        };

        // Map ProductionAndSales
        if (dto.ProductAndSales.FieldsItems != null)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV2(dto.ProductAndSales.FieldsItems);
        }

        // Map descriptions
        if (dto.ProductAndSales.Descriptions != null)
        {
            canonical.Descriptions = MapDescriptionsV2(dto.ProductAndSales.Descriptions);
        }

        return canonical;
    }

    /// <summary>
    /// Updates an existing canonical entity with data from another canonical entity.
    /// </summary>
    /// <param name="existing">The existing canonical entity to update.</param>
    /// <param name="updated">The updated canonical entity data.</param>
    public void UpdateCanonical(CanonicalMonthlyActivity existing, CanonicalMonthlyActivity updated)
    {
        existing.TraceNo = updated.TraceNo;
        existing.Uri = updated.Uri;
        existing.Currency = updated.Currency;
        existing.HasSubCompanySale = updated.HasSubCompanySale;

        // Update collections
        existing.ProductionAndSalesItems = updated.ProductionAndSalesItems;
        existing.Descriptions = updated.Descriptions;
    }

    private static int ExtractFiscalYear(FinancialYearV2Dto financialYear)
    {
        if (financialYear.PriodEndToDate.Contains('/'))
        {
            string[] parts = financialYear.PriodEndToDate.Split('/');

            if (parts.Length >= 1 && int.TryParse(parts[0], out int year))
            {
                return year + 1; // V2 fiscal year is PriodEndToDate year + 1
            }
        }

        return DateTime.Now.Year; // fallback
    }

    private static int ExtractReportMonth(FinancialYearV2Dto financialYear)
    {
        // V2 and older versions report annual data, so ReportMonth is always 1
        return 1;
    }

    private static List<ProductionAndSalesItem> MapProductionAndSalesV2(List<FieldsItemV2Dto> fieldsItems)
    {
        List<ProductionAndSalesItem> items = new();

        foreach (FieldsItemV2Dto fieldItem in fieldsItems)
        {
            // Map each product type (0, 1, 2, 3) to separate items
            foreach (ProductV2Dto product in fieldItem.Products)
            {
                ProductionAndSalesItem item = new()
                {
                    ProductName = fieldItem.ProductName,
                    Unit = fieldItem.ProductUnit,
                    Category = product.TypeId,
                    Type = string.Empty, // V2 doesn't have type field

                    // V2 has simpler structure - map to year-to-date fields
                    YearToDateProductionQuantity = product.TotalProduction,
                    YearToDateSalesQuantity = product.TotalSales,
                    YearToDateSalesRate = product.SalesRate,
                    YearToDateSalesAmount = product.SalesAmount
                };

                items.Add(item);
            }
        }

        return items;
    }

    private static List<MonthlyActivityDescription> MapDescriptionsV2(List<DescriptionV2Dto> descriptions)
    {
        return descriptions
            .Where(d => !string.IsNullOrEmpty(d.Description))
            .Select(d => new MonthlyActivityDescription
            {
                RowCode = int.TryParse(d.RowCode, out int rowCode) ? rowCode : null,
                Description = d.Description ?? string.Empty,
                Category = 0, // V2 doesn't have categories
                RowType = string.Empty // V2 doesn't have row types
            })
            .ToList();
    }
}