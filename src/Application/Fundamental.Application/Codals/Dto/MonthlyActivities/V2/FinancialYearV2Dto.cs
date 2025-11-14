using System.Text.Json.Serialization;
using DNTPersianUtils.Core;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class FinancialYearV2Dto
{
    [JsonPropertyName("prevPeriodEndToDate")]
    public string PrevPeriodEndToDate { get; set; }

    [JsonPropertyName("priodEndToDate")]
    public string PriodEndToDate { get; set; }

    public int FiscalYear => PriodEndToDate.ToPersianDateTime()?.Year ??
                              throw new InvalidOperationException("Invalid Period EndToDate format");

    public int YearEndMonth => 12; // Not provided in V2 payloads; assume standard year-end

    public int ReportMonth => PriodEndToDate.ToPersianDateTime()?.Month ??
                              throw new InvalidOperationException("Invalid Period EndToDate format");
}