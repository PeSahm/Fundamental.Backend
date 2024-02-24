using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateBalanceSheetData;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateIncomeStatementData;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateMonthlyActivityData;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateNonOperationIncomeAndExpenseData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class CodalHostedService(ILogger<CodalHostedService> logger, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
    : BackgroundService
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
            logger.LogInformation("Codal Manufacturing job is starting");
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new UpdateNonOperationIncomeAndExpensesDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateIncomeStatementDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateBalanceSheetDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateMonthlyActivityDataRequest(-7), stoppingToken);
            logger.LogInformation("Codal Manufacturing job is starting");
        }
    }
}