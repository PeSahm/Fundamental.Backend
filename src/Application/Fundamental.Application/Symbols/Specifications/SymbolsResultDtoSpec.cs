using Ardalis.Specification;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SymbolsResultDtoSpec : Specification<Symbol, GetSymbolsResultDto>
{
    public SymbolsResultDtoSpec()
    {
        Query.Select(x => new GetSymbolsResultDto(x.Isin, x.TseInsCode, x.Title, x.Name, x.MarketCap));
    }

    public SymbolsResultDtoSpec NoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}