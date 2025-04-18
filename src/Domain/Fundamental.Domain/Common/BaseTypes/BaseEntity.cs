using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Common.BaseTypes;

public abstract class BaseEntity<TId> : IHaveEvent
{
    private readonly List<IAggregationEvent> _domainEvents = new();

    private DateTime _createdAt;

    private DateTime? _updatedAt;

    public TId Id { get; protected set; }

    public DateTime CreatedAt
    {
        get
        {
            return _createdAt;
        }
        protected set
        {
            _createdAt = value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
        }
    }

    public DateTime? UpdatedAt
    {
        get
        {
            return _updatedAt;
        }
        protected set
        {
            _updatedAt = value?.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : value?.ToUniversalTime();
        }
    }

    /// <summary>
    ///     A read-only list of domain events for this entity.
    /// </summary>
    public IReadOnlyCollection<IAggregationEvent> AggregationEvents => _domainEvents.AsReadOnly();

    /// <summary>
    ///     Clears all domain events for this entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IEvent @event, string name, string? callback = null)
    {
        _domainEvents.Add(new AggregationEvent(@event, name, callback));
    }
}