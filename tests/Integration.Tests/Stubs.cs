using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;

namespace JobCounselor.Integration.Tests;

/// <summary>
/// Lightweight implementation of <see cref="IResumeService"/> used only during
/// integration tests. It returns basic placeholder objects so that the API can
/// execute command handlers without relying on AI services.
/// </summary>
internal sealed class StubResumeService : IResumeService
{
    public Resume GenerateBaseResume(Profile profile)
        => new(Guid.NewGuid(), profile.Id, "stub resume");

    public Resume AdaptResume(Resume resume, JobPosting posting)
        => resume;
}

/// <summary>
/// Minimal implementation of <see cref="ICoverLetterService"/> for integration
/// tests. It fabricates a cover letter without performing any external work.
/// </summary>
internal sealed class StubCoverLetterService : ICoverLetterService
{
    public CoverLetter GenerateCoverLetter(Profile profile, JobPosting posting)
        => new(Guid.NewGuid(), profile.Id, posting.Id, "stub cover letter");
}
