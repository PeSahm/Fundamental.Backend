using Fundamental.Application.Codals.Dto.MonthlyActivities.V1;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V1 data.
/// </summary>
public class MonthlyActivityMappingServiceV1 : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV1>
{
    /// <summary>
    /// Extracts the fiscal year from V1 financial year data.
    /// V1 fiscal year is PriodEndToDate year + 2.
    /// </summary>
    /// <param name="financialYear">The financial year data to extract from.</param>
    /// <returns>The extracted fiscal year.</returns>
    public static int ExtractFiscalYear(FinancialYearV1Dto financialYear)
    {
        if (!string.IsNullOrWhiteSpace(financialYear.PriodEndToDate) &&
            financialYear.PriodEndToDate.Contains('/'))
        {
            string[] parts = financialYear.PriodEndToDate.Split('/');

            if (parts.Length >= 1 && int.TryParse(parts[0], out int year))
            {
                return year + 2; // V1 fiscal year is PriodEndToDate year + 2
            }
        }

        throw new ArgumentException("Invalid or missing PriodEndToDate in financial year data", nameof(financialYear));
    }

    /// <summary>
    /// Maps a V1 Monthly Activity DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivityV1 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        // Extract fiscal year and report month from financial year data
        int fiscalYear = ExtractFiscalYear(dto.FinancialYear);
        int reportMonth = 1; // V1 reports annual data

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = "1",
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = reportMonth,
            HasSubCompanySale = false // V1 doesn't have this field, default to false
        };

        // Map ProductionAndSales
        if (dto.ProductAndSales != null)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV1(dto.ProductAndSales);
        }

        // Map descriptions
        if (dto.Description != null)
        {
            canonical.Descriptions = MapDescriptionsV1(dto.Description);
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

    private static List<ProductionAndSalesItem> MapProductionAndSalesV1(List<ProductAndSalesV1Dto> productAndSales)
    {
        List<ProductionAndSalesItem> items = new();

        foreach (ProductAndSalesV1Dto product in productAndSales)
        {
            ProductionAndSalesItem item = new()
            {
                ProductName = product.ProductName,
                Unit = product.ProductUnit,
                Category = ProductionSalesCategory.Internal, // V1 doesn't have category, default to internal
                RowCode = ProductionSalesRowCode.Data, // V1 items are all data rows
                Type = string.Empty, // V1 doesn't have type field

                // V1 has period and year data - map to appropriate fields
                MonthlyProductionQuantity = product.TotalProductionInPeriod,
                MonthlySalesQuantity = product.TotalSalesInPeriod,
                MonthlySalesRate = product.SalesRateInPeriod,
                MonthlySalesAmount = product.SalesAmountInPeriod,

                YearToDateProductionQuantity = product.TotalProductionInYear,
                YearToDateSalesQuantity = product.TotalSalesInYear,
                YearToDateSalesRate = product.SalesRateInYear,
                YearToDateSalesAmount = product.SalesAmountInYear
            };

            items.Add(item);
        }

        return items;
    }

    private static List<MonthlyActivityDescription> MapDescriptionsV1(DescriptionV1Dto description)
    {
        List<MonthlyActivityDescription> descriptions = new();

        // Add period description if not empty
        if (!string.IsNullOrEmpty(description.PeriodEndToDateDescription))
        {
            descriptions.Add(new MonthlyActivityDescription
            {
                RowCode = 1, // Period description
                Description = description.PeriodEndToDateDescription,
                Category = 0,
                RowType = "PeriodDescription"
            });
        }

        // Add year description if not empty
        if (!string.IsNullOrEmpty(description.YearEndToDateDescription))
        {
            descriptions.Add(new MonthlyActivityDescription
            {
                RowCode = 2, // Year description
                Description = description.YearEndToDateDescription,
                Category = 0,
                RowType = "YearDescription"
            });
        }

        return descriptions;
    }
}