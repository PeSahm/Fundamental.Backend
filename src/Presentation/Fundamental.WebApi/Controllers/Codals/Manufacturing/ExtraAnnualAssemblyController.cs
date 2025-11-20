using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
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

    /// <summary>
    /// Creates an ExtraAnnualAssemblyController using the provided mediator.
    /// </summary>
    public ExtraAnnualAssemblyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a paginated list of extra annual assembly items.
    /// </summary>
    /// <param name="request">Query parameters that control filtering, sorting, and pagination for the list.</param>
    /// <returns>A <see cref="Response{Paginated{GetExtraAnnualAssemblyListItem}}"/> containing the requested page of items.</returns>
    [HttpGet]
    public async Task<Response<Paginated<GetExtraAnnualAssemblyListItem>>> GetExtraAnnualAssemblys(
        [FromQuery] GetExtraAnnualAssemblysRequest request
    )
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// Retrieves details for a specific extra annual assembly by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the extra annual assembly to retrieve.</param>
    /// <returns>A Response containing the requested GetExtraAnnualAssemblyDetailItem.</returns>
    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetExtraAnnualAssemblyByIdRequest))]
    public async Task<Response<GetExtraAnnualAssemblyDetailItem>> GetExtraAnnualAssembly(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetExtraAnnualAssemblyByIdRequest(id));
    }
}