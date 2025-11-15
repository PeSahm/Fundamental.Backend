using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// Represents year/period metadata for interpretativeReportSummaryPage5 sections.
/// </summary>
public class YearDataDto
{
    [JsonProperty("columnId")]
    public string? ColumnId { get; set; }

    [JsonProperty("caption")]
    public string? Caption { get; set; }

    [JsonProperty("periodEndToDate")]
    public string? PeriodEndToDate { get; set; }

    [JsonProperty("yearEndToDate")]
    public string? YearEndToDate { get; set; }

    [JsonProperty("period")]
    public int Period { get; set; }

    [JsonProperty("isAudited")]
    public bool? IsAudited { get; set; }

    public DateOnly? YearEndToDateGeorgian => YearEndToDate?.ToGregorianDateOnly();
    public DateOnly? PeriodEndToDateGeorgian => PeriodEndToDate?.ToGregorianDateOnly();
    public int? FiscalYear => YearEndToDateGeorgian?.GetPersianYear(false);
    public int? FiscalMonth => YearEndToDateGeorgian?.GetPersianMonth(false);
    public int? ReportYear => PeriodEndToDateGeorgian?.GetPersianYear(false);
    public int? ReportMonth => PeriodEndToDateGeorgian?.GetPersianMonth(false);
}
