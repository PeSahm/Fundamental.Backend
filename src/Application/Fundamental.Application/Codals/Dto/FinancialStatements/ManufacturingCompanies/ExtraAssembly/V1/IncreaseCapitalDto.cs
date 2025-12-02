using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for capital increase (افزایش سرمایه).
/// </summary>
public class IncreaseCapitalDto
{
    [JsonProperty("cashIncoming")]
    public int? CashIncoming { get; set; }

    [JsonProperty("retaindedEarning")]
    public int? RetainedEarning { get; set; }

    [JsonProperty("reserves")]
    public int? Reserves { get; set; }

    [JsonProperty("revaluationSurplus")]
    public int? RevaluationSurplus { get; set; }

    [JsonProperty("sarfSaham")]
    public int? SarfSaham { get; set; }

    [JsonProperty("isAccept")]
    public bool? IsAccept { get; set; }

    [JsonProperty("capitalIncreaseValue")]
    public int? CapitalIncreaseValue { get; set; }

    [JsonProperty("increasePercent")]
    public decimal? IncreasePercent { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("cashForceclosurePriority")]
    public decimal? CashForceclosurePriority { get; set; }

    [JsonProperty("cashForceclosurePriorityStockPrice")]
    public decimal? CashForceclosurePriorityStockPrice { get; set; }

    [JsonProperty("cashForceclosurePriorityStockDesc")]
    public string? CashForceclosurePriorityStockDesc { get; set; }

    [JsonProperty("cashForceclosurePriorityAvalableStockCount")]
    public int? CashForceclosurePriorityAvailableStockCount { get; set; }

    [JsonProperty("cashForceclosurePriorityPrizeStockCount")]
    public int? CashForceclosurePriorityPrizeStockCount { get; set; }
}
