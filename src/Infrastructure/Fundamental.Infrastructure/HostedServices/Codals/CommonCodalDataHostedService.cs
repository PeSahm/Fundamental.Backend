using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fundamental.Infrastructure.HostedServices.Codals;

public class CommonCodalDataHostedService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }
}