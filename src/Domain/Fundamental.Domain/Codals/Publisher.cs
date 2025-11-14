using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals;

public sealed class Publisher : BaseEntity<Guid>
{
    public Publisher(Guid id, Symbol symbol, DateTime createdAt)
    {
        Id = id;
        Symbol = symbol;
        CreatedAt = createdAt;
    }

    private Publisher()
    {
    }

    public required string CodalId { get; set; }
    public Symbol Symbol { get; private set; }
    public Symbol? ParentSymbol { get; set; }
    public string? Isic { get; set; }
    public ReportingType ReportingType { get; set; }
    public CompanyType CompanyType { get; set; }
    public string? ExecutiveManager { get; set; }
    public string? Address { get; set; }
    public string? TelNo { get; set; }
    public string? FaxNo { get; set; }
    public string? ActivitySubject { get; set; }
    public string? OfficeAddress { get; set; }
    public string? ShareOfficeAddress { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public PublisherState State { get; set; }
    public string? Inspector { get; set; }
    public string? FinancialManager { get; set; }
    public string? FactoryTel { get; set; }
    public string? FactoryFax { get; set; }
    public string? OfficeTel { get; set; }
    public string? OfficeFax { get; set; }
    public string? ShareOfficeTel { get; set; }
    public string? ShareOfficeFax { get; set; }
    public string? NationalCode { get; set; }
    public string? FinancialYear { get; set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;
    public CodalMoney ListedCapital { get; set; } = CodalMoney.BaseCurrency(0);
    public string? AuditorName { get; set; }
    public EnableSubCompany IsEnableSubCompany { get; set; }
    public bool IsEnabled { get; set; }
    public PublisherFundType FundType { get; set; }
    public PublisherSubCompanyType SubCompanyType { get; set; }
    public bool IsSupplied { get; set; }
    public PublisherMarketType MarketType { get; set; }
    public CodalMoney UnauthorizedCapital { get; set; } = CodalMoney.BaseCurrency(0);

    public void Update(Symbol symbol)
    {
        Symbol = symbol;
    }

    public void UpdateLog()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}