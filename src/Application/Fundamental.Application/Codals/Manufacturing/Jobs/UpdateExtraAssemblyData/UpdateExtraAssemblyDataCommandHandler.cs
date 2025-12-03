using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateExtraAssemblyData;

public sealed class UpdateExtraAssemblyDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateExtraAssemblyDataCommandHandler> logger
)
    : IRequestHandler<UpdateExtraAssemblyDataRequest, Response>
{
    public async Task<Response> Handle(UpdateExtraAssemblyDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> extraAssemblies =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.DaysBefore),
                ReportingType.Production,
                LetterType.ExtraordinaryGeneralMeetingDecisions,
                cancellationToken);

        foreach (GetStatementResponse extraAssembly in extraAssemblies)
        {
            try
            {
                await codalService.ProcessCodal(extraAssembly, LetterPart.NotSpecified, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing extra assembly codal for {@Model}", extraAssembly);
            }
        }

        return Response.Successful();
    }
}
