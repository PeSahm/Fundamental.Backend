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
public class NonOperationIncomeController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Response<Paginated<GetNonOperationIncomesResultItem>>> GetNoneOperationalIncomes(
        [FromQuery] GetNonOperationIncomesRequest request
    )
    {
        return await mediator.Send(request);
    }
}