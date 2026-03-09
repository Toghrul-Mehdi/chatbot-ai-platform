using System.Security.Claims;
using ChatBot.Application.DTOs.Chat;
using ChatBot.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers;

/// <summary>
/// Controller for chat operations with the AI service.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IValidator<ChatRequestDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatController"/> class.
    /// </summary>
    public ChatController(IChatService chatService, IValidator<ChatRequestDto> validator)
    {
        _chatService = chatService;
        _validator = validator;
    }

    /// <summary>
    /// Sends a message to the AI and returns the response.
    /// </summary>
    /// <param name="request">The chat request containing the message and optional history.</param>
    /// <returns>The AI response with session information.</returns>
    [HttpPost("ask")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Ask([FromBody] ChatRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var result = await _chatService.AskAsync(request, userId);

        if (!result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = result.Error });
        }

        return Ok(result.Data);
    }
}
