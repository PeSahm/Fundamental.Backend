using Fundamental.Application.Codal.Commands.UpdateBalanceSheetData;
using Fundamental.Application.Codal.Commands.UpdateIncomeStatementData;
using Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;
using Fundamental.Application.Codal.Commands.UpdateNonOperationIncomeAndExpenseData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.HostedServices;

public class CodalHostedService(ILogger<CodalHostedService> logger, IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer periodicTimer = new(TimeSpan.FromMinutes(10));

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("MonthlyActivityHostedService is starting");
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new UpdateNonOperationIncomeAndExpensesDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateIncomeStatementDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateBalanceSheetDataRequest(-7), stoppingToken);
            await mediator.Send(new UpdateMonthlyActivityDataRequest(-7), stoppingToken);
            logger.LogInformation("MonthlyActivityHostedService is stopping");
        }
    }
}