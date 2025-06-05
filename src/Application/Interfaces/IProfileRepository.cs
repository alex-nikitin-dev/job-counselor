using JobCounselor.Domain.Entities;

namespace JobCounselor.Application.Interfaces;

/// <summary>
/// Repository abstraction for persisting <see cref="Profile"/> entities.
/// </summary>
public interface IProfileRepository
{
    Task AddAsync(Profile profile, CancellationToken cancellationToken);
    Task UpdateAsync(Profile profile, CancellationToken cancellationToken);
    Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
