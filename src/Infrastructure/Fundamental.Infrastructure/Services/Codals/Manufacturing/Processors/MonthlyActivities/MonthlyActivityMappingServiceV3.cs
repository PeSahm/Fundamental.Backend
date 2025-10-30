using Fundamental.Application.Codals.Dto.MonthlyActivities.V3;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V3 data.
/// </summary>
public class MonthlyActivityMappingServiceV3 : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV3>
{
    /// <summary>
    /// Maps a V3 Monthly Activity DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivityV3 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        if (dto.MonthlyActivity is null)
        {
            throw new ArgumentException("MonthlyActivity is null in V3 DTO", nameof(dto));
        }

        // Extract fiscal year and report month from productionAndSales yearData
        YearDataV3Dto? yearData = dto.MonthlyActivity.ProductionAndSales?.YearData.FirstOrDefault();
        int fiscalYear = ExtractFiscalYear(yearData);
        int reportMonth = ExtractReportMonth(dto.MonthlyActivity.ProductionAndSales);

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = "3",
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = reportMonth,
            HasSubCompanySale = false // V3 doesn't have this field, default to false
        };

        // Map ProductionAndSales
        if (dto.MonthlyActivity.ProductionAndSales?.RowItems != null)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV3(dto.MonthlyActivity.ProductionAndSales.RowItems);
        }

        // Map descriptions
        if (dto.MonthlyActivity.ProductMonthlyActivityDesc1?.RowItems != null)
        {
            canonical.Descriptions = MapDescriptionsV3(dto.MonthlyActivity.ProductMonthlyActivityDesc1.RowItems);
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

    private static int ExtractFiscalYear(YearDataV3Dto? yearData)
    {
        if (yearData != null && yearData.YearEndToDate.Contains('/'))
        {
            string[] parts = yearData.YearEndToDate.Split('/');

            if (parts.Length >= 1 && int.TryParse(parts[0], out int year))
            {
                return year + 1; // V3 fiscal year is yearEndToDate year + 1
            }
        }

        return DateTime.Now.Year; // fallback
    }

    private static int ExtractReportMonth(ProductionAndSalesV3Dto? productionAndSales)
    {
        // V3 and older versions report annual data, so ReportMonth is always 1
        return 1;
    }

    private static List<ProductionAndSalesItem> MapProductionAndSalesV3(List<ProductionAndSalesRowItemV3Dto> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11971)) // Has product name
            .Select(x => new ProductionAndSalesItem
            {
                ProductName = x.Value11971 ?? string.Empty,
                Unit = x.Value11972 ?? string.Empty,
                Category = (int?)x.Category,
                Type = x.Value119726 ?? string.Empty,

                // Map period data - V3 has different column structure
                // For V3, we map the corrected values to the main properties
                YearToDateProductionQuantity = ParseDecimal(x.Value119710),
                YearToDateSalesQuantity = ParseDecimal(x.Value119711),
                YearToDateSalesRate = ParseDecimal(x.Value119712),
                YearToDateSalesAmount = ParseDecimal(x.Value119713),

                // Monthly data
                MonthlyProductionQuantity = ParseDecimal(x.Value119714),
                MonthlySalesQuantity = ParseDecimal(x.Value119715),
                MonthlySalesRate = ParseDecimal(x.Value119716),
                MonthlySalesAmount = ParseDecimal(x.Value119717),

                // Cumulative data
                CumulativeToPeriodProductionQuantity = ParseDecimal(x.Value119718),
                CumulativeToPeriodSalesQuantity = ParseDecimal(x.Value119719),
                CumulativeToPeriodSalesRate = ParseDecimal(x.Value119720),
                CumulativeToPeriodSalesAmount = ParseDecimal(x.Value119721),

                // Previous year data
                PreviousYearProductionQuantity = ParseDecimal(x.Value119722),
                PreviousYearSalesQuantity = ParseDecimal(x.Value119723),
                PreviousYearSalesRate = ParseDecimal(x.Value119724),
                PreviousYearSalesAmount = ParseDecimal(x.Value119725)
            })
            .ToList();
    }

    private static List<MonthlyActivityDescription> MapDescriptionsV3(List<DescriptionRowItemV3Dto> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11991)) // Has description text
            .Select(x => new MonthlyActivityDescription
            {
                RowCode = x.RowCode,
                Description = x.Value11991 ?? string.Empty,
                Category = (int?)x.Category,
                RowType = x.RowType
            })
            .ToList();
    }

    private static decimal? ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        // Remove commas and try to parse
        string cleanValue = value.Replace(",", string.Empty);

        if (decimal.TryParse(cleanValue, out decimal result))
        {
            return result;
        }

        return null;
    }
}