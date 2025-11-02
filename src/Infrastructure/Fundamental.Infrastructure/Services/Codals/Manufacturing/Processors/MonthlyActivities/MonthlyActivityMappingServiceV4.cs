using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V4 data.
/// </summary>
public class MonthlyActivityMappingServiceV4 : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivity>
{
    /// <summary>
    /// Maps a V4 Monthly Activity DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivity dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        if (dto.MonthlyActivity is null)
        {
            throw new ArgumentException("MonthlyActivity is null in V4 DTO", nameof(dto));
        }

        if (dto.MonthlyActivity.ProductionAndSales is null)
        {
            throw new ArgumentException("ProductionAndSales is null in V4 DTO", nameof(dto));
        }

        if (dto.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            throw new ArgumentException("No year data found in V4 DTO", nameof(dto));
        }

        YearDatum? yearDatum = dto.MonthlyActivity.ProductionAndSales.YearData
            .Find(x => x.ColumnId == SaleColumnId.ProduceThisMonth);

        if (yearDatum is null || yearDatum.FiscalYear is null || yearDatum.ReportMonth is null)
        {
            throw new ArgumentException("Could not extract fiscal year or report month from V4 data", nameof(dto));
        }

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = "4",
            FiscalYear = yearDatum.FiscalYear.Value,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = yearDatum.ReportMonth.Value,
            HasSubCompanySale = false // Default value
        };

        // Map ProductionAndSales
        if (dto.MonthlyActivity.ProductionAndSales?.RowItems != null)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV4(dto.MonthlyActivity.ProductionAndSales.RowItems);
        }

        // Map BuyRawMaterial (V4 has this section)
        if (dto.MonthlyActivity.BuyRawMaterial?.RowItems != null)
        {
            canonical.BuyRawMaterialItems = MapBuyRawMaterialV4(dto.MonthlyActivity.BuyRawMaterial.RowItems);
        }

        // Map descriptions
        if (dto.MonthlyActivity.ProductMonthlyActivityDesc1?.RowItems != null)
        {
            canonical.Descriptions = MapDescriptionsV4(dto.MonthlyActivity.ProductMonthlyActivityDesc1.RowItems);
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
        existing.BuyRawMaterialItems = updated.BuyRawMaterialItems;
        existing.ProductionAndSalesItems = updated.ProductionAndSalesItems;
        existing.EnergyItems = updated.EnergyItems;
        existing.CurrencyExchangeItems = updated.CurrencyExchangeItems;
        existing.Descriptions = updated.Descriptions;
    }

    private static List<ProductionAndSalesItem> MapProductionAndSalesV4(List<RowItem> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11971)) // Has product name
            .Select(x => new ProductionAndSalesItem
            {
                ProductName = x.Value11971 ?? string.Empty,
                Unit = x.Value11972 ?? string.Empty,
                YearToDateProductionQuantity = x.Value11973,
                YearToDateSalesQuantity = x.Value11974,
                YearToDateSalesRate = x.Value11975,
                YearToDateSalesAmount = x.Value11976,
                CorrectedYearToDateProductionQuantity = x.Value119710,
                CorrectedYearToDateSalesQuantity = x.Value119711,
                CorrectedYearToDateSalesRate = x.Value119712,
                CorrectedYearToDateSalesAmount = x.Value119713,
                MonthlyProductionQuantity = x.Value119714,
                MonthlySalesQuantity = x.Value119715,
                MonthlySalesRate = x.Value119716,
                MonthlySalesAmount = x.Value119717,
                CumulativeToPeriodProductionQuantity = x.Value119718,
                CumulativeToPeriodSalesQuantity = x.Value119719,
                CumulativeToPeriodSalesRate = x.Value119720,
                CumulativeToPeriodSalesAmount = x.Value119721,
                PreviousYearProductionQuantity = x.Value119722,
                PreviousYearSalesQuantity = x.Value119723,
                PreviousYearSalesRate = x.Value119724,
                PreviousYearSalesAmount = x.Value119725,
                Type = x.Value119726 ?? string.Empty,

                // Row classification metadata - map from V4 enums to domain enums
                RowCode = x.RowCode.HasValue
                    ? (ProductionSalesRowCode)(int)x.RowCode.Value
                    : ProductionSalesRowCode.Data,
                Category = x.Category.HasValue
                    ? (ProductionSalesCategory)(int)x.Category.Value
                    : ProductionSalesCategory.Internal
            })
            .ToList();
    }

    private static List<BuyRawMaterialItem> MapBuyRawMaterialV4(List<RowItem> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value34641)) // Has material name
            .Select(x => new BuyRawMaterialItem
            {
                MaterialName = x.Value34641 ?? string.Empty,
                Unit = x.Value34642 ?? string.Empty,
                YearToDateQuantity = x.Value34643,
                YearToDateRate = x.Value34644,
                YearToDateAmount = x.Value34645,
                CorrectedYearToDateQuantity = x.Value34649,
                CorrectedYearToDateRate = x.Value346410,
                CorrectedYearToDateAmount = x.Value346411,
                MonthlyPurchaseQuantity = x.Value346412,
                MonthlyPurchaseRate = x.Value346413,
                MonthlyPurchaseAmount = x.Value346414,
                CumulativeToPeriodQuantity = x.Value346415,
                CumulativeToPeriodRate = x.Value346416,
                CumulativeToPeriodAmount = x.Value346417,
                PreviousYearQuantity = x.Value346418,
                PreviousYearRate = x.Value346419,
                PreviousYearAmount = x.Value346420,

                // Row classification metadata
                RowCode = x.RowCode.HasValue
                    ? (BuyRawMaterialRowCode)x.RowCode.Value
                    : BuyRawMaterialRowCode.Data,
                Category = x.Category.HasValue
                    ? (BuyRawMaterialCategory)x.Category.Value
                    : BuyRawMaterialCategory.Domestic
            })
            .ToList();
    }

    private static List<MonthlyActivityDescription> MapDescriptionsV4(List<RowItem> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11991)) // Has description text
            .Select(x => new MonthlyActivityDescription
            {
                RowCode = (int?)x.RowCode,
                Description = x.Value11991 ?? string.Empty,
                Category = (int?)x.Category,
                RowType = x.RowType ?? string.Empty
            })
            .ToList();
    }
}