namespace JobCounselor.Domain.ValueObjects;

/// <summary>
/// Represents a professional skill.
/// </summary>
public sealed record Skill
{
    public string Name { get; }
    public string Level { get; }

    // Parameterless constructor for EF Core.
    private Skill()
    {
        Name = string.Empty;
        Level = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Skill"/> instance.
    /// </summary>
    /// <param name="name">Name of the skill.</param>
    /// <param name="level">Proficiency level.</param>
    /// <exception cref="ArgumentException">Thrown when name is null or empty.</exception>
    public Skill(string name, string level)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Skill name cannot be empty", nameof(name));
        }

        Name = name;
        Level = level;
    }
}
