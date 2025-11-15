using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateRegisterCapitalIncreaseData;

public class UpdateRegisterCapitalIncreaseDataRequestHandler(
    ICodalService codalService,
    ILogger<UpdateRegisterCapitalIncreaseDataRequestHandler> logger
)
    : IRequestHandler<UpdateCapitalIncreaseRegistrationNoticeDataRequest>
{
    public async Task Handle(UpdateCapitalIncreaseRegistrationNoticeDataRequest request, CancellationToken cancellationToken)
    {
        foreach (ReportingType reportingType in Enum.GetValues<ReportingType>())
        {
            List<GetStatementResponse> statements = await codalService.GetStatements(
                DateTime.UtcNow.AddDays(request.DaysBeforeToday),
                reportingType,
                LetterType.CapitalIncreaseRegistrationNotice,
                cancellationToken);

            foreach (GetStatementResponse statement in statements.OrderBy(x => x.PublishDateMiladi))
            {
                try
                {
                    await codalService.ProcessCodal(statement, ReportingType.UnKnown, LetterPart.NotSpecified, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    // Task cancellation is an expected scenario
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    logger.LogError(e, "Error processing register capital increase codal for {@Model}", statement);
                }
            }
        }
    }
}