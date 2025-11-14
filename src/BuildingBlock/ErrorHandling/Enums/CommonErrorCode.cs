using Fundamental.ErrorHandling.Attributes;

namespace Fundamental.ErrorHandling.Enums;

/// <summary>
///     3 digit error codes for common errors which are not specific to any request.
///     Instead of creating a new error code for each request, these common error codes are used in the pipeline behavior
///     by appending these 3 digit codes to the request (handler) code.
/// </summary>
public enum CommonErrorCode
{
    [ErrorType(BackendErrorType.ApplicationFailure)]
    UnexpectedError = 600,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidIdentity = 601,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidIpAddress = 603,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidYearEndMonth = 604,

    AuthTokenGenerationFailure = 606,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    UnmappedError = 607,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DatabaseError = 700,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DatabaseConnectionFailed = 701,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    SaveChangesFailed = 702,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DbUpdateConcurrencyFailed = 703,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DuplicateEntry = 704,

    [ErrorType(BackendErrorType.Security)]
    ValidationFailed = 800,

    [ErrorType(BackendErrorType.BusinessLogic)]
    AccountIsSuspended = 801,

    [ErrorType(BackendErrorType.Security)]
    ModelBindingFailed = 804,

    [ErrorType(BackendErrorType.Security)]
    DifferentRouteAndBodyIds = 807,

    [ErrorType(BackendErrorType.BusinessLogic)]
    InvalidCodalBalanceSheetRow = 808,

    [ErrorType(BackendErrorType.BusinessLogic)]
    InvalidCodalIncomeStatementRow = 809,

    [ErrorType(BackendErrorType.BusinessLogic)]
    TheTracenoIsNotMatched = 810
}