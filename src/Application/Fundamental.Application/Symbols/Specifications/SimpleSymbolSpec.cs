using Ardalis.Specification;
using Fundamental.Application.Symbols.Models;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SimpleSymbolSpec : Specification<Symbol, SimpleSymbol>
{
    public SimpleSymbolSpec()
    {
        Query.Select(x => new SimpleSymbol(x.Isin, x.TseInsCode));
    }
}