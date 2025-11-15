using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/interpretative-report-summary-page5")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class InterpretativeReportSummaryPage5Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public InterpretativeReportSummaryPage5Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>> GetInterpretativeReportSummaryPage5s(
        [FromQuery] GetInterpretativeReportSummaryPage5sRequest request
    )
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SwaggerRequestType(typeof(GetInterpretativeReportSummaryPage5ByIdRequest))]
    public async Task<Response<GetInterpretativeReportSummaryPage5DetailItem>> GetInterpretativeReportSummaryPage5(
        [FromRoute] Guid id
    )
    {
        return await _mediator.Send(new GetInterpretativeReportSummaryPage5ByIdRequest(id));
    }
}
