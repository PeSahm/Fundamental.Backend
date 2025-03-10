using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Common.BaseTypes;

internal record AggregationEvent(IEvent Data, string Name, string? CallbackName) : IAggregationEvent;