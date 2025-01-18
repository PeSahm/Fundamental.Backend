using ErrorHandling.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers
{
    [ApiController]
    [Route("statements")]
    [ApiVersion("1.0")]
    [TranslateResultToActionResult]
    public class StatementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatementsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}