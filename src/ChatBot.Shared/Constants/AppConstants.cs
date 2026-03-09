namespace ChatBot.Shared.Constants;

/// <summary>
/// Application-wide constants.
/// </summary>
public static class AppConstants
{
    /// <summary>
    /// The name of the application.
    /// </summary>
    public const string AppName = "ChatBot.API";

    /// <summary>
    /// The API version prefix.
    /// </summary>
    public const string ApiPrefix = "api";

    /// <summary>
    /// The name of the AI service HTTP client.
    /// </summary>
    public const string AiServiceHttpClient = "AiService";

    /// <summary>
    /// The default rate limit policy name.
    /// </summary>
    public const string RateLimitPolicy = "PerUserRateLimit";

    /// <summary>
    /// The CORS policy name.
    /// </summary>
    public const string CorsPolicy = "AllowAll";
}
