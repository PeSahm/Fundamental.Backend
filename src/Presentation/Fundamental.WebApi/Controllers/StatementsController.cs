using ErrorHandling.AspNetCore;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Statements.Commands.AddFinancialStatement;
using Fundamental.Application.Statements.Commands.UpdateFinancialStatement;
using Fundamental.Application.Statements.Queries.GetFinancialStatementById;
using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using Fundamental.Web.Common.Swagger;
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

        [HttpPut("{id}")]
        public async Task<Response> UpdateStatement([FromBody] UpdateFinancialStatementRequest request, [FromRoute] Guid id)
        {
            if (id != request.Id)
            {
                return CommonErrorCode.DifferentRouteAndBodyIds.ForRequest(request);
            }

            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<Response<Paginated<GetFinancialStatementsResultItem>>> GetFinancialStatements(
            [FromQuery] GetFinancialStatementsRequest request
        )
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        [SwaggerRequestType(typeof(GetFinancialStatementByIdRequest))]
        public async Task<Response<GetFinancialStatementsResultItem>> GetFinancialStatement(
            [FromRoute] Guid id
        )
        {
            return await _mediator.Send(new GetFinancialStatementByIdRequest(id));
        }
    }
}