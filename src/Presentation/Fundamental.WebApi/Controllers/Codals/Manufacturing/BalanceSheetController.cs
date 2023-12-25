using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/balance-sheet")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class BalanceSheetController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<Response> AddBalanceSheet([FromBody] AddBalanceSheetRequest request)
    {
        return await mediator.Send(request);
    }

    [HttpGet]
    public async Task<Response<Paginated<GetBalanceSheetResultDto>>> GetBalanceSheets([FromQuery] GetBalanceSheetRequest request)
    {
        return await mediator.Send(request);
    }
}