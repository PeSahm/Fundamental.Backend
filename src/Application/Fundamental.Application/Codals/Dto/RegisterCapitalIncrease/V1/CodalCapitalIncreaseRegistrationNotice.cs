using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.RegisterCapitalIncrease.V1;

public class CodalCapitalIncreaseRegistrationNotice
{
    [JsonProperty("cashIncoming")]
    public decimal CashIncoming { get; set; }

    [JsonProperty("lastCapitalIncreaseSession")]
    public string? LastCapitalIncreaseSession { get; set; }

    [JsonProperty("lastExtraAssembly")]
    public string? LastExtraAssembly { get; set; }

    [JsonProperty("newCapital")]
    public decimal NewCapital { get; set; }

    [JsonProperty("previousCapital")]
    public decimal PreviousCapital { get; set; }

    [JsonProperty("reserves")]
    public decimal Reserves { get; set; }

    [JsonProperty("retaindedEarning")]
    public decimal RetaindedEarning { get; set; }

    [JsonProperty("revaluationSurplus")]
    public decimal RevaluationSurplus { get; set; }

    [JsonProperty("sarfSaham")]
    public decimal SarfSaham { get; set; }

    [JsonProperty("startDate")]
    public string StartDate { get; set; }

    [JsonProperty("primaryMarketTracingNo")]
    public long PrimaryMarketTracingNo { get; set; }

    [JsonProperty("cashForceclosurePriority")]
    public decimal CashForceclosurePriority { get; set; }
}