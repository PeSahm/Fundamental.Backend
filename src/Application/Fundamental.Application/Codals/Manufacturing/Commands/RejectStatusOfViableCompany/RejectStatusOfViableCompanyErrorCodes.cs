using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.RejectStatusOfViableCompany;

public enum RejectStatusOfViableCompanyErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    NotFound = 13_380_101,
}