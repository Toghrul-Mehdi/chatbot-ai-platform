namespace ChatBot.Shared.Exceptions;

/// <summary>
/// Exception thrown when validation fails.
/// </summary>
public class ValidationException : AppException
{
    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException(string message) : base(message, 400)
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with validation errors.
    /// </summary>
    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.", 400)
    {
        Errors = new Dictionary<string, string[]>(errors);
    }
}
