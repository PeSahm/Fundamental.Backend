namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class ClosingPriceInfo
{
    public InstrumentState InstrumentState { get; set; }
    public object Instrument { get; set; }
    public int LastHEven { get; set; }
    public long FinalLastDate { get; set; }
    public decimal Nvt { get; set; }
    public int Mop { get; set; }
    public decimal PRedTran { get; set; }
    public object ThirtyDayClosingHistory { get; set; }
    public decimal PriceChange { get; set; }
    public decimal PriceMin { get; set; }
    public decimal PriceMax { get; set; }
    public decimal PriceYesterday { get; set; }
    public decimal PriceFirst { get; set; }
    public bool Last { get; set; }
    public int Id { get; set; }
    public string InsCode { get; set; }
    public long DEven { get; set; }
    public int HEven { get; set; }
    public decimal PClosing { get; set; }
    public bool IClose { get; set; }
    public bool YClose { get; set; }
    public decimal PDrCotVal { get; set; }
    public decimal ZTotTran { get; set; }
    public decimal QTotTran5J { get; set; }
    public decimal QTotCap { get; set; }
}