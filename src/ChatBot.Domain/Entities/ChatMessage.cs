using ChatBot.Domain.Enums;

namespace ChatBot.Domain.Entities;

/// <summary>
/// Represents a chat message within a session.
/// </summary>
public class ChatMessage
{
    /// <summary>Gets or sets the unique identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the session identifier.</summary>
    public Guid SessionId { get; set; }

    /// <summary>Gets or sets the role of the message sender.</summary>
    public MessageRole Role { get; set; }

    /// <summary>Gets or sets the message content.</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>Gets or sets the message timestamp.</summary>
    public DateTime Timestamp { get; set; }
}
