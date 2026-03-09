using ChatBot.Application.DTOs.Chat;
using ChatBot.Shared;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for chat business logic.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Processes a chat message and returns the AI response.
    /// </summary>
    /// <param name="request">The chat request DTO.</param>
    /// <param name="userId">The authenticated user's identifier.</param>
    /// <returns>A result containing the chat response.</returns>
    Task<Result<ChatResponseDto>> AskAsync(ChatRequestDto request, Guid userId);
}
