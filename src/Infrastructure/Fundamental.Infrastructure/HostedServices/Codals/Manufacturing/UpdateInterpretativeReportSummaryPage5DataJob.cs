using Coravel.Invocable;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateInterpretativeReportSummaryPage5Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;

public class UpdateInterpretativeReportSummaryPage5DataJob(IServiceScopeFactory serviceScopeFactory) : IInvocable, ICancellableInvocable
{
    public async Task Invoke()
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateInterpretativeReportSummaryPage5DataRequest(7), CancellationToken);
    }

    public CancellationToken CancellationToken { get; set; }
}
