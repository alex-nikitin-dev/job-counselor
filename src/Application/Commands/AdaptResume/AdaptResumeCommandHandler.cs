using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.AdaptResume;

/// <summary>
/// Uses <see cref="IResumeService"/> to adapt a resume to a job posting.
/// </summary>
public class AdaptResumeCommandHandler : IRequestHandler<AdaptResumeCommand, Resume>
{
    private readonly IResumeService _service;

    public AdaptResumeCommandHandler(IResumeService service)
    {
        _service = service;
    }

    public Task<Resume> Handle(AdaptResumeCommand request, CancellationToken cancellationToken)
    {
        // Adaptation is synchronous for simplicity.
        var adapted = _service.AdaptResume(request.Resume, request.Posting);
        return Task.FromResult(adapted);
    }
}
