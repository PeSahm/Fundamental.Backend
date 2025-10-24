using Coravel.Invocable;
using Fundamental.Application.Prices.Jobs.UpdateClosePrices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.CommonJobs;

public class UpdateClosePricesDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateClosePricesDataCommand(70), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}