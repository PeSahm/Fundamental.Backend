using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/extra-annual-assembly")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class ExtraAnnualAssemblyController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExtraAnnualAssemblyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<Paginated<GetExtraAnnualAssemblyListItem>>> GetExtraAnnualAssemblys(
        [FromQuery] GetExtraAnnualAssemblysRequest request
    )
    {
        return await _mediator.Send(request);
    }
}
