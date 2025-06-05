using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.UpdateProfile;

/// <summary>
/// Handles updating an existing <see cref="Profile"/>.
/// </summary>
public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Profile>
{
    private readonly IProfileRepository _repository;

    public UpdateProfileCommandHandler(IProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<Profile> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByIdAsync(request.ProfileId, cancellationToken) ??
                       throw new InvalidOperationException("Profile not found");

        // Update basic fields
        typeof(Profile).GetProperty("FullName")!.SetValue(profile, request.FullName);
        typeof(Profile).GetProperty("Email")!.SetValue(profile, request.Email);
        typeof(Profile).GetProperty("Phone")!.SetValue(profile, request.Phone);
        typeof(Profile).GetProperty("Summary")!.SetValue(profile, request.Summary);

        await _repository.UpdateAsync(profile, cancellationToken);
        return profile;
    }
}
