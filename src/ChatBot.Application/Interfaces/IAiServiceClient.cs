using ChatBot.Application.DTOs.Chat;
using ChatBot.Shared;

namespace ChatBot.Application.Interfaces;

/// <summary>
/// Interface for communicating with the Python AI service.
/// </summary>
public interface IAiServiceClient
{
    /// <summary>
    /// Sends a chat message to the AI service and returns the response.
    /// </summary>
    /// <param name="message">The user message.</param>
    /// <param name="history">The conversation history.</param>
    /// <returns>A result containing the AI response.</returns>
    Task<Result<ChatResponseDto>> AskAsync(string message, List<HistoryItemDto> history);

    /// <summary>
    /// Checks the health of the AI service.
    /// </summary>
    /// <returns>True if the service is reachable; otherwise, false.</returns>
    Task<bool> CheckHealthAsync();
}
