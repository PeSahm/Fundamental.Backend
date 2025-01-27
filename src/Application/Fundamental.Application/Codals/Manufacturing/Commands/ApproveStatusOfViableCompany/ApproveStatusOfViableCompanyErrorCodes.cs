using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ApproveStatusOfViableCompany;

[HandlerCode(HandlerCode.ApproveStatusOfViableCompany)]
public enum ApproveStatusOfViableCompanyErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    IdIsNotValid = 13_381_101,

    [ErrorType(BackendErrorType.Security)]
    SubsidiaryIsinIsNotValid = 13_381_102,
}