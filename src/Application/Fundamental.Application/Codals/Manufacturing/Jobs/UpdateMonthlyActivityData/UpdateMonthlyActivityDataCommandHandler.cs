using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateMonthlyActivityData;

public sealed class UpdateMonthlyActivityDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateMonthlyActivityDataCommandHandler> logger
)
    : IRequestHandler<UpdateMonthlyActivityDataRequest, Response>
{
    public async Task<Response> Handle(UpdateMonthlyActivityDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> monthlyActivities =
            await codalService.GetStatements(
                "1401/08/01".ToGregorianDateTime()!.Value,
                ReportingType.Production,
                LetterType.MonthlyActivity,
                cancellationToken);

        foreach (GetStatementResponse monthlyActivity in monthlyActivities)
        {
            try
            {
                await codalService.ProcessCodal(monthlyActivity, LetterPart.NotSpecified, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing monthlyActivity codal for {@Model}", monthlyActivity);
            }
        }

        return Response.Successful();
    }
}