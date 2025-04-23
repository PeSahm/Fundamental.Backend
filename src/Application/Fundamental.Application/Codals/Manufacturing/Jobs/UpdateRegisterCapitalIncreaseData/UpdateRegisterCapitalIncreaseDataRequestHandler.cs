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
        try
        {
            List<GetStatementResponse> statements = await codalService.GetStatements(
                DateTime.UtcNow.AddDays(request.DaysBeforeToday),
                ReportingType.Production,
                LetterType.CapitalIncreaseRegistrationNotice,
                cancellationToken);

            foreach (GetStatementResponse statement in statements)
            {
                try
                {
                    await codalService.ProcessCodal(statement, LetterPart.NotSpecified, cancellationToken);
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
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            //Do not log the exception if it is a cancellation exception
        }
    }
}