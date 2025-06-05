using FluentValidation;
using JobCounselor.Application.Commands.UpdateProfile;

namespace JobCounselor.Application.Validators;

/// <summary>
/// Validates <see cref="UpdateProfileCommand"/>.
/// </summary>
public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty();
    }
}
