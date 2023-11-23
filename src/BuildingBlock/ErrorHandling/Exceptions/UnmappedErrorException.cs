using System.Runtime.Serialization;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.ErrorHandling.Exceptions;

[Serializable]
public class UnmappedErrorException : Exception, ICodedException
{
    public UnmappedErrorException(Enum code)
        : base($"'{code}' case of enum '{code.GetType()}' has not been mapped to any target.")
    {
    }

    protected UnmappedErrorException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.UnmappedError;
    }
}