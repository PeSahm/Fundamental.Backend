using System.Reflection;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Exceptions;

namespace Fundamental.ErrorHandling.Helpers;

public static class ErrorTypeHelper
{
    /// <summary>
    /// Get the <see cref="BackendErrorType"/> for the given error code.
    /// </summary>
    /// <exception cref="ErrorTypeAttributeIsMissingException">Occurs if the error code is not annotated with <see cref="ErrorTypeAttribute"/>.</exception>
    public static BackendErrorType GetBackendErrorType(Enum errorCode)
    {
        Type enumType = errorCode.GetType();
        FieldInfo fieldInfo = enumType.GetField(errorCode.ToString())!;
        ErrorTypeAttribute errorTypeAttribute = fieldInfo.GetCustomAttribute<ErrorTypeAttribute>()
                                                ?? throw new ErrorTypeAttributeIsMissingException(errorCode);

        return errorTypeAttribute.BackendErrorType;
    }

    /// <summary>
    /// Get the <see cref="ClientErrorType"/> for the given error code.
    /// </summary>
    /// <exception cref="ErrorTypeAttributeIsMissingException">Occurs if the error code is not annotated with <see cref="ErrorTypeAttribute"/>.</exception>
    public static ClientErrorType GetClientErrorType(Enum errorCode)
    {
        return GetClientErrorType(GetBackendErrorType(errorCode));
    }

    /// <summary>
    /// Get <see cref="ClientErrorType"/> from <see cref="BackendErrorType"/>.
    /// </summary>
    /// <exception cref="ErrorTypeAttributeIsMissingException">Occurs if the error code is not annotated with <see cref="ErrorTypeAttribute"/>.</exception>
    public static ClientErrorType GetClientErrorType(BackendErrorType backendErrorType)
    {
        return backendErrorType switch
        {
            BackendErrorType.BusinessLogic => ClientErrorType.BusinessLogic,
            BackendErrorType.Security => ClientErrorType.BusinessLogic,
            _ => ClientErrorType.InternalServerError,
        };
    }
}