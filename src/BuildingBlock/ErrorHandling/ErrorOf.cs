namespace Fundamental.ErrorHandling;

public readonly record struct ErrorOf<TError>(TError Code, Dictionary<string, string>? Data = null)
    where TError : Enum
{
    public static implicit operator ErrorOf<TError>(TError code)
    {
        return new ErrorOf<TError>(code);
    }

    public TTarget Map<TTarget>(Func<TError, TTarget> map)
    {
        return map(Code);
    }
}