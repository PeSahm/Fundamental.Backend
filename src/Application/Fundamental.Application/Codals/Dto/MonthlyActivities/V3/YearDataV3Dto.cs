using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class YearDataV3Dto
{
    [JsonProperty("columnId")]
    [JsonRequired]
    public string ColumnId { get; set; }

    [JsonProperty("caption")]
    [JsonRequired]
    public string Caption { get; set; }

    [JsonProperty("periodEndToDate")]
    [JsonRequired]
    public string PeriodEndToDate { get; set; }

    [JsonProperty("yearEndToDate")]
    [JsonRequired]
    public string YearEndToDate { get; set; }

    [JsonProperty("period")]
    [JsonRequired]
    public int Period { get; set; }

    [JsonProperty("isAudited")]
    public bool? IsAudited { get; set; }

    public int FiscalYear => YearEndToDate.ToPersianDateTime()?.Year ??
                             throw new InvalidOperationException("Invalid YearEndToDate format");

    public int YearEndMonth => YearEndToDate.ToPersianDateTime()?.Month ??
                              throw new InvalidOperationException("Invalid YearEndToDate format");

    public int ReportMonth => PeriodEndToDate.ToPersianDateTime()?.Month ??
                              throw new InvalidOperationException("Invalid PeriodEndToDate format");
}