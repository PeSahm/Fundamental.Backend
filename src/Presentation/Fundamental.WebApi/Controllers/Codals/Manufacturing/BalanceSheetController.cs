using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/balance-sheet")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class BalanceSheetController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<Response> AddBalanceSheet([FromBody] AddBalanceSheetRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }

    [HttpGet]
    public async Task<Response<Paginated<GetBalanceSheetResultDto>>> GetBalanceSheets(
        [FromQuery] GetBalanceSheetRequest request,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(request, cancellationToken);
    }

    [HttpGet("{traceNo}/{fiscalYear}/{reportMonth}/details")]
    [SwaggerRequestType(typeof(GetBalanceSheetDetailsRequest))]
    public async Task<Response<List<GetBalanceSheetDetailResultDto>>> GetBalanceSheetDetails(
        [FromRoute] ulong traceNo,
        [FromRoute] ushort fiscalYear,
        [FromRoute] ushort reportMonth,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(new GetBalanceSheetDetailsRequest(traceNo, fiscalYear, reportMonth), cancellationToken);
    }
}