using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;
using Fundamental.Application.Symbols.Commands.RejectSymbolShareHolder;
using Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("symbol-share-holders")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class SymbolShareHolderController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<Paginated<GetSymbolShareHoldersResultDto>>> GetSymbolShareHolders(
        [FromQuery] GetSymbolShareHoldersRequest request
    )
    {
        return await mediator.Send(request);
    }

    [HttpPost("reject/{id:guid}")]
    [SwaggerRequestType(typeof(RejectSymbolShareHolderRequest))]
    public async Task<Response> RejectSymbolShareHolder(
        [FromRoute] Guid id
    )
    {
        return await mediator.Send(new RejectSymbolShareHolderRequest(id));
    }

    [HttpPost("approve")]
    public async Task<Response> RejectSymbolShareHolder([FromBody] ApproveSymbolShareHolderRequest request)
    {
        return await mediator.Send(request);
    }
}