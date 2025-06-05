using JobCounselor.Domain.Entities;

namespace JobCounselor.Application.Interfaces;

/// <summary>
/// Provides functionality to generate cover letters for job postings.
/// </summary>
public interface ICoverLetterService
{
    CoverLetter GenerateCoverLetter(Profile profile, JobPosting posting);
}
