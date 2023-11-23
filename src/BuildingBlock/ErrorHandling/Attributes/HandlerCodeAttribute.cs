using Fundamental.ErrorHandling.Enums;

namespace Fundamental.ErrorHandling.Attributes;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class)]
public class HandlerCodeAttribute : Attribute
{
    public HandlerCodeAttribute(HandlerCode handlerCode)
    {
        HandlerCode = handlerCode;
    }

    public HandlerCode HandlerCode { get; }
}