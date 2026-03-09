using ChatBot.Domain.Enums;

namespace ChatBot.Domain.Entities;

/// <summary>
/// Represents a chat session between a user and the AI.
/// </summary>
public class ChatSession
{
    /// <summary>Gets or sets the unique identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the user identifier.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>Gets or sets the session status.</summary>
    public SessionStatus Status { get; set; } = SessionStatus.Active;

    /// <summary>Gets or sets the messages in this session.</summary>
    public List<ChatMessage> Messages { get; set; } = new();
}
