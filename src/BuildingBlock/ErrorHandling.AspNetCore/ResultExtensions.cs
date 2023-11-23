using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.AspNetCore
{
    /// <summary>
    /// Extensions to support converting Result to an ActionResult.
    /// </summary>
    public static class ResultExtensions
    {
        public static ActionResult<T> ToActionResult<T>(this Response<T> result, ControllerBase controller)
        {
            return controller.ToActionResult((IResponse)result);
        }

        public static ActionResult ToActionResult(this Response result, ControllerBase controller)
        {
            return controller.ToActionResult((IResponse)result);
        }

        public static ActionResult<T> ToActionResult<T>(
            this ControllerBase controller,
            Response<T> result
        )
        {
            return controller.ToActionResult((IResponse)result);
        }

        public static ActionResult ToActionResult(this ControllerBase controller, Response result)
        {
            return controller.ToActionResult((IResponse)result);
        }

        internal static ActionResult ToActionResult(this ControllerBase controller, IResponse result)
        {
            switch (result.Success)
            {
                case true:
                    return controller.Ok(result);
                default:
                    switch (result.Error!.Value.Type)
                    {
                        case ClientErrorType.BusinessLogic:
                            return controller.BadRequest(result);
                        default:
                            return controller.StatusCode(500, result);
                    }
            }
        }
    }
}