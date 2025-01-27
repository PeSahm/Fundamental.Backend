using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing
{
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
    }
}