using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class ShareHoldersResponse
{
    public required string Isin { get; init; }

    public required string ShareHolderName { get; init; }

    public decimal NumberOfShares { get; init; }

    [JsonPropertyName("PerOfShares")]
    [JsonProperty("PerOfShares")]
    public decimal Percent { get; init; }

    public decimal ChangeAmount { get; init; }
}