using Coravel.Invocable;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateRegisterCapitalIncreaseData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class UpdateCapitalIncreaseRegistrationNoticeDataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateCapitalIncreaseRegistrationNoticeDataRequest(14), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}