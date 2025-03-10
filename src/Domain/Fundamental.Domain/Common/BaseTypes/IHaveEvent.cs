using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Common.BaseTypes;

public interface IHaveEvent
{
    IReadOnlyCollection<IAggregationEvent> AggregationEvents { get; }

    void ClearDomainEvents();
}