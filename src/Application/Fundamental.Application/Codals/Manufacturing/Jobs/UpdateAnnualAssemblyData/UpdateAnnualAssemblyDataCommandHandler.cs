using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateAnnualAssemblyData;

public sealed class UpdateAnnualAssemblyDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateAnnualAssemblyDataCommandHandler> logger
)
    : IRequestHandler<UpdateAnnualAssemblyDataRequest, Response>
{
    public async Task<Response> Handle(UpdateAnnualAssemblyDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> annualAssemblies =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.DaysBefore),
                ReportingType.Production,
                LetterType.AnnualGeneralMeetingDecisions,
                cancellationToken);

        foreach (GetStatementResponse annualAssembly in annualAssemblies)
        {
            try
            {
                await codalService.ProcessCodal(annualAssembly, LetterPart.NotSpecified, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing annual assembly codal for {@Model}", annualAssembly);
            }
        }

        return Response.Successful();
    }
}
