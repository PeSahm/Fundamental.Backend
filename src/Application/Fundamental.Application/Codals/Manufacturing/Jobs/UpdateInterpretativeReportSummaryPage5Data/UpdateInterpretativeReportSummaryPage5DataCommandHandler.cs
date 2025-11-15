using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateInterpretativeReportSummaryPage5Data;

public sealed class UpdateInterpretativeReportSummaryPage5DataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateInterpretativeReportSummaryPage5DataCommandHandler> logger
)
    : IRequestHandler<UpdateInterpretativeReportSummaryPage5DataRequest, Response>
{
    public async Task<Response> Handle(UpdateInterpretativeReportSummaryPage5DataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> statements =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.DaysBefore),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse statement in statements)
        {
            try
            {
                await codalService.ProcessCodal(statement, LetterPart.InterpretativeReportSummaryPage5, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing interpretative report summary page 5 codal for {@Model}", statement);
            }
        }

        return Response.Successful();
    }
}
