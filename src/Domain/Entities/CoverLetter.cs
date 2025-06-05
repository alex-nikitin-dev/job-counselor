namespace JobCounselor.Domain.Entities;

/// <summary>
/// Represents a cover letter tailored to a job posting.
/// </summary>
public class CoverLetter
{
    public Guid Id { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid JobPostingId { get; private set; }
    public string Content { get; private set; }

    public CoverLetter(Guid id, Guid profileId, Guid jobPostingId, string content)
    {
        Id = id;
        ProfileId = profileId;
        JobPostingId = jobPostingId;
        Content = content;
    }
}
