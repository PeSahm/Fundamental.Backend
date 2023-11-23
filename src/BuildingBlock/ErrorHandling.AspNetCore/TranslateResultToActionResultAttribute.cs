using Fundamental.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ErrorHandling.AspNetCore
{
    public class TranslateResultToActionResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!((context.Result as ObjectResult)?.Value is IResponse result))
            {
                return;
            }

            if (!(context.Controller is ControllerBase controller))
            {
                return;
            }

            context.Result = controller.ToActionResult(result);
        }
    }
}
