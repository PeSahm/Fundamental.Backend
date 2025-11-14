namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public class TradeHistoryResponse
{
    public string Isin { get; set; } = string.Empty;
    public DateTime TradeDate { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal LastPrice { get; set; }
    public decimal OpenPriceAdj { get; set; }
    public decimal HighPriceAdj { get; set; }
    public decimal LowPriceAdj { get; set; }
    public decimal ClosePriceAdj { get; set; }
    public decimal LastPriceAdj { get; set; }
    public decimal TradeVolume { get; set; }
    public decimal TradeValue { get; set; }
    public decimal TradeQuantity { get; set; }
    public decimal ClosingPriceChange { get; set; }
    public decimal ClosingPriceChangePercent { get; set; }
    public decimal PrevClosePrice { get; set; }
    public decimal PrevClosePriceAdj { get; set; }
}