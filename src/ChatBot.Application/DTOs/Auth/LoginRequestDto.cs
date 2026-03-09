namespace ChatBot.Application.DTOs.Auth;

/// <summary>
/// Request DTO for user login.
/// </summary>
public class LoginRequestDto
{
    /// <summary>Gets or sets the username.</summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>Gets or sets the password.</summary>
    public string Password { get; set; } = string.Empty;
}
