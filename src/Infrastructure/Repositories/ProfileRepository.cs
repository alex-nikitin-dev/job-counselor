using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using JobCounselor.Infrastructure.Data;

namespace JobCounselor.Infrastructure.Repositories;

/// <summary>
/// Repository providing data access for <see cref="Profile"/> entities.
/// </summary>
public class ProfileRepository : EfRepository<Profile>, IProfileRepository
{
    /// <summary>
    /// Creates a new <see cref="ProfileRepository"/> instance.
    /// </summary>
    public ProfileRepository(AppDbContext context) : base(context)
    {
    }
}
