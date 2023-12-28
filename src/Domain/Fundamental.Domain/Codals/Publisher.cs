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

    public required string CodalId { get; init; }
    public Symbol Symbol { get; private set; }
    public Symbol? ParentSymbol { get; init; }
    public string? Isic { get; init; }
    public ReportingType ReportingType { get; init; }
    public CompanyType CompanyType { get; init; }
    public string? ExecutiveManager { get; init; }
    public string? Address { get; init; }
    public string? TelNo { get; init; }
    public string? FaxNo { get; init; }
    public string? ActivitySubject { get; init; }
    public string? OfficeAddress { get; init; }
    public string? ShareOfficeAddress { get; init; }
    public string? Website { get; init; }
    public string? Email { get; init; }
    public PublisherState State { get; init; }
    public string? Inspector { get; init; }
    public string? FinancialManager { get; init; }
    public string? FactoryTel { get; init; }
    public string? FactoryFax { get; init; }
    public string? OfficeTel { get; init; }
    public string? OfficeFax { get; init; }
    public string? ShareOfficeTel { get; init; }
    public string? ShareOfficeFax { get; init; }
    public string? NationalCode { get; init; }
    public string? FinancialYear { get; init; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;
    public Money ListedCapital { get; init; } = Money.BaseCurrency(0);
    public string? AuditorName { get; init; }
    public EnableSubCompany IsEnableSubCompany { get; init; }
    public bool IsEnabled { get; init; }
    public PublisherFundType FundType { get; init; }
    public PublisherSubCompanyType SubCompanyType { get; init; }
    public bool IsSupplied { get; init; }
    public PublisherMarketType MarketType { get; init; }
    public Money UnauthorizedCapital { get; init; } = Money.BaseCurrency(0);

    public void Update(Symbol symbol)
    {
        Symbol = symbol;
    }
}