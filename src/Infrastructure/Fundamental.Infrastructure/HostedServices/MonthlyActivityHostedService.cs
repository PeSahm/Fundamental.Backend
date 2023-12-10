using Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.HostedServices;

public class MonthlyActivityHostedService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MonthlyActivityHostedService(ILogger<MonthlyActivityHostedService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(10));

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("MonthlyActivityHostedService is starting");
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new UpdateMonthlyActivityDataRequest(-7), stoppingToken);
            _logger.LogInformation("MonthlyActivityHostedService is stopping");
        }
    }
}