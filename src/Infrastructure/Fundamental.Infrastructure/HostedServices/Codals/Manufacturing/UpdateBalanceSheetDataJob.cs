using Coravel.Invocable;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateBalanceSheetData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class UpdateBalanceSheetDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateBalanceSheetDataRequest(14), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}