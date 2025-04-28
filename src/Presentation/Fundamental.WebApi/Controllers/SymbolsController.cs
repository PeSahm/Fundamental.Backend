using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("symbols")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class SymbolsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetSymbolsResultDto>>> GetSymbols(
        [FromQuery] GetSymbolsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(request, cancellationToken);
    }
}