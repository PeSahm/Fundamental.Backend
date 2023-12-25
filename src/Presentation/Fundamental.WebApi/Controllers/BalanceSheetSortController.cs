using ErrorHandling.AspNetCore;
using Fundamental.Application.CodalSorts.Queries.GetBalanceSheetSort;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("balance-sheet-sort")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public sealed class BalanceSheetSortController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerRequestType(typeof(GetBalanceSheetSortRequest))]
    public async Task<Response<List<GetBalanceSheetSortResultDto>>> GetBalanceSheetSortList()
    {
        return await mediator.Send(new GetBalanceSheetSortRequest());
    }
}