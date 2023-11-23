using System.Reflection;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fundamental.Web.Common.Swagger.Filters;

public class MaskEnumErrorCodesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum && context.Type.GetCustomAttributes<HandlerCodeAttribute>().Any() &&
            schema.Extensions.TryGetValue("x-enumNames", out IOpenApiExtension? names))
        {
            OpenApiArray enumNames = (OpenApiArray)names;
            Dictionary<string, (int Value, BackendErrorType ErrorType)> values = GetEnumMetadata(context.Type);

            for (int i = 0; i < enumNames.Count; i++)
            {
                OpenApiString enumName = (OpenApiString)enumNames[i];
                string name = enumName.Value;
                (int value, BackendErrorType errorType) = values[name];

                if (errorType != BackendErrorType.BusinessLogic)
                {
                    enumNames[i] = new OpenApiString($"_{value}");
                }
            }
        }
    }

    private static Dictionary<string, (int Value, BackendErrorType ErrorType)> GetEnumMetadata(Type enumType)
    {
        Dictionary<string, (int Value, BackendErrorType ErrorType)> enumMetadata = new();

        foreach (Enum enumValue in enumType.GetEnumValues())
        {
            string name = enumValue.ToString();
            int value = Convert.ToInt32(enumValue);
            BackendErrorType errorType = enumType.GetField(name)!.GetCustomAttribute<ErrorTypeAttribute>()!.BackendErrorType;

            enumMetadata.Add(name, (value, errorType));
        }

        return enumMetadata;
    }
}