using System.Text;
using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;

public sealed class GetBalanceSheetResultDto
{
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public ulong TraceNo { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }

    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ushort YearEndMonth { get; init; }

    public ushort ReportMonth { get; init; }

    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsAudited { get; init; }

    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public string IsAuditedDescription => IsAudited ? "حسابرسی شده" : "حسابرسی نشده";

    public string Title => new StringBuilder("گزارش صورت وضعیت مالی نماد ")
        .Append(Symbol)
        .Append(" دوره ")
        .Append(ReportMonth)
        .Append(" ماهه ")
        .Append(" منتهی به ")
        .Append($"{FiscalYear}/{YearEndMonth}/{GetLastDayOfFiscalYear()}")
        .Append(' ')
        .Append(IsAuditedDescription)
        .ToString();

    private int GetLastDayOfFiscalYear()
    {
        int lastDay = $"{FiscalYear}/{YearEndMonth}/1".ToGregorianDateOnly().GetPersianMonthStartAndEndDates()!.LastDayNumber;
        return lastDay;
    }
}