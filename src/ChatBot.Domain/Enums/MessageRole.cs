namespace ChatBot.Domain.Enums;

/// <summary>
/// Represents the role of a message in a chat conversation.
/// </summary>
public enum MessageRole
{
    /// <summary>User message.</summary>
    User,

    /// <summary>AI assistant message.</summary>
    Assistant,

    /// <summary>System message.</summary>
    System
}
