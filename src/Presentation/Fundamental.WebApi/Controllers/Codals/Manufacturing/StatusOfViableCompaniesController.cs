using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Commands.ApproveStatusOfViableCompany;
using Fundamental.Application.Codals.Manufacturing.Commands.RejectStatusOfViableCompany;
using Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/status-of-viable-companies")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class StatusOfViableCompaniesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<Paginated<GetStatusOfViableCompaniesResultDto>>> GetSymbolShareHolders(
        [FromQuery] GetStatusOfViableCompaniesRequest request
    )
    {
        return await mediator.Send(request);
    }

    [HttpPut("reject/{id:guid}")]
    [SwaggerRequestType(typeof(RejectStatusOfViableCompanyRequest))]
    public async Task<Response> RejectSymbolShareHolder(
        [FromRoute] Guid id
    )
    {
        return await mediator.Send(new RejectStatusOfViableCompanyRequest(id));
    }

    [HttpPut("approve")]
    public async Task<Response> RejectSymbolShareHolder([FromBody] ApproveStatusOfViableCompanyRequest request)
    {
        return await mediator.Send(request);
    }
}