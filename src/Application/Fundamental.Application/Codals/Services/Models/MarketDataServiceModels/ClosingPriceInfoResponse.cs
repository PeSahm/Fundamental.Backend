using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class ClosingPriceInfoResponse
{
    [JsonProperty("closingPriceInfo")]
    public ClosingPriceInfo? ClosingPriceInfo { get; set; }
}