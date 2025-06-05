using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.AdaptResume;

/// <summary>
/// Adapts an existing resume to a specific job posting.
/// </summary>
public record AdaptResumeCommand(Resume Resume, JobPosting Posting) : IRequest<Resume>;
