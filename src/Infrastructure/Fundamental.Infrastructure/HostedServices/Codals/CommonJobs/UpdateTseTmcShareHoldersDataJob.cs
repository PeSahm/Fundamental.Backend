using Coravel.Invocable;
using Fundamental.Application.Symbols.Jobs.UpdateTseTmcShareHoldersData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.CommonJobs;

public class UpdateTseTmcShareHoldersDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateTseTmcShareHoldersDataRequest(), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}