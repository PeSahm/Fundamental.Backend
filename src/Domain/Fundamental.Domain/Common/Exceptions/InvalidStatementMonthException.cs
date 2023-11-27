using System.Runtime.Serialization;
using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Common.Exceptions;
[Serializable]
public class InvalidStatementMonthException : AppException, ICodedException
{
    public InvalidStatementMonthException(int? month)
        : base($"{month} is not a valid month.")
    {
    }

    public InvalidStatementMonthException(string? month, Exception innerException)
        : base($"{month} is not a valid month.", innerException)
    {
    }

    protected InvalidStatementMonthException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidYearEndMonth;
    }
}