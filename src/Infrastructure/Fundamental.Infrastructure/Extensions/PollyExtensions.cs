using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Fundamental.Infrastructure.Extensions;

public static class PollyExtensions
{
    public static IServiceCollection AddDbRetryPolicy(this IServiceCollection services)
    {
        // If you need typed policies for specific services
        services.AddSingleton<IAsyncPolicy<DbUpdateConcurrencyException>>(
            Policy<DbUpdateConcurrencyException>.Handle<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    ));

        return services;
    }
}