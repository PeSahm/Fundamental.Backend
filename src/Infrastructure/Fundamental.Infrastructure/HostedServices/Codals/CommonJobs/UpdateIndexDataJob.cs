using Coravel.Invocable;
using Fundamental.Application.Symbols.Jobs.UpdateIndexData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.CommonJobs;

public class UpdateIndexDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateIndexDataCommand(90), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}