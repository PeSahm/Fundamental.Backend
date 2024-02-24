using Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;
using Fundamental.Application.Prices.Jobs.UpdateClosePrices;
using Fundamental.Application.Symbols.Jobs.UpdateSymbolData;
using Fundamental.Application.Symbols.Jobs.UpdateTseTmcShareHoldersData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fundamental.Infrastructure.HostedServices.Codals;

public class CommonCodalDataHostedService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
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
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new UpdateClosePricesDataCommand(365), stoppingToken);
            await mediator.Send(new UpdateSymbolDataCommand(), stoppingToken);
            await mediator.Send(new UpdateTseTmcShareHoldersDataRequest(), stoppingToken);
            await mediator.Send(new UpdateCodalPublisherDataRequest(), stoppingToken);
        }
    }
}