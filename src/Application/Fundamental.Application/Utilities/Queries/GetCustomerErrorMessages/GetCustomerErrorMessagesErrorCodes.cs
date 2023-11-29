using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Utilities.Queries.GetCustomerErrorMessages;

[HandlerCode(HandlerCode.GetCustomerErrorMessages)]
public enum GetCustomerErrorMessagesErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    InvalidClient = 17_999_101,
}