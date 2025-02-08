using Fundamental.ErrorHandling.Attributes;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateNoneOperationalIncomeTags;

[HandlerCode(ErrorHandling.Enums.HandlerCode.UpdateNoneOperationalIncomeTags)]
public enum UpdateNoneOperationalIncomeTagsErrorCodes
{
    [ErrorType(ErrorHandling.Enums.BackendErrorType.Security)]
    EntityNotFound = 13_379_101,
}