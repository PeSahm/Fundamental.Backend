using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Fundamental.WebApi.Extensions;

/// <summary>
/// Health check configuration for Kubernetes probes (startup, liveness, readiness).
/// </summary>
public static class HealthCheckExtensions
{
    private const string StartupTag = "startup";
    private const string LivenessTag = "liveness";
    private const string ReadinessTag = "readiness";

    /// <summary>
    /// Adds health checks for PostgreSQL and Redis with appropriate tags for Kubernetes probes.
    /// </summary>
    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? postgresConnectionString = configuration.GetConnectionString("postgres");
        string? redisConnectionString = configuration.GetConnectionString("redis");

        IHealthChecksBuilder healthChecksBuilder = services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: [LivenessTag]);

        // PostgreSQL health check (critical for readiness)
        if (!string.IsNullOrEmpty(postgresConnectionString))
        {
            healthChecksBuilder.AddNpgSql(
                connectionString: postgresConnectionString,
                name: "postgresql",
                failureStatus: HealthStatus.Unhealthy,
                tags: [StartupTag, ReadinessTag],
                timeout: TimeSpan.FromSeconds(5));
        }

        // Redis health check (optional, degrades gracefully)
        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            healthChecksBuilder.AddRedis(
                redisConnectionString: redisConnectionString,
                name: "redis",
                failureStatus: HealthStatus.Degraded,
                tags: [StartupTag, ReadinessTag],
                timeout: TimeSpan.FromSeconds(3));
        }

        return services;
    }

    /// <summary>
    /// Maps health check endpoints for Kubernetes probes.
    /// </summary>
    public static IEndpointRouteBuilder MapCustomHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        // Startup probe: Only succeeds once the app is initialized and dependencies are available
        // Used by Kubernetes to know when the container has started
        endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(StartupTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        // Liveness probe: Simple self-check, indicates the process is running
        // If this fails, Kubernetes will restart the container
        endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(LivenessTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        // Readiness probe: Checks if dependencies (DB, cache) are available
        // If this fails, Kubernetes removes the pod from service endpoints
        endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(ReadinessTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        // Combined health check for general status
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        return endpoints;
    }
}
