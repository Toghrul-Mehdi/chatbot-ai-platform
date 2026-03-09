using ChatBot.Domain.Entities;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for JWT token operations.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <returns>The generated JWT token string.</returns>
    string GenerateToken(User user);

    /// <summary>
    /// Gets the token expiration time in minutes.
    /// </summary>
    int ExpirationMinutes { get; }
}
