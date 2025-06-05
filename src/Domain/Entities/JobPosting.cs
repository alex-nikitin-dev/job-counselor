using JobCounselor.Domain.Enums;

namespace JobCounselor.Domain.Entities;

/// <summary>
/// Represents a job posting for which a candidate may apply.
/// </summary>
public class JobPosting
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Company { get; private set; }
    public string Description { get; private set; }
    public JobStage Stage { get; private set; }

    public JobPosting(Guid id, string title, string company, string description)
    {
        Id = id;
        Title = title;
        Company = company;
        Description = description;
        Stage = JobStage.Draft;
    }

    /// <summary>
    /// Updates the job stage.
    /// </summary>
    public void MoveToStage(JobStage stage) => Stage = stage;
}
