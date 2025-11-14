using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementList;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.Dto;
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
    [SwaggerRequestType(typeof(GetFinancialStatementsRequest))]
    public async Task<Response<GetFinancialStatementsResultDto>> GetFinancialStatementsRequest(string isin)
    {
        return await mediator.Send(new GetFinancialStatementsRequest(isin));
    }

    [HttpGet]
    public async Task<Response<Paginated<GetFinancialStatementsResultDto>>> GetFinancialStatementsListReqeust([FromQuery] GetFinancialStatementListRequest request)
    {
        return await mediator.Send(request);
    }
}