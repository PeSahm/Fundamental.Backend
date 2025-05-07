using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateIncomeStatementData;

public sealed class UpdateIncomeStatementDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateIncomeStatementDataCommandHandler> logger
)
    : IRequestHandler<UpdateIncomeStatementDataRequest, Response>
{
    public async Task<Response> Handle(UpdateIncomeStatementDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> statements =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.Days),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse incomeStatement in statements)
        {
            try
            {
                await codalService.ProcessCodal(incomeStatement, LetterPart.IncomeStatement, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing IncomeStatement codal for {@Model}", incomeStatement);
            }
        }

        return Response.Successful();
    }
}