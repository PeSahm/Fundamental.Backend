namespace Fundamental.ErrorHandling.Abstracts;

public abstract class AppException : Exception
{
    protected AppException()
    {
    }

    protected AppException(string message)
        : base(message)
    {
    }

    protected AppException(string message, Exception inner)
        : base(message, inner)
    {
    }
}