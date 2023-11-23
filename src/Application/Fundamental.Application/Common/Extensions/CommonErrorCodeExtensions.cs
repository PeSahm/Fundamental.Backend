using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Common.Extensions;

public static class CommonErrorCodeExtensions
{
    public static Error ForRequest<TRequest>(this CommonErrorCode errorCode, TRequest request, Dictionary<string, string>? values = null)
        where TRequest : IBaseRequest
    {
        return Error.FromCommonErrorCode(request.GetHandlerCode(), errorCode, values);
    }

    public static Error ForRequestType<TRequest>(this CommonErrorCode errorCode, Dictionary<string, string>? values = null)
        where TRequest : IBaseRequest
    {
        return Error.FromCommonErrorCode(RequestExtensions.GetHandlerCode<TRequest>(), errorCode, values);
    }
}