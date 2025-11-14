namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class IndexResponseItem
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Open { get; set; }
    public string Isin { get; set; }

    public string InsCode { get; set; }
}