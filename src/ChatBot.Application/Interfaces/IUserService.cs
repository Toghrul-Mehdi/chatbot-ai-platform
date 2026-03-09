using ChatBot.Application.DTOs.Auth;
using ChatBot.Application.DTOs.User;
using ChatBot.Shared;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for user management operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <returns>A result containing the token response on success.</returns>
    Task<Result<TokenResponseDto>> RegisterAsync(RegisterRequestDto request);

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <returns>A result containing the token response on success.</returns>
    Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto request);

    /// <summary>
    /// Gets a user by their identifier.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>A result containing the user DTO.</returns>
    Task<Result<UserDto>> GetByIdAsync(Guid id);
}
