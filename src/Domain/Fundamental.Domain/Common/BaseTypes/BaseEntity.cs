using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Common.BaseTypes;

public abstract class BaseEntity<TId> : IHaveEvent
{
    private readonly List<IAggregationEvent> _domainEvents = new();

    /// <summary>
    /// A read-only list of domain events for this entity.
    /// </summary>
    public IReadOnlyCollection<IAggregationEvent> AggregationEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Clears all domain events for this entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IEvent @event, string name, string? callback = null)
    {
        _domainEvents.Add(new AggregationEvent(@event, name, callback));
    }

    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
}