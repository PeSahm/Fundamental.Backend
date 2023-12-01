using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Commands.AddSymbolRelation;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("symbols")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class SymbolsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SymbolsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<List<GetSymbolsResultDto>>> GetSymbols([FromQuery] GetSymbolsRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("relation")]
    public async Task<Response> AddSymbolRelation([FromQuery] AddSymbolRelationRequest request)
    {
        return await _mediator.Send(request);
    }
}