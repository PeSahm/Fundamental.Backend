using ErrorHandling.AspNetCore;
using Fundamental.Application.Statements.Commands.AddMonthlyActivity;
using Fundamental.ErrorHandling;
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
    }
}