using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateBalanceSheetData;

public class UpdateBalanceSheetDataCommandHandler(ICodalService codalService, ILogger<UpdateBalanceSheetDataCommandHandler> logger)
    : IRequestHandler<UpdateBalanceSheetDataRequest, Response>
{
    public async Task<Response> Handle(UpdateBalanceSheetDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> balanceSheets =
            await codalService.GetStatements(
                DateTime.Now.AddDays(request.Days),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse balanceSheet in balanceSheets)
        {
            try
            {
                await codalService.ProcessCodal(balanceSheet, LetterPart.BalanceSheet, cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing balanceSheets codal for {@Model}", balanceSheet);
            }
        }

        return Response.Successful();
    }
}