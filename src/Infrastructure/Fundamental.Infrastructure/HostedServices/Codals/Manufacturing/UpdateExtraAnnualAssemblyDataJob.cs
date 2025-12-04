using Coravel.Invocable;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateExtraAnnualAssemblyData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class UpdateExtraAnnualAssemblyDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateExtraAnnualAssemblyDataRequest(7), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}
