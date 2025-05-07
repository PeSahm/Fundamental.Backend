using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateTheStatusOfViableCompanyData;

public sealed class UpdateTheStatusOfViableCompanyDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateTheStatusOfViableCompanyDataCommandHandler> logger
) : IRequestHandler<UpdateTheStatusOfViableCompanyDataReqeust, Response>
{
    public async Task<Response> Handle(UpdateTheStatusOfViableCompanyDataReqeust request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> statements =
            await codalService.GetStatements(
                DateTime.Now.AddDays(-1 * request.Days),
                ReportingType.Production,
                LetterType.InterimStatement,
                cancellationToken);

        foreach (GetStatementResponse theStatusOfViableCompany in statements)
        {
            try
            {
                await codalService.ProcessCodal(theStatusOfViableCompany, LetterPart.TheStatusOfViableCompanies, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Ignore the task cancellation exception
            }
            catch (Exception e)
            {
                logger.LogError(e, " Error processing TheStatusOfViableCompanies codal for {@Model}", theStatusOfViableCompany);
            }
        }

        return Response.Successful();
    }
}