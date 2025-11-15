using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    InterpretativeReportSummaryPage5;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;

/// <summary>
/// Mapping service for InterpretativeReportSummaryPage5 V2 data.
/// Maps DTO to canonical entity with all 14 sections.
/// </summary>
public class InterpretativeReportSummaryPage5MappingServiceV2 : ICanonicalMappingService<CanonicalInterpretativeReportSummaryPage5,
    CodalInterpretativeReportSummaryPage5V2>
{
    public Task<CanonicalInterpretativeReportSummaryPage5> MapToCanonicalAsync(
        CodalInterpretativeReportSummaryPage5V2 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        // Extract fiscal year and report month from first section's yearData
        if (dto.OtherOperatingIncome?.YearData?.Any() != true)
        {
            throw new InvalidOperationException("No year data found in V2 DTO");
        }

        YearDataDto firstYear = dto.OtherOperatingIncome.YearData[0];

        if (firstYear.FiscalYear is null || firstYear.ReportMonth is null)
        {
            throw new InvalidOperationException("Could not extract fiscal year from V2 data");
        }

        int fiscalYear = firstYear.FiscalYear.Value;
        int reportMonth = firstYear.ReportMonth.Value;
        int yearEndMonth = firstYear.FiscalMonth ?? 12;

        CanonicalInterpretativeReportSummaryPage5 canonical = new(
            Guid.NewGuid(),
            symbol,
            statement.TracingNo,
            statement.HtmlUrl,
            new FiscalYear(fiscalYear),
            new StatementMonth(yearEndMonth),
            new StatementMonth(reportMonth),
            statement.PublishDateMiladi.ToUniversalTime(),
            "V2"
        );

        // Map all 14 sections
        canonical.OtherOperatingIncomes = MapOtherOperatingIncome(dto.OtherOperatingIncome);
        canonical.OtherNonOperatingExpenses = MapOtherNonOperatingExpenses(dto.OtherNonOperatingExpenses);
        canonical.FinancingDetails = MapFinancingDetails(dto.DetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod);
        canonical.FinancingDetailsEstimated = MapFinancingDetails(dto.DetailsOfTheFinancingOfTheCompanyEst1);
        canonical.InvestmentIncomes = MapInvestmentIncome(dto.NonOperationIncomeAndExpensesInvestmentIncome);
        canonical.MiscellaneousExpenses = MapMiscellaneousItems(dto.NonOperationIncomeAndExpensesMiscellaneousItems);
        canonical.Descriptions = MapAllDescriptions(dto);

        return Task.FromResult(canonical);
    }

    public void UpdateCanonical(CanonicalInterpretativeReportSummaryPage5 existing, CanonicalInterpretativeReportSummaryPage5 updated)
    {
        existing.OtherOperatingIncomes = updated.OtherOperatingIncomes;
        existing.OtherNonOperatingExpenses = updated.OtherNonOperatingExpenses;
        existing.FinancingDetails = updated.FinancingDetails;
        existing.FinancingDetailsEstimated = updated.FinancingDetailsEstimated;
        existing.InvestmentIncomes = updated.InvestmentIncomes;
        existing.MiscellaneousExpenses = updated.MiscellaneousExpenses;
        existing.Descriptions = updated.Descriptions;
    }

    private static InterpretativeReportRowType? ParseRowType(string? rowType)
    {
        return rowType switch
        {
            "CustomRow" => InterpretativeReportRowType.CustomRow,
            "FixedRow" => InterpretativeReportRowType.FixedRow,
            _ => null
        };
    }

    private static decimal? ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return decimal.TryParse(value, out decimal result) ? result : null;
    }

    private List<OtherOperatingIncomeItem> MapOtherOperatingIncome(OtherOperatingIncomeDto? dto)
    {
        if (dto?.RowItems == null)
        {
            return new List<OtherOperatingIncomeItem>();
        }

        return dto.RowItems
            .Select(x => new OtherOperatingIncomeItem
            {
                RowCode = (OtherOperatingIncomeRowCode)x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Id = x.Id,
                ItemDescription = x.GetDescription(),
                PreviousPeriodAmount = ParseDecimal(x.Value2422),
                CurrentPeriodAmount = ParseDecimal(x.Value2423),
                EstimatedPeriodAmount = ParseDecimal(x.Value2424)
            })
            .ToList();
    }

    private List<OtherNonOperatingExpenseItem> MapOtherNonOperatingExpenses(OtherNonOperatingExpensesDto? dto)
    {
        if (dto?.RowItems == null)
        {
            return new List<OtherNonOperatingExpenseItem>();
        }

        return dto.RowItems
            .Select(x => new OtherNonOperatingExpenseItem
            {
                RowCode = (OtherNonOperatingExpenseRowCode)x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Id = x.Id,
                ItemDescription = x.GetDescription(),
                PreviousPeriodAmount = ParseDecimal(x.Value3522),
                CurrentPeriodAmount = ParseDecimal(x.Value3523),
                EstimatedPeriodAmount = ParseDecimal(x.Value3524)
            })
            .ToList();
    }

    private List<FinancingDetailItem> MapFinancingDetails(FinancingDetailsDto? dto)
    {
        if (dto?.RowItems == null)
        {
            return new List<FinancingDetailItem>();
        }

        return dto.RowItems
            .Select(x => new FinancingDetailItem
            {
                RowCode = (FinancingDetailRowCode)x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                FinancingSource = x.Value2431 ?? x.Value23371,
                InterestRate = ParseDecimal(x.Value2432) ?? ParseDecimal(x.Value23372),
                BeginningBalance = ParseDecimal(x.Value2433) ?? ParseDecimal(x.Value23373),
                EndingBalanceRial = ParseDecimal(x.Value2434) ?? ParseDecimal(x.Value23374),
                CurrencyType = x.Value2435 ?? x.Value23375,
                ForeignAmount = ParseDecimal(x.Value2436) ?? ParseDecimal(x.Value23376),
                ForeignRialEquivalent = ParseDecimal(x.Value2437) ?? ParseDecimal(x.Value23377),
                ShortTerm = ParseDecimal(x.Value2438) ?? ParseDecimal(x.Value23378),
                LongTerm = ParseDecimal(x.Value2439) ?? ParseDecimal(x.Value23379),
                FinancialExpense = ParseDecimal(x.Value24310) ?? ParseDecimal(x.Value233710),
                OtherDescriptions = x.Value24311 ?? x.Value233711
            })
            .ToList();
    }

    private List<InvestmentIncomeItem> MapInvestmentIncome(InvestmentIncomeDto? dto)
    {
        if (dto?.RowItems == null)
        {
            return new List<InvestmentIncomeItem>();
        }

        return dto.RowItems
            .Select(x => new InvestmentIncomeItem
            {
                RowCode = (InvestmentIncomeRowCode)x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Id = x.Id,
                ItemDescription = x.GetDescription(),
                PreviousPeriodAmount = ParseDecimal(x.Value2452),
                CurrentPeriodAmount = ParseDecimal(x.Value2453),
                EstimatedPeriodAmount = ParseDecimal(x.Value2454)
            })
            .ToList();
    }

    private List<MiscellaneousExpenseItem> MapMiscellaneousItems(MiscellaneousItemsDto? dto)
    {
        if (dto?.RowItems == null)
        {
            return new List<MiscellaneousExpenseItem>();
        }

        return dto.RowItems
            .Select(x => new MiscellaneousExpenseItem
            {
                RowCode = (MiscellaneousExpenseRowCode)x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Id = x.Id,
                ItemDescription = x.GetDescription(),
                PreviousPeriodAmount = ParseDecimal(x.Value3532),
                CurrentPeriodAmount = ParseDecimal(x.Value3533),
                EstimatedPeriodAmount = ParseDecimal(x.Value3534)
            })
            .ToList();
    }

    private List<InterpretativeReportDescription> MapAllDescriptions(CodalInterpretativeReportSummaryPage5V2 dto)
    {
        List<InterpretativeReportDescription> descriptions = new();

        if (dto.P5Desc1?.RowItems != null)
        {
            descriptions.AddRange(dto.P5Desc1.RowItems.Select(x => new InterpretativeReportDescription
            {
                RowCode = x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Description = x.Value23331,
                SectionName = "P5Desc1"
            }));
        }

        if (dto.DescriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod?.RowItems != null)
        {
            descriptions.AddRange(dto.DescriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod.RowItems.Select(x =>
                new InterpretativeReportDescription
                {
                    RowCode = x.RowCode,
                    Category = x.Category,
                    RowType = ParseRowType(x.RowType),
                    Description = x.Value2531,
                    SectionName = "DescriptionForDetailsOfTheFinancing"
                }));
        }

        if (dto.CompanyEstimatesOfFinancingProgramsAndCompanyFinanceChanges?.RowItems != null)
        {
            descriptions.AddRange(dto.CompanyEstimatesOfFinancingProgramsAndCompanyFinanceChanges.RowItems.Select(x =>
                new InterpretativeReportDescription
                {
                    RowCode = x.RowCode,
                    Category = x.Category,
                    RowType = ParseRowType(x.RowType),
                    Description = x.Value2441,
                    SectionName = "CompanyEstimatesOfFinancingPrograms"
                }));
        }

        if (dto.CorporateIncomeProgram?.RowItems != null)
        {
            descriptions.AddRange(dto.CorporateIncomeProgram.RowItems.Select(x => new InterpretativeReportDescription
            {
                RowCode = x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                SectionName = "CorporateIncomeProgram",
                AdditionalValue1 = x.Value2461,
                AdditionalValue2 = x.Value2462,
                AdditionalValue3 = x.Value2463,
                AdditionalValue4 = x.Value2464,
                AdditionalValue5 = x.Value2465
            }));
        }

        if (dto.OtherImportantPrograms?.RowItems != null)
        {
            descriptions.AddRange(dto.OtherImportantPrograms.RowItems.Select(x => new InterpretativeReportDescription
            {
                RowCode = x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Description = x.Value2471,
                SectionName = "OtherImportantPrograms",
                AdditionalValue1 = x.Value2472
            }));
        }

        if (dto.OtherImportantNotes?.RowItems != null)
        {
            descriptions.AddRange(dto.OtherImportantNotes.RowItems.Select(x => new InterpretativeReportDescription
            {
                RowCode = x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Description = x.Value2481,
                SectionName = "OtherImportantNotes"
            }));
        }

        if (dto.P5Desc2?.RowItems != null)
        {
            descriptions.AddRange(dto.P5Desc2.RowItems.Select(x => new InterpretativeReportDescription
            {
                RowCode = x.RowCode,
                Category = x.Category,
                RowType = ParseRowType(x.RowType),
                Description = x.Value23461,
                SectionName = "P5Desc2"
            }));
        }

        return descriptions;
    }
}