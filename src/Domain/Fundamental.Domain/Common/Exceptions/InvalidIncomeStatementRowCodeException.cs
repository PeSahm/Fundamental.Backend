using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Common.Exceptions;

public class InvalidIncomeStatementRowCodeException : AppException, ICodedException
{
    public InvalidIncomeStatementRowCodeException(ushort? rowCode)
        : base($"{rowCode} is not a valid  incomeStatement row code.")
    {
    }

    public InvalidIncomeStatementRowCodeException(ushort? rowCode, Exception innerException)
        : base($"{rowCode} is not a valid incomeStatement row code.", innerException)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidCodalIncomeStatementRow;
    }
}