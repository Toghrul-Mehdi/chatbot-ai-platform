using ChatBot.Application.DTOs.Auth;
using ChatBot.Application.DTOs.User;
using ChatBot.Application.Interfaces;
using ChatBot.Domain.Entities;
using ChatBot.Shared;
using Microsoft.Extensions.Logging;

namespace ChatBot.Application.Services;

/// <summary>
/// Handles user registration and authentication.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<UserService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    public UserService(
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result<TokenResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        _logger.LogInformation("Registering new user: {Username}", request.Username);

        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            return Result<TokenResponseDto>.Failure("Username already exists.");
        }

        var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
        if (existingEmail != null)
        {
            return Result<TokenResponseDto>.Failure("Email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userRepository.AddAsync(user);

        var token = _tokenService.GenerateToken(user);
        var response = new TokenResponseDto
        {
            AccessToken = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_tokenService.ExpirationMinutes)
        };

        _logger.LogInformation("User {Username} registered successfully", request.Username);
        return Result<TokenResponseDto>.Success(response);
    }

    /// <inheritdoc />
    public async Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto request)
    {
        _logger.LogInformation("Login attempt for user: {Username}", request.Username);

        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            return Result<TokenResponseDto>.Failure("Invalid username or password.");
        }

        if (!user.IsActive)
        {
            return Result<TokenResponseDto>.Failure("User account is deactivated.");
        }

        var token = _tokenService.GenerateToken(user);
        var response = new TokenResponseDto
        {
            AccessToken = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_tokenService.ExpirationMinutes)
        };

        _logger.LogInformation("User {Username} logged in successfully", request.Username);
        return Result<TokenResponseDto>.Success(response);
    }

    /// <inheritdoc />
    public async Task<Result<UserDto>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        return Result<UserDto>.Success(new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        });
    }

    private static string HashPassword(string password)
    {
        var salt = new byte[16];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(
            password, salt, 100000, System.Security.Cryptography.HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);

        var combined = new byte[48];
        Array.Copy(salt, 0, combined, 0, 16);
        Array.Copy(hash, 0, combined, 16, 32);
        return Convert.ToBase64String(combined);
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var combined = Convert.FromBase64String(storedHash);
        var salt = new byte[16];
        Array.Copy(combined, 0, salt, 0, 16);

        using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(
            password, salt, 100000, System.Security.Cryptography.HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);

        return System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(
            hash, combined.AsSpan(16));
    }
}
