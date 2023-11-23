using System.Runtime.Serialization;
using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Exceptions;

[Serializable]
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

    protected InvalidIpAddressException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidIpAddress;
    }
}