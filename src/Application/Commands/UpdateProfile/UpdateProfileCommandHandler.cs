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

        // Use domain method to update details
        profile.UpdateDetails(request.FullName, request.Email, request.Phone, request.Summary);

        await _repository.UpdateAsync(profile, cancellationToken);
        return profile;
    }
}
