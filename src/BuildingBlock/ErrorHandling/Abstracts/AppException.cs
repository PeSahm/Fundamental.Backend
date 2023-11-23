using System.Runtime.Serialization;

namespace Fundamental.ErrorHandling.Abstracts;

[Serializable]
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

    protected AppException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}