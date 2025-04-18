using Coravel.Invocable;
using Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.CommonJobs;

public class UpdateCodalPublisherDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateCodalPublisherDataRequest(), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}