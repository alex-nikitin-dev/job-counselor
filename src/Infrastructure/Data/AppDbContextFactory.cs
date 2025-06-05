using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JobCounselor.Infrastructure.Data;

/// <summary>
/// Provides design-time DbContext creation for EF Core tooling.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=jobcounselor.db");
        return new AppDbContext(optionsBuilder.Options);
    }
}
