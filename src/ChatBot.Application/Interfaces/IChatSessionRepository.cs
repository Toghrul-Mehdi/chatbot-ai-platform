using ChatBot.Domain.Entities;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for chat session data access.
/// </summary>
public interface IChatSessionRepository
{
    /// <summary>Gets a chat session by identifier.</summary>
    Task<ChatSession?> GetByIdAsync(Guid id);

    /// <summary>Gets all sessions for a user.</summary>
    Task<List<ChatSession>> GetByUserIdAsync(Guid userId);

    /// <summary>Adds a new chat session.</summary>
    Task<ChatSession> AddAsync(ChatSession session);

    /// <summary>Updates an existing chat session.</summary>
    Task UpdateAsync(ChatSession session);
}
