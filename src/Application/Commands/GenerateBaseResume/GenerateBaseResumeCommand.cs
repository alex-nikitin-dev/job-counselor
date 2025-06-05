using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.GenerateBaseResume;

/// <summary>
/// Generates a base resume from a profile.
/// </summary>
public record GenerateBaseResumeCommand(Guid ProfileId) : IRequest<Resume>;
