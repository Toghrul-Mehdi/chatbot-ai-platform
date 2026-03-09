using ChatBot.Application.DTOs.Chat;
using ChatBot.Application.Interfaces;
using ChatBot.Domain.Entities;
using ChatBot.Domain.Enums;
using ChatBot.Shared;
using Microsoft.Extensions.Logging;

namespace ChatBot.Application.Services;

/// <summary>
/// Orchestrates chat operations including AI service calls and session management.
/// </summary>
public class ChatService : IChatService
{
    private readonly IAiServiceClient _aiServiceClient;
    private readonly IChatSessionRepository _sessionRepository;
    private readonly ILogger<ChatService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatService"/> class.
    /// </summary>
    public ChatService(
        IAiServiceClient aiServiceClient,
        IChatSessionRepository sessionRepository,
        ILogger<ChatService> logger)
    {
        _aiServiceClient = aiServiceClient;
        _sessionRepository = sessionRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result<ChatResponseDto>> AskAsync(ChatRequestDto request, Guid userId)
    {
        _logger.LogInformation("Processing chat request for user {UserId}", userId);

        // Create or get session
        var session = new ChatSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Status = SessionStatus.Active
        };

        // Add user message
        session.Messages.Add(new ChatMessage
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Role = MessageRole.User,
            Content = request.Message,
            Timestamp = DateTime.UtcNow
        });

        // Call AI service
        var aiResult = await _aiServiceClient.AskAsync(request.Message, request.History);
        if (!aiResult.IsSuccess)
        {
            _logger.LogWarning("AI service call failed: {Error}", aiResult.Error);
            return Result<ChatResponseDto>.Failure(aiResult.Error!);
        }

        // Add assistant message
        session.Messages.Add(new ChatMessage
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Role = MessageRole.Assistant,
            Content = aiResult.Data!.Answer,
            Timestamp = DateTime.UtcNow
        });

        // Save session
        await _sessionRepository.AddAsync(session);

        var response = new ChatResponseDto
        {
            Question = request.Message,
            Answer = aiResult.Data!.Answer,
            SessionId = session.Id.ToString(),
            Timestamp = DateTime.UtcNow
        };

        _logger.LogInformation("Chat request processed successfully for session {SessionId}", session.Id);
        return Result<ChatResponseDto>.Success(response);
    }
}
