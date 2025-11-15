using System.Reflection;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;
using Fundamental.Web.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fundamental.WebApi.Extensions;

public static class ApiBehaviorOptionsExtensions
{
    public static IMvcBuilder ConfigureCustomApiBehaviorOptions(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
                {
                    logger.LogError(
                        "MODEL_BINDING_FAILED --- Null ActionDescriptor --- Errors: {@Errors}",
                        context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
                    return DefaultResult(context);
                }

                HandlerCode? handlerCode = GetActionHandlerCode(actionDescriptor.MethodInfo);

                if (handlerCode is not null)
                {
                    logger.LogError(
                        "MODEL_BINDING_FAILED --- {ErrorCode} --- Errors: {@Errors}",
                        ErrorCodeHelper.Format(((int)handlerCode * 1000) + (int)CommonErrorCode.ModelBindingFailed),
                        context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    );
                    return new OkObjectResult((Response)Error.FromCommonErrorCode(
                        handlerCode.Value,
                        CommonErrorCode.ModelBindingFailed,
                        context.ModelState.ToDictionary(
                            a => a.Key.TrimStart('$', '.'),
                            a => string.Join(", ", a.Value!.Errors.Select(e => e.ErrorMessage)))
                    ));
                }

                logger.LogError(
                    "MODEL_BINDING_FAILED --- Unknown Handler --- Errors: '{@Errors}'",
                    context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
                return DefaultResult(context);
            };
        });
    }

    private static HandlerCode? GetActionHandlerCode(MethodInfo methodInfo)
    {
        Type? requestType = null;
        SwaggerRequestTypeAttribute? requestTypeAttribute = methodInfo.GetCustomAttribute<SwaggerRequestTypeAttribute>();

        if (requestTypeAttribute is not null)
        {
            requestType = requestTypeAttribute.Type;
        }

        requestType ??= Array.Find(
            methodInfo.GetParameters(),
            p => p.ParameterType.IsAssignableTo(typeof(IBaseRequest)))?.ParameterType;
        return requestType?.GetCustomAttribute<HandlerCodeAttribute>()?.HandlerCode;
    }

    private static IActionResult DefaultResult(ActionContext context)
    {
        ProblemDetailsFactory problemDetailsFactory =
            context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        ValidationProblemDetails responseModel =
            problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
        return new BadRequestObjectResult(responseModel);
    }
}