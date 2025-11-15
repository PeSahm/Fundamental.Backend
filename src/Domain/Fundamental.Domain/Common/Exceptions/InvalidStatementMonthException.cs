using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Common.Exceptions;

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

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidYearEndMonth;
    }
}