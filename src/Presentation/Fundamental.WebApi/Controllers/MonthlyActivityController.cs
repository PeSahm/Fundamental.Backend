using ErrorHandling.AspNetCore;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Statements.Commands.AddMonthlyActivity;
using Fundamental.Application.Statements.Commands.UpdateMonthlyActivity;
using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Application.Statements.Queries.GetMonthlyActivityById;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers
{
    [ApiController]
    [Route("monthly-activity")]
    [ApiVersion("1.0")]
    [TranslateResultToActionResult]
    public class MonthlyActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MonthlyActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Response> AddStatement([FromBody] AddMonthlyActivityRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("{id}")]
        public async Task<Response> UpdateStatement([FromBody] UpdateMonthlyActivityRequest request, [FromRoute] Guid id)
        {
            if (id != request.Id)
            {
                return CommonErrorCode.DifferentRouteAndBodyIds.ForRequest(request);
            }

            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<Response<Paginated<GetMonthlyActivitiesResultItem>>> GetMonthlyActivities(
            [FromQuery] GetMonthlyActivitiesRequest request
        )
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        [SwaggerRequestType(typeof(GetMonthlyActivityByIdRequest))]
        public async Task<Response<GetMonthlyActivitiesResultItem>> GetMonthlyActivity(
            [FromRoute] Guid id
        )
        {
            return await _mediator.Send(new GetMonthlyActivityByIdRequest(id));
        }
    }
}