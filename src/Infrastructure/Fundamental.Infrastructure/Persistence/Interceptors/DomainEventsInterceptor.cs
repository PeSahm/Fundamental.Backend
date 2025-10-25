using DotNetCore.CAP;
using DotNetCore.CAP.Messages;
using Fundamental.BuildingBlock.Events;
using Fundamental.Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Persistence.Interceptors;

public class DomainEventsInterceptor(IServiceScopeFactory serviceProvider) : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default
    )
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ICapPublisher capPublisher = scope.ServiceProvider.GetRequiredService<ICapPublisher>();

        // Check if the current DbContext is available
        DbContext? context = eventData.Context;

        if (context is null)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        // Retrieve all entities that inherit from BaseEntity
        // and have domain events to publish.
        List<IHaveEvent> entitiesWithEvents = context.ChangeTracker
            .Entries<IHaveEvent>()
            .Select(e => e.Entity)
            .Where(e => e.AggregationEvents.Any())
            .ToList();

        // Publish each domain event, then clear the events from the entity
        Dictionary<string, string?> headers = new(1);

        foreach (IHaveEvent entity in entitiesWithEvents)
        {
            foreach (IAggregationEvent aggregationEvent in entity.AggregationEvents)
            {
                // Publish via CAP
                // You could add logic here for the CallbackName
                // e.g., pass it as a header or some custom usage.
                if (aggregationEvent.CallbackName is not null)
                {
                    headers.Add(Headers.CallbackName, aggregationEvent.CallbackName);
                }

                capPublisher.Publish(aggregationEvent.Name, aggregationEvent.Data, headers);
            }

            entity.ClearDomainEvents();
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}