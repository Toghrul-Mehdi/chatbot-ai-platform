namespace ChatBot.Shared.Helpers;

/// <summary>
/// Helper methods for date and time operations.
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    public static DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Formats a DateTime to ISO 8601 string.
    /// </summary>
    public static string ToIso8601(DateTime dateTime) =>
        dateTime.ToString("o");
}
