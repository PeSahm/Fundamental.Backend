using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fundamental.Web.Common.Swagger.Filters;

public class RemoveApiVersionHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        OpenApiParameter? versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "x-api-version");

        if (versionParameter != null && context.ApiDescription.Properties.TryGetValue(typeof(ApiVersion), out object? apiVersion))
        {
            string version = apiVersion.ToString()!;

            if (version == "1.0")
            {
                operation.Parameters.Remove(versionParameter);
            }
            else
            {
                versionParameter.Example = new OpenApiString(version);
            }
        }
    }
}