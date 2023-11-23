namespace Fundamental.ErrorHandling;

public readonly record struct Response : IResponse
{
    private Response(Error error)
    {
        Success = false;
        Error = error;
    }

    public bool Success { get; init; }

    public object? Data { get; init; } = null;
    public Error? Error { get; init; } = null;

    public static implicit operator Response(Error error)
    {
        return new Response(error);
    }

    public static implicit operator Response(Enum code)
    {
        return new Response(ErrorHandling.Error.FromErrorCode(code));
    }

    public static Response Successful()
    {
        return new Response { Success = true };
    }
}

public readonly record struct Response<TData> : IResponse<TData>
{
    private Response(Error error)
    {
        Success = false;
        Error = error;
    }

    private Response(TData? value)
    {
        Success = true;
        Data = value;
    }

    public bool Success { get; init; }

    public TData? Data { get; } = default;

    public Error? Error { get; init; } = null;

    public static implicit operator Response<TData>(TData value)
    {
        return new Response<TData>(value);
    }

    public static implicit operator Response<TData>(Error error)
    {
        return new Response<TData>(error);
    }

    public static implicit operator Response<TData>(Enum code)
    {
        return new Response<TData>(ErrorHandling.Error.FromErrorCode(code));
    }
}