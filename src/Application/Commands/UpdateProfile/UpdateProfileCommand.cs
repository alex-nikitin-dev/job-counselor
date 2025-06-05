using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.UpdateProfile;

/// <summary>
/// Command to update an existing profile.
/// </summary>
public record UpdateProfileCommand(
    Guid ProfileId,
    string FullName,
    string Email,
    string Phone,
    string Summary) : IRequest<Profile>;
