using Fundamental.BuildingBlock.Events;

namespace Fundamental.Domain.Codals.Manufacturing.Events;

public record CapitalIncreaseRegistrationNoticeCreated(
    string Isin,
    ulong TraceNo,
    string Uri,
    DateOnly StartDate,
    DateOnly LastExtraAssemblyDate,
    decimal NewCapital,
    decimal PreviousCapital
) : IEvent;