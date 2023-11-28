using Fundamental.ErrorHandling.Attributes;

namespace Fundamental.ErrorHandling.Exceptions;

public class HandlerErrorsAttributeIsMissingException : Exception
{
    public HandlerErrorsAttributeIsMissingException(string enumName)
        : base($"'{enumName}' must be annotated with '{nameof(HandlerCodeAttribute)}'.")
    {
    }
}