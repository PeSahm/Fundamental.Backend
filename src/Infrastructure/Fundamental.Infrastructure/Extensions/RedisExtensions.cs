using Fundamental.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Fundamental.Infrastructure.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationOptions configurationOptions = GetRedisConfigurationOptions(configuration);

        ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        return services;
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(IConfiguration configuration)
    {
        RedisOptions? redisOptions = configuration.GetSection("Redis").Get<RedisOptions>();

        if (redisOptions == null)
        {
            throw new InvalidOperationException("Redis configuration is missing in appsettings.json");
        }

        ConfigurationOptions configurationOptions = new()
        {
            EndPoints = { redisOptions.ConnectionString },
            User = redisOptions.User,
            Password = redisOptions.Password,
            DefaultDatabase = redisOptions.DefaultDatabase,
            ConnectTimeout = redisOptions.ConnectTimeout,
            ConnectRetry = redisOptions.ConnectRetry,
            KeepAlive = redisOptions.KeepAlive,
            AbortOnConnectFail = redisOptions.AbortOnConnectFail,
            ConfigCheckSeconds = redisOptions.ConfigCheckSeconds
        };
        return configurationOptions;
    }

    public static IServiceCollection AddCustomHybridCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(option =>
        {
            option.ConfigurationOptions = GetRedisConfigurationOptions(configuration);
        });
        services.AddHybridCache(option =>
        {
            option.ReportTagMetrics = true;
            option.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(5),
                LocalCacheExpiration = TimeSpan.FromMinutes(5),
            };
        });
        return services;
    }
}