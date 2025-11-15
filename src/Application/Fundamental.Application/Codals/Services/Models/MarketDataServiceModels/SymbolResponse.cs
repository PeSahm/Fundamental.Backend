namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class SymbolResponse
{
    public string Isin { get; set; }

    public string TseInsCode { get; set; }

    public string EnName { get; set; }

    public string SymbolEnName { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string CompanyEnCode { get; set; }

    public string CompanyPersianName { get; set; }

    public string? CompanyIsin { get; set; }

    public ulong MarketCap { get; set; }

    public string SectorCode { get; set; }

    public string SubSectorCode { get; set; }

    public SymbolCustomExtension SymbolCustomExtension { get; set; }
}