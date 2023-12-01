using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Commands.AddSymbolRelation;
using Fundamental.Application.Symbols.Queries;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("symbol-relations")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class SymbolRelationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SymbolRelationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Response> AddSymbolRelation([FromQuery] AddSymbolRelationRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    public async Task<Response<Paginated<GetSymbolRelationsResultItem>>> GetSymbolRelations([FromQuery] GetSymbolRelationsRequest request)
    {
        return await _mediator.Send(request);
    }
}