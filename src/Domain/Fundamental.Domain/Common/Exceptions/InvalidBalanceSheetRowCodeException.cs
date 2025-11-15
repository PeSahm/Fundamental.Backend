using Fundamental.ErrorHandling.Abstracts;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Interfaces;

namespace Fundamental.Domain.Common.Exceptions;

public class InvalidBalanceSheetRowCodeException : AppException, ICodedException
{
    public InvalidBalanceSheetRowCodeException(ushort? rowCode)
        : base($"{rowCode} is not a valid BalanceSheet row code.")
    {
    }

    public InvalidBalanceSheetRowCodeException(ushort? rowCode, Exception innerException)
        : base($"{rowCode} is not a valid BalanceSheet row code.", innerException)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.InvalidCodalBalanceSheetRow;
    }
}