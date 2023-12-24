using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;

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
                DateTime.Now.AddDays(request.Days),
                ReportingType.Production,
                LetterType.MonthlyActivity,
                cancellationToken);

        foreach (GetStatementResponse monthlyActivity in monthlyActivities)
        {
            try
            {
                await codalService.ProcessCodal(monthlyActivity, LetterPart.NotSpecified, cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing monthlyActivity codal for {@Model}", monthlyActivity);
            }
        }

        return Response.Successful();
    }
}