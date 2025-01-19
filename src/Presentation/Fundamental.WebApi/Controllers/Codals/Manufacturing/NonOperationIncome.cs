using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers.Codals.Manufacturing;

[ApiController]
[Route("[area]/non-operation-income")]
[ApiVersion("1.0")]
[TranslateResultToActionResult]
[Area("Manufacturing")]
public class NonOperationIncome(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<Paginated<GetNonOperationIncomesResultItem>>> GetNoneOperationalIncoms(
        [FromQuery] GetNonOperationIncomesRequest request
    )
    {
        return await mediator.Send(request);
    }
}