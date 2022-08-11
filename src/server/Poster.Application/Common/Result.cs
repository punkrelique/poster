namespace Poster.Application.Common;

public class Result
{
    public bool Success { get; }
    public string Error { get; private set; }

    protected Result(bool success, string error)
    {
        if (success && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException();
        
        if (!success && string.IsNullOrEmpty(error))
            throw new InvalidOperationException();
        
        Success = success;
        Error = error;
    }

    public static Result Fail(string message)
        => new Result(false, message);

    public static ResultOfT<T> Fail<T>(string message)
        => new ResultOfT<T>(default(T), false, message);

    public static Result Ok()
        => new Result(true, string.Empty);

    public static ResultOfT<T> Ok<T>(T value)
        => new ResultOfT<T>(value, true, string.Empty);
}