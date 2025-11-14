using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
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
    public Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivity dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        if (dto.MonthlyActivity is null)
        {
            throw new InvalidOperationException("MonthlyActivity is null in V4 DTO");
        }

        if (dto.MonthlyActivity.ProductionAndSales is null)
        {
            throw new InvalidOperationException("ProductionAndSales is null in V4 DTO");
        }

        if (dto.MonthlyActivity.ProductionAndSales.YearData is null)
        {
            throw new InvalidOperationException("YearData is null in V4 DTO");
        }

        if (dto.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            throw new InvalidOperationException("No year data found in V4 DTO");
        }

        YearDatum? yearDatum = dto.MonthlyActivity.ProductionAndSales.YearData
            .Find(x => x.ColumnId == SaleColumnId.ProduceThisMonth);

        if (yearDatum is null || yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
        {
            throw new InvalidOperationException("Could not extract fiscal year or report month from V4 data");
        }

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new(
            Guid.NewGuid(),
            symbol,
            statement.TracingNo,
            statement.HtmlUrl,
            yearDatum.FiscalYear.Value,
            yearDatum.FiscalMonth,
            yearDatum.ReportMonth,
            statement.PublishDateMiladi,
            nameof(CodalVersion.V4)
        );

        // Map ProductionAndSales
        if (dto.MonthlyActivity.ProductionAndSales?.RowItems != null)
        {
            canonical.ProductionAndSalesItems = MapProductionAndSalesV4(dto.MonthlyActivity.ProductionAndSales.RowItems);
        }

        // Map Energy (V4 has this section)
        if (dto.MonthlyActivity.Energy?.RowItems != null)
        {
            canonical.EnergyItems = MapEnergyV4(dto.MonthlyActivity.Energy.RowItems);
        }

        // Map descriptions
        if (dto.MonthlyActivity.ProductMonthlyActivityDesc1?.RowItems.Count > 0)
        {
            canonical.Descriptions = MapDescriptionsV4(dto.MonthlyActivity.ProductMonthlyActivityDesc1.RowItems);
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
                ProductName = x.Value11971,
                Unit = x.Value11972,
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
                Type = x.Value119726,

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

    private static List<EnergyItem> MapEnergyV4(List<EnergyRowItem> rowItems)
    {
        return rowItems
            .Where(x => !string.IsNullOrEmpty(x.Value31951))
            .Select(x => new EnergyItem
            {
                // Descriptive fields: value_31951-31954
                Industry = x.Value31951,
                Classification = x.Value31952,
                EnergyType = x.Value31953,
                Unit = x.Value31954,

                // Year-to-date values (consumption/rate/cost): value_31955-31957
                YearToDateConsumption = x.Value31955,
                YearToDateRate = x.Value31956,
                YearToDateCost = x.Value31957,

                // Corrected year-to-date values (consumption/rate/cost): value_319511-319513
                CorrectedYearToDateConsumption = x.Value319511,
                CorrectedYearToDateRate = x.Value319512,
                CorrectedYearToDateCost = x.Value319513,

                // Monthly values (consumption/rate/cost): value_319514-319516
                MonthlyConsumption = x.Value319514,
                MonthlyRate = x.Value319515,
                MonthlyCost = x.Value319516,

                // Cumulative to period values (consumption/rate/cost): value_319517-319519
                CumulativeToPeriodConsumption = x.Value319517,
                CumulativeToPeriodRate = x.Value319518,
                CumulativeToPeriodCost = x.Value319519,

                // Previous year values (consumption/rate/cost): value_319520-319522
                PreviousYearConsumption = x.Value319520,
                PreviousYearRate = x.Value319521,
                PreviousYearCost = x.Value319522,

                // Forecast and explanation: value_319523-319524
                ForecastYearConsumption = x.Value319523,
                ConsumptionChangeExplanation = x.Value319524,

                // Classification metadata
                RowCode = x.RowCode.HasValue
                    ? (EnergyRowCode)x.RowCode.Value
                    : EnergyRowCode.Data,
                Category = x.Category ?? 0
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
                Description = x.Value11991,
                Category = (int?)x.Category,
                RowType = x.RowType
            })
            .ToList();
    }
}