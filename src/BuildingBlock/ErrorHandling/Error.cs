using System.Text.Json.Serialization;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;

namespace Fundamental.ErrorHandling;

/// <summary>
/// Represents an error.
/// </summary>
public readonly record struct Error
{
    private Error(int code, BackendErrorType backendType, Dictionary<string, string>? values = null)
    {
        Code = code;
        BackendType = backendType;
        Values = values;
    }

    /// <summary>
    /// Gets the unique error code.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Gets <see cref="BackendErrorType"/> of the error.
    /// </summary>
    [JsonIgnore]
    public BackendErrorType BackendType { get; }

    /// <summary>
    /// Gets <see cref="ClientErrorType"/> of the error.
    /// </summary>
    public ClientErrorType Type => ErrorTypeHelper.GetClientErrorType(BackendType);

    /// <summary>
    /// Gets values of placeholders in the error message.
    /// </summary>
    public Dictionary<string, string>? Values { get; }

    public static Error FromErrorCode(Enum code, Dictionary<string, string>? values = null)
    {
        BackendErrorType backendErrorType = ErrorTypeHelper.GetBackendErrorType(code);
        return new Error(Convert.ToInt32(code), backendErrorType, values);
    }

    public static Error FromCommonErrorCode(HandlerCode handlerCode, CommonErrorCode commonCode, Dictionary<string, string>? values = null)
    {
        BackendErrorType backendErrorType = ErrorTypeHelper.GetBackendErrorType(commonCode);
        return new Error((Convert.ToInt32(handlerCode) * 1000) + Convert.ToInt32(commonCode), backendErrorType, values);
    }
}
