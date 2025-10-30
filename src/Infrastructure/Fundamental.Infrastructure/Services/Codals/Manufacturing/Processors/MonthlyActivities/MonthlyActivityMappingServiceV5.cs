using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

/// <summary>
/// Mapping service for Monthly Activity V5 data.
/// </summary>
public class MonthlyActivityMappingServiceV5 : IMonthlyActivityMappingService
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
                YearToDateQuantity = x.Value34643,
                YearToDateRate = x.Value34644,
                YearToDateAmount = x.Value34645,
                CorrectedYearToDateQuantity = x.Value34649,
                CorrectedYearToDateRate = x.Value346410,
                CorrectedYearToDateAmount = x.Value346411
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
                YearToDateProductionQuantity = x.Value11973,
                YearToDateSalesQuantity = x.Value11974,
                YearToDateSalesRate = x.Value11975,
                YearToDateSalesAmount = x.Value11976,
                CorrectedYearToDateProductionQuantity = x.Value11979,
                CorrectedYearToDateSalesQuantity = x.Value119710,
                CorrectedYearToDateSalesRate = x.Value119711,
                CorrectedYearToDateSalesAmount = x.Value119712
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
                EnergyType = x.Value31951,
                Unit = x.Value31952,
                YearToDateConsumption = x.Value31955,
                YearToDateCost = x.Value31956,
                CorrectedYearToDateConsumption = x.Value319511,
                CorrectedYearToDateCost = x.Value319512
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
                YearToDateForeignAmount = x.Value36403,
                YearToDateExchangeRate = x.Value36404,
                YearToDateRialAmount = x.Value36405,
                CorrectedYearToDateForeignAmount = x.Value36409,
                CorrectedYearToDateExchangeRate = x.Value364010,
                CorrectedYearToDateRialAmount = x.Value364011
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
                Description = x.Value11991
            })
            .ToList();
    }
}