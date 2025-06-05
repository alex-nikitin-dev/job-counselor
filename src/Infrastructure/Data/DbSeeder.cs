using JobCounselor.Domain.Entities;
using JobCounselor.Domain.Enums;
using JobCounselor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace JobCounselor.Infrastructure.Data;

/// <summary>
/// Utility class that seeds the database with sample data during development.
/// </summary>
public class DbSeeder
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Creates a new <see cref="DbSeeder"/> instance.
    /// </summary>
    public DbSeeder(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Ensures the database is created and populated with sample data.
    /// </summary>
    public async Task SeedAsync()
    {
        // Apply pending migrations if using a relational database provider.
        await _db.Database.MigrateAsync();

        if (await _db.Profiles.AnyAsync() || await _db.JobPostings.AnyAsync())
        {
            return; // Already seeded
        }

        var profiles = new[]
        {
            CreateProfile("Alice Smith", "alice@example.com"),
            CreateProfile("Bob Johnson", "bob@example.com"),
            CreateProfile("Carol Davis", "carol@example.com")
        };

        await _db.Profiles.AddRangeAsync(profiles);

        var jobs = new[]
        {
            CreateJob("Backend Developer", JobStage.Draft),
            CreateJob("QA Engineer", JobStage.Applied),
            CreateJob("Project Manager", JobStage.Interviewing),
            CreateJob("DevOps Specialist", JobStage.Offered),
            CreateJob("UX Designer", JobStage.Rejected)
        };

        await _db.JobPostings.AddRangeAsync(jobs);

        await _db.SaveChangesAsync();
    }

    private static Profile CreateProfile(string name, string email)
    {
        var profile = new Profile(Guid.NewGuid(), name, email, "555-1234", "Sample profile");
        profile.AddSkill(new Skill("C#", "Advanced"));
        profile.AddEducation(new EducationItem("BSc CS", "State University", DateTime.UtcNow.AddYears(-5), DateTime.UtcNow.AddYears(-1)));
        profile.AddExperience(new ExperienceItem("Developer", "SomeCorp", DateTime.UtcNow.AddYears(-1), null, "Worked on projects"));
        profile.AddLanguage(new Language("English", "Native"));
        return profile;
    }

    private static JobPosting CreateJob(string title, JobStage stage)
    {
        var job = new JobPosting(Guid.NewGuid(), title, "Acme", "Sample job");
        job.MoveToStage(stage);
        return job;
    }
}
