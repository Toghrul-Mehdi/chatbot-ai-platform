namespace ChatBot.Shared;

/// <summary>
/// Generic result wrapper for operation outcomes.
/// </summary>
/// <typeparam name="T">The type of data returned on success.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the data returned on success.
    /// </summary>
    public T? Data { get; }

    private Result(bool isSuccess, T? data, string? error)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    /// <summary>
    /// Creates a successful result with the specified data.
    /// </summary>
    public static Result<T> Success(T data) => new(true, data, null);

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    public static Result<T> Failure(string error) => new(false, default, error);
}
