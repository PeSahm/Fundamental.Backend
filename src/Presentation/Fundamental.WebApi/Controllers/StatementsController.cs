using ErrorHandling.AspNetCore;
using Fundamental.Application.Statements.Commands.AddFinancialStatement;
using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
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

        [HttpPost]
        public async Task<Response> AddStatement([FromBody] AddFinancialStatementRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<Response<Paginated<GetFinancialStatementsResultItem>>> AddStatement(
            [FromQuery] GetFinancialStatementsRequest request
        )
        {
            return await _mediator.Send(request);
        }
    }
}