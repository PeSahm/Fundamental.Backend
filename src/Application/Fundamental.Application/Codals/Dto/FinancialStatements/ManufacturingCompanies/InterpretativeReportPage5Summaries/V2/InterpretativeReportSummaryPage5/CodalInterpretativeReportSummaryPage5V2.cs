using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    InterpretativeReportSummaryPage5;

#pragma warning disable SA1402, SA1649

/// <summary>
/// Root DTO for interpretativeReportSummaryPage5 V2 JSON structure.
/// </summary>
public class RootInterpretativeReportSummaryPage5V2
{
    [JsonProperty("listedCapital")]
    public string? ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string? UnauthorizedCapital { get; set; }

    [JsonProperty("interpretativeReportSummaryPage5")]
    public CodalInterpretativeReportSummaryPage5V2? InterpretativeReportSummaryPage5 { get; set; }
}

/// <summary>
/// Main DTO for interpretativeReportSummaryPage5 V2.
/// Contains all 14 sections of the report.
/// </summary>
public class CodalInterpretativeReportSummaryPage5V2 : ICodalMappingServiceMetadata
{
    [JsonProperty("version")]
    public string? Version { get; set; }

    [JsonProperty("p5Desc1")]
    public DescriptionDto? P5Desc1 { get; set; }

    [JsonProperty("otherOperatingIncome")]
    public OtherOperatingIncomeDto? OtherOperatingIncome { get; set; }

    [JsonProperty("otherNonOperatingExpenses")]
    public OtherNonOperatingExpensesDto? OtherNonOperatingExpenses { get; set; }

    [JsonProperty("detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod")]
    public FinancingDetailsDto? DetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod { get; set; }

    [JsonProperty("detailsOfTheFinancingOfTheCompany-Est1")]
    public FinancingDetailsDto? DetailsOfTheFinancingOfTheCompanyEst1 { get; set; }

    [JsonProperty("detailsOfTheFinancingOfTheCompany-Est2")]
    public FinancingDetailsDto? DetailsOfTheFinancingOfTheCompanyEst2 { get; set; }

    [JsonProperty("descriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod")]
    public DescriptionDto? DescriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod { get; set; }

    [JsonProperty("companyEstimatesOfFinancingProgramsAndCompanyFinanceChanges")]
    public DescriptionDto? CompanyEstimatesOfFinancingProgramsAndCompanyFinanceChanges { get; set; }

    [JsonProperty("nonOperationIncomeAndExpensesInvestmentIncome")]
    public InvestmentIncomeDto? NonOperationIncomeAndExpensesInvestmentIncome { get; set; }

    [JsonProperty("nonOperationIncomeAndExpensesMiscellaneousItems")]
    public MiscellaneousItemsDto? NonOperationIncomeAndExpensesMiscellaneousItems { get; set; }

    [JsonProperty("corporateIncomeProgram")]
    public CorporateIncomeProgramDto? CorporateIncomeProgram { get; set; }

    [JsonProperty("otherImportantPrograms")]
    public OtherImportantProgramsDto? OtherImportantPrograms { get; set; }

    [JsonProperty("otherImportantNotes")]
    public DescriptionDto? OtherImportantNotes { get; set; }

    [JsonProperty("p5Desc2")]
    public DescriptionDto? P5Desc2 { get; set; }

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.InterimStatement;
    public CodalVersion CodalVersion => CodalVersion.V2;
    public LetterPart LetterPart => LetterPart.InterpretativeReportSummaryPage5;

    /// <summary>
    /// Validates if the report has the minimum required data.
    /// </summary>
    public bool IsValidReport()
    {
        if (OtherOperatingIncome?.YearData == null || !OtherOperatingIncome.YearData.Any())
        {
            return false;
        }

        YearDataDto? firstYear = OtherOperatingIncome.YearData.FirstOrDefault();

        if (firstYear?.FiscalYear == null || firstYear.FiscalMonth == null || firstYear.ReportMonth == null)
        {
            return false;
        }

        return true;
    }
}

#pragma warning restore SA1402