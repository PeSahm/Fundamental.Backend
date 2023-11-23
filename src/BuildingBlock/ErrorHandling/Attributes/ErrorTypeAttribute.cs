using Fundamental.ErrorHandling.Enums;

namespace Fundamental.ErrorHandling.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ErrorTypeAttribute : Attribute
{
    public ErrorTypeAttribute(BackendErrorType backendErrorType)
    {
        BackendErrorType = backendErrorType;
    }

    public BackendErrorType BackendErrorType { get; }
}