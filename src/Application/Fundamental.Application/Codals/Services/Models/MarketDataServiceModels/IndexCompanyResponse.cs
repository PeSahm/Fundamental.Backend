namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class IndexCompanyResponse
{
    public List<IndexCompanyItem> IndexCompany { get; set; } = new();
}

#pragma warning disable SA1402
public sealed class IndexCompanyItem
#pragma warning restore SA1402
{
    public Instrument Instrument { get; set; }
}

#pragma warning disable SA1402
public sealed class Instrument
#pragma warning restore SA1402
{
    public string InsCode { get; set; }
    public string LVal30 { get; set; }
    public string LVal18Afc { get; set; }
}
