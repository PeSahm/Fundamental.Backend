using Fundamental.ErrorHandling.Attributes;

namespace Fundamental.ErrorHandling.Exceptions;

/// <summary>
/// Error codes enum field must be annotated with <see cref="ErrorTypeAttribute"/>.
/// </summary>
public class ErrorTypeAttributeIsMissingException : Exception
{
    public ErrorTypeAttributeIsMissingException(Enum enumValue)
        : base($"'{enumValue.ToString()}' value of '{enumValue.GetType()}' enum must be annotated " +
               $"with '{nameof(ErrorTypeAttribute)}'.")
    {
    }
}