namespace ChatBot.Application.DTOs.Chat;

/// <summary>
/// Response DTO for a chat message.
/// </summary>
public class ChatResponseDto
{
    /// <summary>Gets or sets the original question.</summary>
    public string Question { get; set; } = string.Empty;

    /// <summary>Gets or sets the AI answer.</summary>
    public string Answer { get; set; } = string.Empty;

    /// <summary>Gets or sets the session identifier.</summary>
    public string SessionId { get; set; } = string.Empty;

    /// <summary>Gets or sets the response timestamp.</summary>
    public DateTime Timestamp { get; set; }
}
