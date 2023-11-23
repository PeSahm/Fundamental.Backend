using System.Reflection;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Fundamental.Web.Common.Swagger;

public static class ApiDescriptionExtensions
{
    public static HandlerCode? GetHandlerCode(this ApiDescription apiDescription, out Type? requestType)
    {
        requestType = null;

        if (apiDescription.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(a => a is SwaggerRequestTypeAttribute) is SwaggerRequestTypeAttribute swaggerRequestTypeAttribute)
        {
            requestType = swaggerRequestTypeAttribute.Type;
        }

        requestType ??= apiDescription.ActionDescriptor.Parameters
            .FirstOrDefault(p => p.ParameterType.IsAssignableTo(typeof(IBaseRequest)))?.ParameterType;

        if (requestType is not null)
        {
            return requestType.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode;
        }

        return null;
    }
}