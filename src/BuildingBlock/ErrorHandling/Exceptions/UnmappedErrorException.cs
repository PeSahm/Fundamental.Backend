using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.ErrorHandling.Exceptions;

public class UnmappedErrorException : Exception, ICodedException
{
    public UnmappedErrorException(Enum code)
        : base($"'{code}' case of enum '{code.GetType()}' has not been mapped to any target.")
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.UnmappedError;
    }
}