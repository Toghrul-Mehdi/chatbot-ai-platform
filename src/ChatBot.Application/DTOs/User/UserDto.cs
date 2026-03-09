namespace ChatBot.Application.DTOs.User;

/// <summary>
/// DTO for user information.
/// </summary>
public class UserDto
{
    /// <summary>Gets or sets the user identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the username.</summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>Gets or sets the email address.</summary>
    public string Email { get; set; } = string.Empty;
}
