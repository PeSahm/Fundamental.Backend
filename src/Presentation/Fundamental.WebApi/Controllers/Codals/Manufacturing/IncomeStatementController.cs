using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/income-statement")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class IncomeStatementController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<Response> AddIncomeStatement([FromBody] AddIncomeStatementRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }

    [HttpGet]
    public async Task<Response<Paginated<GetIncomeStatementsResultDto>>> GetIncomeStatement(
        [FromQuery] GetIncomeStatementsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(request, cancellationToken);
    }
}