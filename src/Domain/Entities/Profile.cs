using JobCounselor.Domain.ValueObjects;

namespace JobCounselor.Domain.Entities;

/// <summary>
/// Represents a job seeker's profile containing personal information and skills.
/// </summary>
public class Profile
{
    /// <summary>
    /// Database identifier.
    /// </summary>
    public Guid Id { get; private set; }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Summary { get; private set; }

    private readonly List<Skill> _skills = new();
    private readonly List<EducationItem> _education = new();
    private readonly List<ExperienceItem> _experience = new();
    private readonly List<Language> _languages = new();

    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<EducationItem> Education => _education.AsReadOnly();
    public IReadOnlyCollection<ExperienceItem> Experience => _experience.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();

    public Profile(Guid id, string fullName, string email, string phone, string summary)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        Summary = summary;
    }

    /// <summary>
    /// Adds a skill to the profile.
    /// </summary>
    public void AddSkill(Skill skill) => _skills.Add(skill);

    /// <summary>
    /// Adds an education item to the profile.
    /// </summary>
    public void AddEducation(EducationItem item) => _education.Add(item);

    /// <summary>
    /// Adds an experience item to the profile.
    /// </summary>
    public void AddExperience(ExperienceItem item) => _experience.Add(item);

    /// <summary>
    /// Adds a language to the profile.
    /// </summary>
    public void AddLanguage(Language language) => _languages.Add(language);
}
