using System.Collections.Concurrent;
using ChatBot.Application.Interfaces;
using ChatBot.Domain.Entities;

namespace ChatBot.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of the chat session repository.
/// </summary>
public class ChatSessionRepository : IChatSessionRepository
{
    private readonly ConcurrentDictionary<Guid, ChatSession> _sessions = new();

    /// <inheritdoc />
    public Task<ChatSession?> GetByIdAsync(Guid id)
    {
        _sessions.TryGetValue(id, out var session);
        return Task.FromResult(session);
    }

    /// <inheritdoc />
    public Task<List<ChatSession>> GetByUserIdAsync(Guid userId)
    {
        var sessions = _sessions.Values
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToList();
        return Task.FromResult(sessions);
    }

    /// <inheritdoc />
    public Task<ChatSession> AddAsync(ChatSession session)
    {
        _sessions.TryAdd(session.Id, session);
        return Task.FromResult(session);
    }

    /// <inheritdoc />
    public Task UpdateAsync(ChatSession session)
    {
        _sessions[session.Id] = session;
        return Task.CompletedTask;
    }
}
