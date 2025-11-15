using Ardalis.Specification;
using Fundamental.Application.Symbols.Models;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SimpleIndexSpec : Specification<Index, SimpleIndex>
{
    public SimpleIndexSpec()
    {
        Query.Select(x => new SimpleIndex
        {
            Isin = x.Symbol.Isin,
            Date = x.Date
        });
    }
}