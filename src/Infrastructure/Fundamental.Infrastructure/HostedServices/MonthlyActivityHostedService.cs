using Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.HostedServices;

public class MonthlyActivityHostedService(ILogger<MonthlyActivityHostedService> logger, IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(10));

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("MonthlyActivityHostedService is starting");
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new UpdateMonthlyActivityDataRequest(-7), stoppingToken);
            logger.LogInformation("MonthlyActivityHostedService is stopping");
        }
    }
}