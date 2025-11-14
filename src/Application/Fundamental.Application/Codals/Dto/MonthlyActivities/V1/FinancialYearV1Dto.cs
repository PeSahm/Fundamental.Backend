using System.Text.Json.Serialization;
using DNTPersianUtils.Core;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V1;

public class FinancialYearV1Dto
{
    [JsonPropertyName("priodEndToDate")]
    public string PriodEndToDate { get; set; }

    [JsonPropertyName("yearEndToDate")]
    public string YearEndToDate { get; set; }

    public int FiscalYear => YearEndToDate?.ToPersianDateTime()?.Year ??
                             throw new InvalidOperationException("Invalid or missing YearEndToDate format");

    public int YearEndMonth => YearEndToDate?.ToPersianDateTime()?.Month ??
                               throw new InvalidOperationException("Invalid or missing YearEndToDate format");

    public int ReportMonth => PriodEndToDate?.ToPersianDateTime()?.Month ??
                              throw new InvalidOperationException("Invalid or missing Period EndToDate format");
}