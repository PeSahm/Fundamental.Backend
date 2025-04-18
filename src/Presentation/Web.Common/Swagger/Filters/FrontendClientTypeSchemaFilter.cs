using Fundamental.Application;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fundamental.Web.Common.Swagger.Filters;

public class FrontendClientTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Title == SchemaTitle.FRONTEND_CLIENT_TYPE)
        {
            schema.Enum = new List<IOpenApiAny>
            {
                new OpenApiString("admin"),
                new OpenApiString("web"),
                new OpenApiString("job")
            };
        }
    }
}