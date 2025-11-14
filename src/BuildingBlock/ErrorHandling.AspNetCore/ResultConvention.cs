using System.Net;
using System.Runtime.CompilerServices;
using Fundamental.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;

[assembly: InternalsVisibleTo("Ardalis.Result.AspNetCore.UnitTests")]

namespace ErrorHandling.AspNetCore;

internal class ResultConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        if (!action.Filters.Any(f => f is TranslateResultToActionResultAttribute)
            && !action.Controller.Filters.Any(f => f is TranslateResultToActionResultAttribute))
        {
            return;
        }

        Type returnType = action.ActionMethod.ReturnType;

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            returnType = returnType.GetGenericArguments()[0];
        }

        bool isResult = returnType.IsGenericType &&
                        (returnType.GetGenericTypeDefinition() == typeof(Response) ||
                         returnType.GetGenericTypeDefinition() == typeof(Response<>));

        if (isResult)
        {
            AddProducesResponseTypeAttribute(action.Filters, (int)HttpStatusCode.OK, returnType);
            AddProducesResponseTypeAttribute(action.Filters, (int)HttpStatusCode.BadRequest, returnType);
            AddProducesResponseTypeAttribute(action.Filters, (int)HttpStatusCode.InternalServerError, returnType);
        }
    }

    private static void AddProducesResponseTypeAttribute(IList<IFilterMetadata> filters, int statusCode, Type? responseType)
    {
        if (!filters.Any(f => f is IApiResponseMetadataProvider rmp && rmp.StatusCode == statusCode))
        {
            filters.Add(responseType == null
                ? new ProducesResponseTypeAttribute(statusCode)
                : new ProducesResponseTypeAttribute(responseType, statusCode));
        }
    }
}