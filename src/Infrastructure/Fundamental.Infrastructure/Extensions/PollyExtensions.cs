using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Fundamental.Infrastructure.Extensions;

public static class PollyExtensions
{
    public static IServiceCollection AddDbRetryPolicy(this IServiceCollection services)
    {
        services.AddResiliencePipeline(
            "DbUpdateConcurrencyException",
            (builder, context) =>
            {
                builder.AddRetry(new RetryStrategyOptions
                {
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(1),
                    Name = "DbUpdateException",
                    MaxDelay = TimeSpan.FromSeconds(10),
                    ShouldHandle = new PredicateBuilder()
                        .Handle<DbUpdateConcurrencyException>()
                });
                builder.AddTimeout(TimeSpan.FromSeconds(1));
            });

        return services;
    }
}