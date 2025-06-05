using FluentValidation;
using JobCounselor.Application.Commands.GenerateCoverLetter;

namespace JobCounselor.Application.Validators;

/// <summary>
/// Validates <see cref="GenerateCoverLetterCommand"/>.
/// </summary>
public class GenerateCoverLetterCommandValidator : AbstractValidator<GenerateCoverLetterCommand>
{
    public GenerateCoverLetterCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty();
        RuleFor(x => x.Posting).NotNull();
    }
}
