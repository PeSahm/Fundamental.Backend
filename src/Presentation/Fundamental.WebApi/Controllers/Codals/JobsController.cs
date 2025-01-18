using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Jobs.ExecuteStatementJob;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals;

[ApiController]
[Route("jobs")]
[Produces("application/json")]
[TranslateResultToActionResult]
[ApiVersion("1.0")]
public class JobsController(IMediator mediator) : ControllerBase
{
    [HttpPost("statement")]
    public async Task<Response> ExecuteStatementJob([FromBody] ExecuteStatementJobRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }
}