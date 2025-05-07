using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Symbols.Entities;

public class Sector : BaseEntity<Guid>
{
    public static Sector Empty => new Sector() { Name = "Empty" };

    public Sector(
        Guid id,
        string name,
        DateTime createdAt
    )
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt.ToUniversalTime();
    }

    protected Sector()
    {
    }

    public string Name { get; private set; }
    public List<Symbol> Symbols { get; set; } = new();
}