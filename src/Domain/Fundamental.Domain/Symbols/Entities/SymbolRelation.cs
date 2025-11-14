using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Symbols.Entities;

public class SymbolRelation : BaseEntity<Guid>
{
    public SymbolRelation(Guid id, Symbol parent, Symbol child, decimal ratio, DateTime createdAt)
    {
        Id = id;
        Parent = parent;
        Child = child;
        Ratio = ratio;
        CreatedAt = createdAt;
    }

    protected SymbolRelation()
    {
    }

    public Symbol Parent { get; set; }
    public decimal Ratio { get; set; }
    public Symbol Child { get; set; }
}