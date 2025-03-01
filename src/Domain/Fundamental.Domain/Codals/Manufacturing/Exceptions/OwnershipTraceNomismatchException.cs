using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Codals.Manufacturing.Exceptions;

public class OwnershipTraceNoMismatchException : AppException, ICodedException
{
    public OwnershipTraceNoMismatchException(ulong traceNo)
        : base("The trace no is not matched.")
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.TheTracenoIsNotMatched;
    }
}