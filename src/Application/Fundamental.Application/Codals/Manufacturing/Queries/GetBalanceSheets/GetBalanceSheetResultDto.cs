using Fundamental.Application.Codals.Common;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;

public sealed class GetBalanceSheetResultDto
{
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public ulong TraceNo { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }

    public DateTime PublishDate { get; set; }

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

    public string Title => FinancialStatementTitleGenerator.GenerateTitle(
        "گزارش صورت وضعیت مالی نماد ",
        Symbol,
        ReportMonth,
        YearEndMonth,
        FiscalYear,
        IsAudited);
}