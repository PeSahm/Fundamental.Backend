using System.Runtime.Serialization;
using Fundamental.ErrorHandling.Attributes;

namespace Fundamental.ErrorHandling.Exceptions;

/// <summary>
/// Error codes enum must be annotated with <see cref="HandlerCodeAttribute"/>.
/// </summary>
[Serializable]
public class HandlerErrorsAttributeIsMissingException : Exception
{
    public HandlerErrorsAttributeIsMissingException(string enumName)
        : base($"'{enumName}' must be annotated with '{nameof(HandlerCodeAttribute)}'.")
    {
    }

    protected HandlerErrorsAttributeIsMissingException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}