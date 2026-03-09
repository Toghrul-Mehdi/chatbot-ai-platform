namespace ChatBot.Application.DTOs.Chat;

/// <summary>
/// Represents a history item in a chat conversation.
/// </summary>
public class HistoryItemDto
{
    /// <summary>Gets or sets the role (user, assistant, system).</summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>Gets or sets the message content.</summary>
    public string Content { get; set; } = string.Empty;
}
