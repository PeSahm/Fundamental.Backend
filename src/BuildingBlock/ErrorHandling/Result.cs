using System.Diagnostics.CodeAnalysis;

namespace Fundamental.ErrorHandling;

public readonly record struct Result<TValue, TErrorEnum>
    where TErrorEnum : Enum
{
    private readonly TValue? _value;
    private readonly ErrorOf<TErrorEnum>? _error;

    public Result(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }

    public Result(ErrorOf<TErrorEnum> error)
    {
        IsError = true;
        _error = error;
        _value = default;
    }

    public Result()
    {
        IsError = false;
        _value = default;
        _error = default;
    }

    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    public static implicit operator Result<TValue, TErrorEnum>(TValue value)
    {
        return new Result<TValue, TErrorEnum>(value);
    }

    public static implicit operator Result<TValue, TErrorEnum>(TErrorEnum error)
    {
        return new Result<TValue, TErrorEnum>(new ErrorOf<TErrorEnum>(error));
    }

    public bool TryGetValue([NotNullWhen(true)] out TValue? value)
    {
        value = _value;
        return IsSuccess;
    }

    public bool TryGetValue(
        [NotNullWhen(true)] out TValue? value,
        [NotNullWhen(false)] out ErrorOf<TErrorEnum>? error)
    {
        value = _value;
        error = _error;
        return IsSuccess;
    }

    public bool TryGetError([NotNullWhen(true)] out ErrorOf<TErrorEnum>? error)
    {
        error = _error;
        return IsError;
    }
}

public readonly record struct Result<TErrorEnum>
    where TErrorEnum : Enum
{
    private readonly ErrorOf<TErrorEnum>? _error;

    public Result(ErrorOf<TErrorEnum> error)
    {
        IsError = true;
        _error = error;
    }

    public Result()
    {
        IsError = false;
        _error = default;
    }

    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    public static implicit operator Result<TErrorEnum>(TErrorEnum error)
    {
        return new Result<TErrorEnum>(new ErrorOf<TErrorEnum>(error));
    }

    public bool TryGetError([NotNullWhen(true)] out ErrorOf<TErrorEnum>? error)
    {
        error = _error;
        return IsError;
    }

    public static Result<TErrorEnum> Successful()
    {
        return new Result<TErrorEnum>();
    }
}