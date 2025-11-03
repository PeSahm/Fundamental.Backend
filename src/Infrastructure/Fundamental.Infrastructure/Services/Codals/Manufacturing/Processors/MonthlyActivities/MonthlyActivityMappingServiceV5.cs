using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V5 data.
/// </summary>
public class MonthlyActivityMappingServiceV5 : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>
{
    /// <summary>
    /// Maps a V5 Monthly Activity DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    public async Task<CanonicalMonthlyActivity> MapToCanonicalAsync(
        CodalMonthlyActivityV5 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        // Extract fiscal year and report month from productionAndSales yearData
        int fiscalYear = DateTime.Now.Year;
        int reportMonth = 1;

        if (dto.MonthlyActivity?.ProductionAndSales?.YearData?.Any() == true)
        {
            // Find the yearData entry with the highest period (most recent month)
            YearDatumV5 latestYearDatum = dto.MonthlyActivity.ProductionAndSales.YearData
                .OrderByDescending(yd => yd.Period ?? 0)
                .First();

            fiscalYear = latestYearDatum.FiscalYear ?? latestYearDatum.ReportYear ?? DateTime.Now.Year;
            reportMonth = latestYearDatum.Period ?? latestYearDatum.ReportMonth ?? 1;
        }

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity
        {
            Symbol = symbol,
            TraceNo = statement.TracingNo,
            Uri = statement.HtmlUrl,
            Version = dto.CodalVersion.ToString(),
            FiscalYear = fiscalYear,
            Currency = IsoCurrency.IRR,
            YearEndMonth = 12,
            ReportMonth = reportMonth,
            HasSubCompanySale = false
        };

        // Map all sections
        canonical.BuyRawMaterialItems = MapBuyRawMaterial(dto.MonthlyActivity?.BuyRawMaterial);
        canonical.ProductionAndSalesItems = MapProductionAndSales(dto.MonthlyActivity?.ProductionAndSales);
        canonical.EnergyItems = MapEnergy(dto.MonthlyActivity?.Energy);
        canonical.CurrencyExchangeItems = MapCurrencyExchange(dto.MonthlyActivity?.SourceUsesCurrency);
        canonical.Descriptions = MapDescriptions(dto.MonthlyActivity?.ProductMonthlyActivityDesc1);

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

