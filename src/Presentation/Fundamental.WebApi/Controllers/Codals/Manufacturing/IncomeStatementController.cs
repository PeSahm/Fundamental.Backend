using ErrorHandling.AspNetCore;
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
    [HttpGet]
    public async Task<Response<Paginated<GetIncomeStatementsResultDto>>> GetIncomeStatement([FromQuery] GetIncomeStatementsRequest request)
    {
        return await mediator.Send(request);
    }
}