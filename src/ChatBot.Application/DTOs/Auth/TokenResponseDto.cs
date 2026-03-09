namespace ChatBot.Application.DTOs.Auth;

/// <summary>
/// Response DTO containing an access token.
/// </summary>
public class TokenResponseDto
{
    /// <summary>Gets or sets the JWT access token.</summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>Gets or sets the token expiration time.</summary>
    public DateTime ExpiresAt { get; set; }
}
