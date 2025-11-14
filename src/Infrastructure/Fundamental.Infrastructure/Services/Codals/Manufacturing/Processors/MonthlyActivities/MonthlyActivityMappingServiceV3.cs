using Fundamental.Application.Codals.Dto.MonthlyActivities.V3;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
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
    public Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivityV3 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        if (dto.MonthlyActivity is null)
        {
            throw new InvalidOperationException("MonthlyActivity is null in V3 DTO");
        }

        // Extract fiscal year and report month from productionAndSales yearData
        YearDataV3Dto? yearData = dto.MonthlyActivity.ProductionAndSales.YearData.FirstOrDefault();

        if (yearData is null)
        {
            throw new InvalidOperationException("Missing year data in V3 DTO");
        }

        int fiscalYear = yearData.FiscalYear;
        int reportMonth = yearData.ReportMonth;

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new(
            Guid.NewGuid(),
            symbol,
            statement.TracingNo,
            statement.HtmlUrl,
            fiscalYear,
            yearData.YearEndMonth,
            reportMonth,
            statement.PublishDateMiladi,
            nameof(CodalVersion.V3)
        );

        // Map ProductionAndSales
        if (dto.MonthlyActivity.ProductionAndSales.RowItems.Count > 0)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV3(dto.MonthlyActivity.ProductionAndSales.RowItems);
        }

        // Map descriptions
        if (dto.MonthlyActivity.ProductMonthlyActivityDesc1.RowItems.Count > 0)
        {
            canonical.Descriptions = MapDescriptionsV3(dto.MonthlyActivity.ProductMonthlyActivityDesc1.RowItems);
        }

        return Task.FromResult(canonical);
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

    // Removed legacy ExtractFiscalYear; logic centralized inside YearDataV3Dto.

    private static List<ProductionAndSalesItem> MapProductionAndSalesV3(List<ProductionAndSalesRowItemV3Dto> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11971)) // Has product name
            .Select(x => new ProductionAndSalesItem
            {
                ProductName = x.Value11971 ?? string.Empty,
                Unit = x.Value11972 ?? string.Empty,
                Category = (ProductionSalesCategory)x.Category,
                RowCode = (ProductionSalesRowCode)x.RowCode,
                Type = x.Value119726 ?? string.Empty,

                // Year-to-date data (original, before corrections)
                YearToDateProductionQuantity = ParseDecimal(x.Value11973),
                YearToDateSalesQuantity = ParseDecimal(x.Value11974),
                YearToDateSalesRate = ParseDecimal(x.Value11975),
                YearToDateSalesAmount = ParseDecimal(x.Value11976),

                // Correction values
                CorrectionProductionQuantity = ParseDecimal(x.Value11977),
                CorrectionSalesQuantity = ParseDecimal(x.Value11978),
                CorrectionSalesAmount = ParseDecimal(x.Value11979),

                // Corrected year-to-date data (after corrections)
                CorrectedYearToDateProductionQuantity = ParseDecimal(x.Value119710),
                CorrectedYearToDateSalesQuantity = ParseDecimal(x.Value119711),
                CorrectedYearToDateSalesRate = ParseDecimal(x.Value119712),
                CorrectedYearToDateSalesAmount = ParseDecimal(x.Value119713),

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
                Category = x.Category,
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