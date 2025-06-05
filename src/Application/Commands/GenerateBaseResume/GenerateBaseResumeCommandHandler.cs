using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.GenerateBaseResume;

/// <summary>
/// Handles generation of base resumes from profiles.
/// </summary>
public class GenerateBaseResumeCommandHandler : IRequestHandler<GenerateBaseResumeCommand, Resume>
{
    private readonly IProfileRepository _repository;
    private readonly IResumeService _resumeService;

    public GenerateBaseResumeCommandHandler(IProfileRepository repository, IResumeService resumeService)
    {
        _repository = repository;
        _resumeService = resumeService;
    }

    public async Task<Resume> Handle(GenerateBaseResumeCommand request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByIdAsync(request.ProfileId, cancellationToken) ??
                       throw new InvalidOperationException("Profile not found");

        var resume = _resumeService.GenerateBaseResume(profile);
        return resume;
    }
}
