namespace JobCounselor.Domain.ValueObjects;

/// <summary>
/// Represents a language proficiency.
/// </summary>
public sealed record Language
{
    public string Name { get; }
    public string Proficiency { get; }

    // Parameterless constructor for EF Core.
    private Language()
    {
        Name = string.Empty;
        Proficiency = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Language"/> instance.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when name is invalid.</exception>
    public Language(string name, string proficiency)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Language name is required", nameof(name));
        }

        Name = name;
        Proficiency = proficiency;
    }
}
