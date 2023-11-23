using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers
{
    [ApiController]
    [Route("symbols")]
    [ApiVersion("1.0")]
    public class SymbolsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SymbolsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Response<List<GetSymbolsResultDto>>> GetSymbols([FromQuery]string filter)
        {
            return await _mediator.Send(new GetSymbolsRequest(filter));
        }
    }
}
