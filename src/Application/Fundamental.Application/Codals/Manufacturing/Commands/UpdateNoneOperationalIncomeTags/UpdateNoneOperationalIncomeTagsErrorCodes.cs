using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateNoneOperationalIncomeTags;

[HandlerCode(HandlerCode.UpdateNoneOperationalIncomeTags)]
public enum UpdateNoneOperationalIncomeTagsErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    EntityNotFound = 13_379_101
}