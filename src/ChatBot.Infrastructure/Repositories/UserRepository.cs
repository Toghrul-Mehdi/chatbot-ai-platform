using System.Collections.Concurrent;
using ChatBot.Application.Interfaces;
using ChatBot.Domain.Entities;

namespace ChatBot.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of the user repository.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    /// <inheritdoc />
    public Task<User?> GetByIdAsync(Guid id)
    {
        _users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task<User?> GetByUsernameAsync(string username)
    {
        var user = _users.Values.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task<User?> GetByEmailAsync(string email)
    {
        var user = _users.Values.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task<User> AddAsync(User user)
    {
        _users.TryAdd(user.Id, user);
        return Task.FromResult(user);
    }
}
