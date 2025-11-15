using Fundamental.Application;
using Fundamental.Application.Utilities.Queries.GetCustomerErrorMessages;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("error-messages")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class ErrorMessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ErrorMessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{client}", Name = "GetCustomerErrorMessages")]
    [SwaggerRequestType(typeof(GetCustomerErrorMessagesRequest))]
    public async Task<Response<Dictionary<string, string>>> GetAll(
        [FromRoute][SwaggerSchema(Title = SchemaTitle.FRONTEND_CLIENT_TYPE)] string client,
        [FromQuery] string? culture
    )
    {
        GetCustomerErrorMessagesRequest request = new(client, culture);
        return await _mediator.Send(request);
    }
}