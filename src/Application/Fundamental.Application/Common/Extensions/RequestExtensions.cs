using System.Collections.Concurrent;
using System.Reflection;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Common.Extensions;

public static class RequestExtensions
{
    private static readonly ConcurrentDictionary<Type, HandlerCode> _handlerCodeCache = new();

    public static HandlerCode GetHandlerCode<TRequest>(this TRequest request)
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(request.GetType());
    }

    public static HandlerCode GetHandlerCode<TRequest>()
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(typeof(TRequest));
    }

    private static HandlerCode GetHandlerCodeOfType(Type type)
    {
        return _handlerCodeCache.GetOrAdd(type, t => t.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode);
    }
}