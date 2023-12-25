using ErrorHandling.AspNetCore;
using Fundamental.Application.Statements.Commands.AddBalanceSheet;
using Fundamental.Application.Statements.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("balance-sheet")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
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