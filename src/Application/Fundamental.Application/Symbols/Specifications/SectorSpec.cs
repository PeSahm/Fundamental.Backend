using Ardalis.Specification;
using Fundamental.Application.Symbols.Queries.GetSectors;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public sealed class SectorSpec : Specification<Sector, GetSectorsResultDto>
{
    public SectorSpec()
    {
        Query.Select(s => new GetSectorsResultDto
        {
            Id = s.Id,
            Name = s.Name,
            SymbolCount = s.Symbols.Count
        });
    }

    public SectorSpec Filter(string filter)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            Query.Where(s => s.Name.Contains(filter));
        }

        return this;
    }
}