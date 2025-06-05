using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.GenerateCoverLetter;

/// <summary>
/// Generates a cover letter for a specific job posting.
/// </summary>
public record GenerateCoverLetterCommand(Guid ProfileId, JobPosting Posting) : IRequest<CoverLetter>;
