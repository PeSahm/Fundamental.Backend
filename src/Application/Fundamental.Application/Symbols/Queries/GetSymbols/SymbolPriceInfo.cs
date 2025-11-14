namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public sealed class SymbolPriceInfo
{
    public static SymbolPriceInfo Empty => new(0, 0, 0);
    public SymbolPriceInfo(decimal lastPrice, decimal closePrice, decimal yesterdayPrice)
    {
        LastPrice = lastPrice;
        ClosePrice = closePrice;
        YesterdayPrice = yesterdayPrice;
    }

    public decimal LastPrice { get; }
    public decimal ClosePrice { get; }
    public decimal YesterdayPrice { get; }

    public decimal LastPriceChangePercent =>
        YesterdayPrice == 0 ? 0 : Math.Round((LastPrice - YesterdayPrice) / YesterdayPrice * 100, 2);

    public decimal ClosePriceChangePercent =>
        YesterdayPrice == 0 ? 0 : Math.Round((ClosePrice - YesterdayPrice) / YesterdayPrice * 100, 2);
}