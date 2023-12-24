using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codal.Commands.UpdateBalanceSheetData;

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