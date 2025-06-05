using FluentValidation;
using JobCounselor.Application.Commands.AdaptResume;

namespace JobCounselor.Application.Validators;

/// <summary>
/// Validates <see cref="AdaptResumeCommand"/>.
/// </summary>
public class AdaptResumeCommandValidator : AbstractValidator<AdaptResumeCommand>
{
    public AdaptResumeCommandValidator()
    {
        RuleFor(x => x.Resume).NotNull();
        RuleFor(x => x.Posting).NotNull();
    }
}
