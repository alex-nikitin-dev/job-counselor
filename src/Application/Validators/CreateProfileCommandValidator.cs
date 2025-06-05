using FluentValidation;
using JobCounselor.Application.Commands.CreateProfile;

namespace JobCounselor.Application.Validators;

/// <summary>
/// Validates <see cref="CreateProfileCommand"/>.
/// </summary>
public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty();
    }
}
