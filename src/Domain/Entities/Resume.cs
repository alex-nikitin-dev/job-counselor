using JobCounselor.Domain.ValueObjects;

namespace JobCounselor.Domain.Entities;

/// <summary>
/// Represents a resume document generated from a profile.
/// </summary>
public class Resume
{
    public Guid Id { get; private set; }
    public Guid ProfileId { get; private set; }
    public string Content { get; private set; }

    private readonly List<Skill> _skills = new();
    private readonly List<EducationItem> _education = new();
    private readonly List<ExperienceItem> _experience = new();

    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<EducationItem> Education => _education.AsReadOnly();
    public IReadOnlyCollection<ExperienceItem> Experience => _experience.AsReadOnly();

    public Resume(Guid id, Guid profileId, string content)
    {
        Id = id;
        ProfileId = profileId;
        Content = content;
    }

    public void AddSkill(Skill skill) => _skills.Add(skill);
    public void AddEducation(EducationItem item) => _education.Add(item);
    public void AddExperience(ExperienceItem item) => _experience.Add(item);
}
