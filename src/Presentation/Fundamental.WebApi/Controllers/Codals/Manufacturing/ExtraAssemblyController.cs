using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/extra-assembly")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class ExtraAssemblyController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExtraAssemblyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<Paginated<GetExtraAssemblyListItem>>> GetExtraAssemblys(
        [FromQuery] GetExtraAssemblysRequest request
    )
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetExtraAssemblyByIdRequest))]
    public async Task<Response<GetExtraAssemblyDetailItem>> GetExtraAssembly(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetExtraAssemblyByIdRequest(id));
    }
}
