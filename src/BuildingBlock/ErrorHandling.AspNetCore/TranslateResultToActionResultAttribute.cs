using Fundamental.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ErrorHandling.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TranslateResultToActionResultAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult { Value: IResponse result })
        {
            return;
        }

        if (context.Controller is not ControllerBase controller)
        {
            return;
        }

        context.Result = controller.ToActionResult(result);
    }
}