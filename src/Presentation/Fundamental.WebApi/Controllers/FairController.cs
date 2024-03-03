using ErrorHandling.AspNetCore;
using Fundamental.Application.Fairs.Commands.SaveFairs;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundamental.WebApi.Controllers;

[ApiController]
[Route("api/fair")]
[AllowAnonymous]
[TranslateResultToActionResult]
public class FairController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<Response> Save([FromBody] SaveFairRequest fair)
    {
        return await sender.Send(fair);
    }
}