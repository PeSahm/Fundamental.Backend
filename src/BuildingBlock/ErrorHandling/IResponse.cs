namespace Fundamental.ErrorHandling;

public interface IResponse
{
    public bool Success { get; init; }
    public Error? Error { get; init; }
}

public interface IResponse<out TData> : IResponse
{
    public TData? Data { get; }
}