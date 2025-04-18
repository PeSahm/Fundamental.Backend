using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Prices.Events;

public record ClosePriceUpdated(string Isin, decimal ClosePrice, DateOnly Date) : IEvent;