using Fundamental.BuildingBlock;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Enums;

namespace Fundamental.Domain.Symbols.Entities;

public class Symbol : BaseEntity<Guid>
{
    public static Symbol Empty => new Symbol() { Name = "Empty" };

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
        string? companyIsin,
        ulong marketCap,
        string sectorCode,
        string subSectorCode,
        ProductType productType,
        ExchangeType? exchangeType,
        EtfType? etfType,
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
        ProductType = productType;
        ProductType2 = productType;
        ExchangeType = exchangeType ?? ExchangeType.None;
        EtfType = etfType;

        CreatedAt = createdAt;

        if (productType is ProductType.Index)
        {
            Name = Title = Isin.CorrectIndicesName("Index");
        }
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
    public string? CompanyIsin { get; private set; }
    public ulong MarketCap { get; private set; }
    public string SectorCode { get; private set; }
    public string SubSectorCode { get; private set; }

    public bool IsUnOfficial { get; private set; }

    public ProductType ProductType { get; private set; } = ProductType.Equity;

    public ProductType ProductType2 { get; private set; } = ProductType.Equity;

    public ExchangeType ExchangeType { get; private set; } = ExchangeType.None;

    public EtfType? EtfType { get; private set; }

    public Publisher? Publisher { get; private set; }

    public Sector? Sector { get; set; }

    public ICollection<SymbolRelation> InvestmentSymbols { get; private set; } = new List<SymbolRelation>();

    public ICollection<SymbolRelation> InvestorSymbols { get; private set; } = new List<SymbolRelation>();

    public static Symbol CreateByParentSymbol(Symbol parent, string name, string title, DateTime updatedAt)
    {
        string postfix = name.Replace(parent.Name, string.Empty).Trim();
        Symbol newSymbol = new(
            Guid.NewGuid(),
            $"{parent.Isin}{postfix}".Trim(),
            parent.TseInsCode,
            parent.EnName,
            parent.SymbolEnName,
            title,
            name,
            parent.CompanyEnCode,
            parent.CompanyPersianName,
            parent.CompanyIsin,
            parent.MarketCap,
            parent.SectorCode,
            parent.SubSectorCode,
            parent.ProductType,
            parent.ExchangeType,
            parent.EtfType,
            updatedAt
        )
        {
            IsUnOfficial = true,
        };
        return newSymbol;
    }

    public void AddInvestmentSymbol(SymbolRelation symbolRelation)
    {
        InvestmentSymbols.Add(symbolRelation);
    }

    public void Update(
        string tseInsCode,
        string enName,
        string symbolEnName,
        string title,
        string name,
        string companyEnCode,
        string companyPersianName,
        string? companyIsin,
        ulong marketCap,
        string sectorCode,
        string subSectorCode,
        ProductType productType,
        ExchangeType? exchangeType,
        EtfType? etfType,
        DateTime updatedAt
    )
    {
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
        ProductType = productType;
        ProductType2 = productType;
        ExchangeType = exchangeType ?? ExchangeType.None;
        EtfType = etfType;
        UpdatedAt = updatedAt;

        if (productType is ProductType.Index)
        {
            Name = Title = Isin.CorrectIndicesName(name);
        }
    }
}