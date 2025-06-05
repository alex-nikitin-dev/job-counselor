using FluentValidation;
using JobCounselor.Application.Commands.GenerateBaseResume;

namespace JobCounselor.Application.Validators;

/// <summary>
/// Validates <see cref="GenerateBaseResumeCommand"/>.
/// </summary>
public class GenerateBaseResumeCommandValidator : AbstractValidator<GenerateBaseResumeCommand>
{
    public GenerateBaseResumeCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty();
    }
}
