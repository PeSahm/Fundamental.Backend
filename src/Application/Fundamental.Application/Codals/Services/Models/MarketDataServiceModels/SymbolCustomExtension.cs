using Fundamental.Domain.Symbols.Enums;

namespace Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

public sealed class SymbolCustomExtension
{
    public ProductType? ProductType { get; set; }

    public ExchangeType? CustomExchangeType { get; set; }

    public EtfType? EtfType { get; set; }
}