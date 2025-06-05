using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.CreateProfile;

/// <summary>
/// Command to create a new profile.
/// </summary>
public record CreateProfileCommand(
    string FullName,
    string Email,
    string Phone,
    string Summary) : IRequest<Profile>;
