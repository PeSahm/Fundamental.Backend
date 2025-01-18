using System.Globalization;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;
using Fundamental.Web.Common.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace Fundamental.WebApi.Swagger;

public class CustomerApiDescriptionProvider(IOptions<ApiExplorerOptions> options) : IApiDescriptionProvider
{
    public int Order => -1;

    public void OnProvidersExecuting(ApiDescriptionProviderContext context)
    {
        // Method intentionally left empty.
    }

    public void OnProvidersExecuted(ApiDescriptionProviderContext context)
    {
        string format = options.Value.GroupNameFormat;
        CultureInfo culture = CultureInfo.CurrentCulture;
        IList<ApiDescription> contextResults = context.Results;
        List<ApiDescription> newResults = new();

        foreach (ApiDescription apiDescription in contextResults)
        {
            ApiVersion apiVersion = apiDescription.GetApiVersion();
            string version = apiVersion.ToString(format, culture);
            HandlerCode? handlerCode = apiDescription.GetHandlerCode(out Type? _);

            if (handlerCode is not null)
            {
                Client clientCode = ErrorCodeHelper.GetClientOfHandlerCode(handlerCode.Value);

                switch (clientCode)
                {
                    case Client.CustomerWeb or Client.AdminWeb or Client.CodalJob:
                        apiDescription.GroupName = $"{clientCode}-{version}";
                        newResults.Add(apiDescription);
                        break;
                    case Client.CustomerShared:
                    {
                        apiDescription.GroupName = $"{Client.AdminWeb}-{version}";
                        newResults.Add(apiDescription);
                        ApiDescription customerWebApiDescription = apiDescription.Clone();
                        customerWebApiDescription.GroupName = $"{Client.CustomerWeb}-{version}";
                        newResults.Add(customerWebApiDescription);
                        break;
                    }
                }
            }
        }

        contextResults.Clear();
        newResults.ForEach(contextResults.Add);
    }
}