        existing.BuyRawMaterialItems = updated.BuyRawMaterialItems;
        existing.ProductionAndSalesItems = updated.ProductionAndSalesItems;
        existing.EnergyItems = updated.EnergyItems;
        existing.CurrencyExchangeItems = updated.CurrencyExchangeItems;
        existing.Descriptions = updated.Descriptions;
    }

    private List<BuyRawMaterialItem> MapBuyRawMaterial(BuyRawMaterial? buyRawMaterial)
    {
        if (buyRawMaterial?.RowItems == null)
        {
            return new List<BuyRawMaterialItem>();
        }

        return buyRawMaterial.RowItems
            .Where(x => !string.IsNullOrEmpty(x.Value34641))
            .Select(x => new BuyRawMaterialItem
            {
                MaterialName = x.Value34641,
                Unit = x.Value34642,

                // Year-to-date values (up to end of previous period): value_34643-34645
                YearToDateQuantity = x.Value34643,
                YearToDateRate = x.Value34644,
                YearToDateAmount = x.Value34645,

                // Note: Correction fields (value_34646-34648) not yet in DTO
                // Note: Monthly fields (value_346412-346414) not yet in DTO
                // Note: Cumulative fields (value_346415-346417) not yet in DTO
                // Note: Previous year fields (value_346418-346420) not yet in DTO

                // Corrected year-to-date values: value_34649-346411
                CorrectedYearToDateQuantity = x.Value34649,
                CorrectedYearToDateRate = x.Value346410,
                CorrectedYearToDateAmount = x.Value346411,

                // Classification metadata
                RowCode = x.RowCode.HasValue
                    ? (BuyRawMaterialRowCode)x.RowCode.Value
                    : BuyRawMaterialRowCode.Data,
                Category = x.Category.HasValue
                    ? (BuyRawMaterialCategory)x.Category.Value
                    : BuyRawMaterialCategory.Domestic
            })
            .ToList();
    }

    private List<ProductionAndSalesItem> MapProductionAndSales(ProductionAndSalesV5? productionAndSales)
    {
        if (productionAndSales?.RowItems == null)
        {
            return new List<ProductionAndSalesItem>();
        }

        return productionAndSales.RowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11971))
            .Select(x => new ProductionAndSalesItem
            {
                ProductName = x.Value11971,
                Unit = x.Value11972,

                // Year-to-date values (up to end of previous period): value_11973-11976
                YearToDateProductionQuantity = x.Value11973,
                YearToDateSalesQuantity = x.Value11974,
                YearToDateSalesRate = x.Value11975,
                YearToDateSalesAmount = x.Value11976,

                // Correction values: value_11977-11979
                CorrectionProductionQuantity = x.Value11977,
                CorrectionSalesQuantity = x.Value11978,
                CorrectionSalesAmount = x.Value11979,

                // Corrected year-to-date values (up to end of previous period): value_119710-119713
                CorrectedYearToDateProductionQuantity = x.Value119710,
                CorrectedYearToDateSalesQuantity = x.Value119711,
                CorrectedYearToDateSalesRate = x.Value119712,
                CorrectedYearToDateSalesAmount = x.Value119713,

                // Monthly values (one month period for current month): value_119714-119717
                MonthlyProductionQuantity = x.Value119714,
                MonthlySalesQuantity = x.Value119715,
                MonthlySalesRate = x.Value119716,
                MonthlySalesAmount = x.Value119717,

                // Cumulative to current period values: value_119718-119721
                CumulativeToPeriodProductionQuantity = x.Value119718,
                CumulativeToPeriodSalesQuantity = x.Value119719,
                CumulativeToPeriodSalesRate = x.Value119720,
                CumulativeToPeriodSalesAmount = x.Value119721,

                // Previous year values (same period): value_119722-119725
                PreviousYearProductionQuantity = x.Value119722,
                PreviousYearSalesQuantity = x.Value119723,
                PreviousYearSalesRate = x.Value119724,
                PreviousYearSalesAmount = x.Value119725,

                // Additional metadata
                Type = x.Value119726,

                // Row classification metadata
                RowCode = (ProductionSalesRowCode)(x.RowCode ?? -1),
                Category = (ProductionSalesCategory)(x.Category ?? 1)
            })
            .ToList();
    }

    private List<EnergyItem> MapEnergy(Energy? energy)
    {
        if (energy?.RowItems == null)
        {
            return new List<EnergyItem>();
        }

        return energy.RowItems
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

                // Note: Correction fields (value_31958-319510) not yet in DTO

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

    private List<CurrencyExchangeItem> MapCurrencyExchange(SourceUsesCurrency? sourceUsesCurrency)
    {
        if (sourceUsesCurrency?.RowItems == null)
        {
            return new List<CurrencyExchangeItem>();
        }

        return sourceUsesCurrency.RowItems
            .Where(x => !string.IsNullOrEmpty(x.Value36401))
            .Select(x => new CurrencyExchangeItem
            {
                Description = x.Value36401,
                Currency = x.Value36402,

                // Year-to-date values (foreign/rate/rial): value_36403-36405
                YearToDateForeignAmount = x.Value36403,
                YearToDateExchangeRate = x.Value36404,
                YearToDateRialAmount = x.Value36405,

                // Note: Correction fields (value_36406-36408) not yet in DTO

                // Corrected year-to-date values (foreign/rate/rial): value_36409-364011
                CorrectedYearToDateForeignAmount = x.Value36409,
                CorrectedYearToDateExchangeRate = x.Value364010,
                CorrectedYearToDateRialAmount = x.Value364011,

                // Monthly values (foreign/rate/rial): value_364012-364014
                MonthlyForeignAmount = x.Value364012,
                MonthlyExchangeRate = x.Value364013,
                MonthlyRialAmount = x.Value364014,

                // Cumulative to period values (foreign/rate/rial): value_364015-364017
                CumulativeToPeriodForeignAmount = x.Value364015,
                CumulativeToPeriodExchangeRate = x.Value364016,
                CumulativeToPeriodRialAmount = x.Value364017,

                // Previous year values (foreign/rate/rial): value_364018-364020
                PreviousYearForeignAmount = x.Value364018,
                PreviousYearExchangeRate = x.Value364019,
                PreviousYearRialAmount = x.Value364020,

                // Forecast remaining values (foreign/rate/rial): value_364021-364023
                ForecastRemainingForeignAmount = x.Value364021,
                ForecastRemainingExchangeRate = x.Value364022,
                ForecastRemainingRialAmount = x.Value364023,

                // Classification metadata with enum casting and fallback defaults
                RowCode = x.RowCode.HasValue
                    ? (CurrencyExchangeRowCode)x.RowCode.Value
                    : CurrencyExchangeRowCode.Data,
                Category = x.Category.HasValue
                    ? (CurrencyExchangeCategory)x.Category.Value
                    : CurrencyExchangeCategory.Sources
            })
            .ToList();
    }

    private List<MonthlyActivityDescription> MapDescriptions(ProductMonthlyActivityDesc1V5? descriptions)
    {
        if (descriptions?.RowItems == null)
        {
            return new List<MonthlyActivityDescription>();
        }

        return descriptions.RowItems
            .Where(x => !string.IsNullOrEmpty(x.Value11991))
            .Select(x => new MonthlyActivityDescription
            {
                RowCode = x.RowCode,
                Description = x.Value11991,
                Category = x.Category,
                RowType = x.RowType
            })
            .ToList();
    }
}