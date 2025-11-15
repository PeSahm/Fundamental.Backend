using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/annual-assembly")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class AnnualAssemblyController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnnualAssemblyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<Paginated<GetAnnualAssemblyListItem>>> GetAnnualAssemblys(
        [FromQuery] GetAnnualAssemblysRequest request
    )
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetAnnualAssemblyByIdRequest))]
    public async Task<Response<GetAnnualAssemblyDetailItem>> GetAnnualAssembly(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetAnnualAssemblyByIdRequest(id));
    }
}
