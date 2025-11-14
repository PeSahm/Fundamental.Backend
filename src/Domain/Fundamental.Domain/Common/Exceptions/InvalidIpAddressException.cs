using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Common.Exceptions;

public class InvalidIpAddressException : AppException, ICodedException
{
    public InvalidIpAddressException(string? ipAddress)
        : base($"{ipAddress} is not a valid IP address.")
    {
    }

    public InvalidIpAddressException(string? ipAddress, Exception innerException)
        : base($"{ipAddress} is not a valid IP address.", innerException)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidIpAddress;
    }
}