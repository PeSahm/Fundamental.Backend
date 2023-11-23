using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Enums;

namespace Fundamental.Domain.Symbols.Entities;

public class Symbol : BaseEntity<Guid>
{
    public Symbol(
        Guid id,
        string isin,
        string tseInsCode,
        string enName,
        string symbolEnName,
        string title,
        string name,
        string companyEnCode,
        string companyPersianName,
        string companyIsin,
        ulong marketCap,
        string sectorCode,
        string subSectorCode,
        DateTime createdAt
        )
    {
        Id = id;
        Isin = isin;
        TseInsCode = tseInsCode;
        EnName = enName;
        SymbolEnName = symbolEnName;
        Title = title;
        Name = name;
        CompanyEnCode = companyEnCode;
        CompanyPersianName = companyPersianName;
        CompanyIsin = companyIsin;
        MarketCap = marketCap;
        SectorCode = sectorCode;
        SubSectorCode = subSectorCode;
        CreatedAt = createdAt;
    }

    protected Symbol()
    {
    }

    public string Isin { get; private set; }
    public string TseInsCode { get; private set; }
    public string EnName { get; private set; }
    public string SymbolEnName { get; private set; }
    public string Title { get; private set; }
    public string Name { get; private set; }
    public string CompanyEnCode { get; private set; }
    public string CompanyPersianName { get; private set; }
    public string CompanyIsin { get; private set; }
    public ulong MarketCap { get; private set; }
    public string SectorCode { get; private set; }
    public string SubSectorCode { get; private set; }
    public void SetProductType(ProductType productType, DateTime updatedAt)
    {
        ProductType = productType;
        UpdatedAt = updatedAt;
    }

    public ProductType ProductType { get; private set; }
}