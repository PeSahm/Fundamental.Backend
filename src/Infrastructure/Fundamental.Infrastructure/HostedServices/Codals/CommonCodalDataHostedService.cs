using Fundamental.Application.Codals.Manufacturing.Commands.UpdateMonthlyActivity;
using Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsClosePrice;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateMonthlyActivityData;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateRegisterCapitalIncreaseData;
using Fundamental.Application.Prices.Jobs.UpdateClosePrices;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fundamental.Infrastructure.HostedServices.Codals;

public class CommonCodalDataHostedService(IServiceScopeFactory factory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }
}