namespace ChatBot.Shared.Exceptions;

/// <summary>
/// Base application exception.
/// </summary>
public class AppException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    public AppException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }
}
