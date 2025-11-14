using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/balance-sheet-sort")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public sealed class BalanceSheetSortController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerRequestType(typeof(GetBalanceSheetSortRequest))]
    public async Task<Response<List<GetBalanceSheetSortResultDto>>> GetBalanceSheetSortList()
    {
        return await mediator.Send(new GetBalanceSheetSortRequest());
    }
}