using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/income-statement-sort")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public sealed class IncomeStatementSortController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerRequestType(typeof(GetIncomeStatementSortRequest))]
    public async Task<Response<List<GetIncomeStatementSortResultDto>>> GetIncomeStatementSortList()
    {
        return await mediator.Send(new GetIncomeStatementSortRequest());
    }
}