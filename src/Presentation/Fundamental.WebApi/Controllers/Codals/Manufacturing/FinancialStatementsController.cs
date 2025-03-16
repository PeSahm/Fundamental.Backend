using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/financial-statements")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class FinancialStatementsController(ISender mediator) : ControllerBase
{
    [HttpGet("isin/{isin}")]
    [SwaggerRequestType(typeof(GetFinancialStatementsReqeust))]
    public async Task<Response<GetFinancialStatementsResultDto>> GetBalanceSheetSortList(string isin)
    {
        return await mediator.Send(new GetFinancialStatementsReqeust(isin));
    }
}