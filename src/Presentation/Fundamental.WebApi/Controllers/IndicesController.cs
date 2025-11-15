using ErrorHandling.AspNetCore;
using Fundamental.Application.Symbols.Queries.GetIndices;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("indices")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
public class IndicesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<GetIndicesResultDto>> GetIndices([FromQuery] GetIndicesRequest request)
    {
        return await mediator.Send(request);
    }
}