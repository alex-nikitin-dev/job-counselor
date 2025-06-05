using JobCounselor.Domain.Entities;

namespace JobCounselor.Application.Interfaces;

/// <summary>
/// Provides resume generation capabilities.
/// </summary>
public interface IResumeService
{
    Resume GenerateBaseResume(Profile profile);
    Resume AdaptResume(Resume resume, JobPosting posting);
}
