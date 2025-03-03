using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class CalculationHostedService(
    ILogger<CalculationHostedService> logger,
    IServiceScopeFactory serviceScopeFactory,
    IConfiguration configuration
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!configuration.GetValue<bool>("JobEnabled"))
        {
            await Task.CompletedTask;
            return;
        }

        PeriodicTimer periodicTimer = new(TimeSpan.FromMinutes(10));

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("Codal Manufacturing Calculation job is starting");
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new UpdateFinancialStatementsDataRequest(), stoppingToken);

            logger.LogInformation("Codal Manufacturing job is starting");
        }
    }
}