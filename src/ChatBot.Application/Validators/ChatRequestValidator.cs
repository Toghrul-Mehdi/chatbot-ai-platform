using ChatBot.Application.DTOs.Chat;
using FluentValidation;

namespace ChatBot.Application.Validators;

/// <summary>
/// Validator for <see cref="ChatRequestDto"/>.
/// </summary>
public class ChatRequestValidator : AbstractValidator<ChatRequestDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRequestValidator"/> class.
    /// </summary>
    public ChatRequestValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(4000).WithMessage("Message must not exceed 4000 characters.");
    }
}
