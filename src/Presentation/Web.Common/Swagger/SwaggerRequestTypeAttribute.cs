namespace Fundamental.Web.Common.Swagger;

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerRequestTypeAttribute : Attribute
{
    public SwaggerRequestTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}