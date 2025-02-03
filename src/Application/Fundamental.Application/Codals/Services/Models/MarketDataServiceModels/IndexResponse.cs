namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class IndexResponse
{
    public List<IndexResponseItem> Result { get; set; } = new();
    public long TotalRecords { get; set; }
}