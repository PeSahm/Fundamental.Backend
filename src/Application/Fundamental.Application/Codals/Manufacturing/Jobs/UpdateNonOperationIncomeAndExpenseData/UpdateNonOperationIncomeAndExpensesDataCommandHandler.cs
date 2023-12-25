using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateNonOperationIncomeAndExpenseData;

public class UpdateNonOperationIncomeAndExpensesDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateNonOperationIncomeAndExpensesDataCommandHandler> logger
) : IRequestHandler<UpdateNonOperationIncomeAndExpensesDataRequest, Response>
{
    public async Task<Response> Handle(UpdateNonOperationIncomeAndExpensesDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> statements =
            await codalService.GetStatements(
                DateTime.Now.AddDays(request.Days),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse nonOperationIncomeAndExpenses in statements)
        {
            try
            {
                await codalService.ProcessCodal(nonOperationIncomeAndExpenses, LetterPart.NonOperationIncomeAndExpenses, cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing NonOperationIncomeAndExpenses codal for {@Model}", nonOperationIncomeAndExpenses);
            }
        }

        return Response.Successful();
    }
}