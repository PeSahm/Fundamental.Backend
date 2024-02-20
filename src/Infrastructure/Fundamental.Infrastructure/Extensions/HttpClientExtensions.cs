using Fundamental.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        AddMdpHttpClient(services, configuration);

        return services;
    }

    private static void AddMdpHttpClient(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(
            HttpClients.MDP,
            client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("Mdp:url")!);
                client.Timeout = Timeout.InfiniteTimeSpan;
            });
    }
}