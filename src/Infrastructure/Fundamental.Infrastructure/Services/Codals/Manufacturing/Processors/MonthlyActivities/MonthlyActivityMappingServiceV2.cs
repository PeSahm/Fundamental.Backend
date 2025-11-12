using Fundamental.Application.Codals.Dto.MonthlyActivities.V2;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
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
        int reportMonth = 1; // V2 and older versions report annual data

        // Create canonical entity
        CanonicalMonthlyActivity canonical = new CanonicalMonthlyActivity(
            Guid.NewGuid(),
            symbol,
            statement.TracingNo,
            statement.HtmlUrl,
            new FiscalYear(fiscalYear),
            new StatementMonth(12),
            new StatementMonth(reportMonth),
            statement.PublishDateMiladi,
            "2"
        );

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
        if (!string.IsNullOrWhiteSpace(financialYear.PriodEndToDate) &&
            financialYear.PriodEndToDate.Contains('/'))
        {
            string[] parts = financialYear.PriodEndToDate.Split('/');

            if (parts.Length >= 1 && int.TryParse(parts[0], out int year))
            {
                return year + 1; // V2 fiscal year is PriodEndToDate year + 1
            }
        }

        throw new ArgumentException("Invalid or missing PriodEndToDate in financial year data", nameof(financialYear));
    }

    private static List<ProductionAndSalesItem> MapProductionAndSalesV2(List<FieldsItemV2Dto> fieldsItems)
    {
        List<ProductionAndSalesItem> items = new();

        foreach (FieldsItemV2Dto fieldItem in fieldsItems)
        {
            // Create a single item per product, mapping typeId to appropriate time period fields
            ProductionAndSalesItem item = new()
            {
                ProductName = fieldItem.ProductName,
                Unit = fieldItem.ProductUnit,
                Category = ProductionSalesCategory.Internal, // V2 typically reports internal products
                RowCode = ProductionSalesRowCode.Data,
                Type = string.Empty // V2 doesn't have type field
            };

            // Map each typeId to the corresponding time period fields
            foreach (ProductV2Dto product in fieldItem.Products)
            {
                switch (product.TypeId)
                {
                    case 0: // از ابتدای سال مالی تا پایان دوره قبل (From beginning of fiscal year to end of previous period)
                        item.YearToDateProductionQuantity = product.TotalProduction;
                        item.YearToDateSalesQuantity = product.TotalSales;
                        item.YearToDateSalesRate = product.SalesRate;
                        item.YearToDateSalesAmount = product.SalesAmount;
                        break;

                    case 1: // از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده (Corrected previous period)
                        item.CorrectedYearToDateProductionQuantity = product.TotalProduction;
                        item.CorrectedYearToDateSalesQuantity = product.TotalSales;
                        item.CorrectedYearToDateSalesRate = product.SalesRate;
                        item.CorrectedYearToDateSalesAmount = product.SalesAmount;
                        break;

                    case 2: // دوره یک ماهه مربوط به ماه جاری (Current month one-month period)
                        item.MonthlyProductionQuantity = product.TotalProduction;
                        item.MonthlySalesQuantity = product.TotalSales;
                        item.MonthlySalesRate = product.SalesRate;
                        item.MonthlySalesAmount = product.SalesAmount;
                        break;

                    case 3: // از ابتدای سال تا پایان دوره جاری (From beginning of year to end of current period)
                        item.CumulativeToPeriodProductionQuantity = product.TotalProduction;
                        item.CumulativeToPeriodSalesQuantity = product.TotalSales;
                        item.CumulativeToPeriodSalesRate = product.SalesRate;
                        item.CumulativeToPeriodSalesAmount = product.SalesAmount;
                        break;
                }
            }

            items.Add(item);
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