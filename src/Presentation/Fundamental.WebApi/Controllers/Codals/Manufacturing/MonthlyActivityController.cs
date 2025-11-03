using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;
using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/monthly-activity")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class MonthlyActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of MonthlyActivityController using the provided mediator.
    /// </summary>
    public MonthlyActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a paginated list of monthly activities matching the supplied query parameters.
    /// </summary>
    /// <param name="request">Query parameters and filters for listing monthly activities (bound from the query string).</param>
    /// <returns>A Response containing a paginated list of GetMonthlyActivitiesListItem.</returns>
    [HttpGet]
    public async Task<Response<Paginated<GetMonthlyActivitiesListItem>>> GetMonthlyActivities(
        [FromQuery] GetMonthlyActivitiesRequest request
    )
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// Retrieves the detailed information for a monthly activity identified by the given id.
    /// </summary>
    /// <param name="id">The identifier of the monthly activity to retrieve.</param>
    /// <returns>A Response containing the requested GetMonthlyActivityDetailItem.</returns>
    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetMonthlyActivityByIdRequest))]
    public async Task<Response<GetMonthlyActivityDetailItem>> GetMonthlyActivity(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetMonthlyActivityByIdRequest(id));
    }
}