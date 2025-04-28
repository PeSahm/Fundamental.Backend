namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class InstrumentState
{
    public int Idn { get; set; }
    public int DEven { get; set; }
    public int HEven { get; set; }
    public string InsCode { get; set; }
    public string LVal18AFC { get; set; }
    public string LVal30 { get; set; }
    public string CEtaval { get; set; }
    public int RealHeven { get; set; }
    public int UnderSupervision { get; set; }
    public string CEtavalTitle { get; set; }
}