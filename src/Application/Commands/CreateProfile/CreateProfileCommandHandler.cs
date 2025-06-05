using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.CreateProfile;

/// <summary>
/// Handles creation of new <see cref="Profile"/> instances.
/// </summary>
public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Profile>
{
    private readonly IProfileRepository _repository;

    public CreateProfileCommandHandler(IProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<Profile> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = new Profile(Guid.NewGuid(), request.FullName, request.Email, request.Phone, request.Summary);

        await _repository.AddAsync(profile, cancellationToken);

        return profile;
    }
}
