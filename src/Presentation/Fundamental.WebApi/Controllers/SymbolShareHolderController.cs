using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
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
}