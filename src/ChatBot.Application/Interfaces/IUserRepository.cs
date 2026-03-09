using ChatBot.Domain.Entities;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for user data access.
/// </summary>
public interface IUserRepository
{
    /// <summary>Gets a user by identifier.</summary>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>Gets a user by username.</summary>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>Gets a user by email.</summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>Adds a new user.</summary>
    Task<User> AddAsync(User user);
}
