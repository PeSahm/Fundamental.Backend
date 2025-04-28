using Fundamental.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Fundamental.Infrastructure.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
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

        ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        return services;
    }
}