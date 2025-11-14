using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Symbols.Entities;

public class IndexCompany : BaseEntity<Guid>
{
    public IndexCompany(Guid id, Symbol index, Symbol company, DateTime creeatedAt)
    {
        Id = id;
        Index = index;
        Company = company;
        CreatedAt = creeatedAt;
    }

    private IndexCompany()
    {
    }

    public Symbol Index { get; set; }
    public Symbol Company { get; set; }
}