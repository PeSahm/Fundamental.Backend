namespace Fundamental.ErrorHandling.Enums;

public enum BackendErrorType
{
    /// <summary>
    /// This error occurs when the business logic is not
    /// implemented properly. For example, if the user tries to withdraw more
    /// money than the balance in the account, then this error occurs.
    /// </summary>
    BusinessLogic,

    /// <summary>
    /// This error occurs when the
    /// application fails to communicate with the internal service. For example,
    /// if the application fails to communicate with the database to fetch the
    /// user details.
    /// </summary>
    ApplicationFailure,

    /// <summary>
    /// this error occurs when the application
    /// fails to communicate with the third party. For example, if the application
    /// fails to communicate with the SMS service provider to send 2FA code.
    /// </summary>
    ThirdPartyFailure,

    /// <summary>
    /// This error occurs when the user tries to access the
    /// application without proper authentication. For example, if email format is
    /// already validated at the front-end however a potential attacker tries to
    /// access the application by sending a request with invalid email format through
    /// curl or postman to test web service security, in this case application will provide a fake
    /// success message to attacker and at the same time create an urgent alert for the system
    /// administrator to look into the user's profile and block or suspend the user account.
    /// </summary>
    Security,
}