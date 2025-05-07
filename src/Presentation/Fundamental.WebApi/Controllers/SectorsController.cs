using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Queries.GetSectors;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("sectors")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class SectorsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetSectorsResultDto>>> GetSectors(
        [FromQuery] GetSectorsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(request, cancellationToken);
    }
}