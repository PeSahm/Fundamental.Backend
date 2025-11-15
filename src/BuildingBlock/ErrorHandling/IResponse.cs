namespace Fundamental.ErrorHandling;

public interface IResponse
{
    bool Success { get; init; }
    Error? Error { get; init; }
}

public interface IResponse<out TData> : IResponse
{
    TData? Data { get; }
}