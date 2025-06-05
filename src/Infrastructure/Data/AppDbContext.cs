using System.Reflection;
using JobCounselor.Domain.Entities;
using JobCounselor.Domain.ValueObjects;
using JobCounselor.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobCounselor.Infrastructure.Data;

/// <summary>
/// Entity Framework Core database context for the application.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Creates a new <see cref="AppDbContext"/> using the given options.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSets for aggregate roots
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Resume> Resumes => Set<Resume>();
    public DbSet<CoverLetter> CoverLetters => Set<CoverLetter>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();

    /// <summary>
    /// Configures the model by mapping domain entities to database tables
    /// and applying value converters/owned types where necessary.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations defined in this assembly via the fluent API.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure enum conversions.
        var jobStageConverter = new EnumToStringConverter<JobStage>();
        modelBuilder.Entity<JobPosting>()
            .Property(j => j.Stage)
            .HasConversion(jobStageConverter);

        // Map owned collections for Profile entity.
        modelBuilder.Entity<Profile>(builder =>
        {
            builder.HasKey(p => p.Id);

            // Map skills collection stored in a separate table using the private backing field.
            builder.OwnsMany<Skill>(
                nameof(Profile.Skills),
                sb =>
                {
                    sb.WithOwner().HasForeignKey("ProfileId");
                    sb.Property<Guid>("Id");
                    sb.HasKey("Id");
                    sb.ToTable("ProfileSkills");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Education collection
            builder.OwnsMany<EducationItem>(
                nameof(Profile.Education),
                eb =>
                {
                    eb.WithOwner().HasForeignKey("ProfileId");
                    eb.Property<Guid>("Id");
                    eb.HasKey("Id");
                    eb.ToTable("ProfileEducation");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Experience collection
            builder.OwnsMany<ExperienceItem>(
                nameof(Profile.Experience),
                exb =>
                {
                    exb.WithOwner().HasForeignKey("ProfileId");
                    exb.Property<Guid>("Id");
                    exb.HasKey("Id");
                    exb.ToTable("ProfileExperience");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Languages collection
            builder.OwnsMany<Language>(
                nameof(Profile.Languages),
                lb =>
                {
                    lb.WithOwner().HasForeignKey("ProfileId");
                    lb.Property<Guid>("Id");
                    lb.HasKey("Id");
                    lb.ToTable("ProfileLanguages");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        // Map owned collections for Resume entity in a similar fashion.
        modelBuilder.Entity<Resume>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.OwnsMany<Skill>(
                nameof(Resume.Skills),
                sb =>
                {
                    sb.WithOwner().HasForeignKey("ResumeId");
                    sb.Property<Guid>("Id");
                    sb.HasKey("Id");
                    sb.ToTable("ResumeSkills");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany<EducationItem>(
                nameof(Resume.Education),
                eb =>
                {
                    eb.WithOwner().HasForeignKey("ResumeId");
                    eb.Property<Guid>("Id");
                    eb.HasKey("Id");
                    eb.ToTable("ResumeEducation");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany<ExperienceItem>(
                nameof(Resume.Experience),
                exb =>
                {
                    exb.WithOwner().HasForeignKey("ResumeId");
                    exb.Property<Guid>("Id");
                    exb.HasKey("Id");
                    exb.ToTable("ResumeExperience");
                })
                .Navigation
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
