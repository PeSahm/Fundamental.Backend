using Fundamental.ErrorHandling.Enums;

namespace Fundamental.ErrorHandling.Helpers;

public static class ErrorCodeHelper
{
    public static string Format(int code)
    {
        return code.ToString("n0").Replace(",", "_");
    }

    public static int ToInt(string code)
    {
        return int.Parse(code.Replace("_", string.Empty));
    }

    public static bool IsCommonErrorCode(int errorCode)
    {
        int code = errorCode % 1000;
        return code >= 600;
    }

    /// <summary>
    /// Determines the domain of the error code.
    /// </summary>
    public static DomainCode? GetDomainOfErrorCode(int errorCode)
    {
        int domainNumber = errorCode / 1000000;
        return Enum.IsDefined(typeof(DomainCode), domainNumber)
            ? (DomainCode)domainNumber
            : null;
    }

    /// <summary>
    /// Determines the client of the error code.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">If client digit of the error code does not exist in the <see cref="Client"/> enum.</exception>
    public static Client GetClientOfErrorCode(string errorCode)
    {
        int errorCodeInt = ToInt(errorCode);
        int clientDigit = (errorCodeInt / 100000) % 10;
        return Enum.IsDefined(typeof(Client), clientDigit)
            ? (Client)clientDigit
            : throw new ArgumentOutOfRangeException($"Client digit '{clientDigit}' detected for error code '{errorCode}' is not valid.");
    }

    /// <summary>
    /// Determines the client of the handler code.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">If client digit of the handler code does not exist in the <see cref="Client"/> enum.</exception>
    public static Client GetClientOfHandlerCode(HandlerCode handlerCode)
    {
        int clientDigit = ((int)handlerCode % 1000) / 100;
        return Enum.IsDefined(typeof(Client), clientDigit)
            ? (Client)clientDigit
            : throw new ArgumentOutOfRangeException(
                $"Client digit '{clientDigit}' detected for handler code '{handlerCode}' is not valid.");
    }
}