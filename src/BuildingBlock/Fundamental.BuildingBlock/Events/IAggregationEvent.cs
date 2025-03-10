namespace Fundamental.BuildingBlock.Events;

public interface IAggregationEvent
{
    /// <summary>
    /// The name of the event.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The callback name for the event.
    /// </summary>
    string? CallbackName { get; }

    IEvent Data { get; }
}