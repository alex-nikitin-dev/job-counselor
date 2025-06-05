namespace JobCounselor.Domain.ValueObjects;

/// <summary>
/// Represents a professional experience entry.
/// </summary>
public sealed record ExperienceItem
{
    public string Title { get; }
    public string Company { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }
    public string Description { get; }

    // Parameterless constructor for EF Core.
    private ExperienceItem()
    {
        Title = string.Empty;
        Company = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="ExperienceItem"/> instance.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when values are invalid.</exception>
    public ExperienceItem(string title, string company, DateTime startDate, DateTime? endDate, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required", nameof(title));
        }
        if (string.IsNullOrWhiteSpace(company))
        {
            throw new ArgumentException("Company is required", nameof(company));
        }
        if (endDate.HasValue && endDate.Value < startDate)
        {
            throw new ArgumentException("End date cannot be before start date", nameof(endDate));
        }

        Title = title;
        Company = company;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }
}
