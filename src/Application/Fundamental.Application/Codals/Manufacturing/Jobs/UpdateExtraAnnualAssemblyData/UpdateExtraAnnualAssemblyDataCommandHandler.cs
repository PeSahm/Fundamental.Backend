using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateExtraAnnualAssemblyData;

public sealed class UpdateExtraAnnualAssemblyDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateExtraAnnualAssemblyDataCommandHandler> logger
)
    : IRequestHandler<UpdateExtraAnnualAssemblyDataRequest, Response>
{
    public async Task<Response> Handle(UpdateExtraAnnualAssemblyDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> extraAnnualAssemblies =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.DaysBefore),
                ReportingType.Production,
                LetterType.OrdinaryGeneralMeetingExtraordinaryDecisions,
                cancellationToken);

        foreach (GetStatementResponse extraAnnualAssembly in extraAnnualAssemblies)
        {
            try
            {
                await codalService.ProcessCodal(extraAnnualAssembly, LetterPart.NotSpecified, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing extra annual assembly codal for {@Model}", extraAnnualAssembly);
            }
        }

        return Response.Successful();
    }
}
