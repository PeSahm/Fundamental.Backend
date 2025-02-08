using ErrorHandling.AspNetCore;
using Fundamental.Application.Codals.Manufacturing.Commands.UpdateNoneOperationalIncomeTags;
using Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;
using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
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

    [HttpPut("tags/{id}")]
    public async Task<Response> UpdateTags([FromBody] UpdateNoneOperationalIncomeTagsRequest request, [FromRoute] Guid id)
    {
        if (id != request.Id)
        {
            return CommonErrorCode.DifferentRouteAndBodyIds.ForRequest(request);
        }

        return await mediator.Send(request);
    }
}