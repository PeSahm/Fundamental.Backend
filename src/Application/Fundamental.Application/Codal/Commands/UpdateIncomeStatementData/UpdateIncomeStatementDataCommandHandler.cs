using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codal.Commands.UpdateIncomeStatementData;

public class UpdateIncomeStatementDataCommandHandler(ICodalService codalService)
    : IRequestHandler<UpdateIncomeStatementDataRequest, Response>
{
    public async Task<Response> Handle(UpdateIncomeStatementDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> statements =
            await codalService.GetStatements(
                DateTime.Now.AddDays(request.Days),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse balanceSheet in statements)
        {
            try
            {
                await codalService.ProcessCodal(balanceSheet, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return Response.Successful();
    }
}