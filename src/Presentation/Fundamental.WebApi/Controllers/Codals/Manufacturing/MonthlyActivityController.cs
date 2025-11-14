using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;
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

    public MonthlyActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<Paginated<GetMonthlyActivitiesListItem>>> GetMonthlyActivities(
        [FromQuery] GetMonthlyActivitiesRequest request
    )
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetMonthlyActivityByIdRequest))]
    public async Task<Response<GetMonthlyActivityDetailItem>> GetMonthlyActivity(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetMonthlyActivityByIdRequest(id));
    }
}