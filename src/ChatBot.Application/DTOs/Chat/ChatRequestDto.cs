namespace ChatBot.Application.DTOs.Chat;

/// <summary>
/// Request DTO for sending a chat message.
/// </summary>
public class ChatRequestDto
{
    /// <summary>Gets or sets the user message.</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Gets or sets the conversation history.</summary>
    public List<HistoryItemDto> History { get; set; } = new();
}
