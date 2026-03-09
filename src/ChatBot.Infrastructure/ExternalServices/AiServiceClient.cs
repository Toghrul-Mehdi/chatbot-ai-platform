using System.Net.Http.Headers;
using ChatBot.Application.DTOs.Chat;
using ChatBot.Application.Interfaces;
using ChatBot.Shared;
using ChatBot.Shared.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChatBot.Infrastructure.ExternalServices;

/// <summary>
/// HTTP client for communicating with the Python AI service.
/// </summary>
public class AiServiceClient : IAiServiceClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AiServiceClient> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AiServiceClient"/> class.
    /// </summary>
    public AiServiceClient(IHttpClientFactory httpClientFactory, ILogger<AiServiceClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result<ChatResponseDto>> AskAsync(string message, List<HistoryItemDto> history)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(AppConstants.AiServiceHttpClient);

            var requestBody = new
            {
                message,
                history = history.Select(h => new { role = h.Role, content = h.Content }).ToList()
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            _logger.LogInformation("Sending request to AI service: {Message}", message);

            var response = await client.PostAsync("/chat/ask", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("AI service returned {StatusCode}: {Body}", response.StatusCode, errorBody);
                return Result<ChatResponseDto>.Failure($"AI service returned {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var aiResponse = JsonConvert.DeserializeAnonymousType(responseJson, new { question = "", answer = "" });

            if (aiResponse == null)
            {
                return Result<ChatResponseDto>.Failure("Failed to parse AI service response.");
            }

            return Result<ChatResponseDto>.Success(new ChatResponseDto
            {
                Question = aiResponse.question,
                Answer = aiResponse.answer,
                Timestamp = DateTime.UtcNow
            });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to communicate with AI service");
            return Result<ChatResponseDto>.Failure("AI service is unavailable. Please try again later.");
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "AI service request timed out");
            return Result<ChatResponseDto>.Failure("AI service request timed out.");
        }
    }

    /// <inheritdoc />
    public async Task<bool> CheckHealthAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient(AppConstants.AiServiceHttpClient);
            var response = await client.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI service health check failed");
            return false;
        }
    }
}
