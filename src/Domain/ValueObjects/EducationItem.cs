namespace JobCounselor.Domain.ValueObjects;

/// <summary>
/// Represents an education entry on a profile or resume.
/// </summary>
public sealed record EducationItem
{
    public string Degree { get; }
    public string Institution { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }

    /// <summary>
    /// Creates a new <see cref="EducationItem"/> instance.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when values are invalid.</exception>
    public EducationItem(string degree, string institution, DateTime startDate, DateTime? endDate)
    {
        if (string.IsNullOrWhiteSpace(degree))
        {
            throw new ArgumentException("Degree is required", nameof(degree));
        }
        if (string.IsNullOrWhiteSpace(institution))
        {
            throw new ArgumentException("Institution is required", nameof(institution));
        }
        if (endDate.HasValue && endDate.Value < startDate)
        {
            throw new ArgumentException("End date cannot be before start date", nameof(endDate));
        }

        Degree = degree;
        Institution = institution;
        StartDate = startDate;
        EndDate = endDate;
    }
}
