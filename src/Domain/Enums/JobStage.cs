namespace JobCounselor.Domain.Enums;

/// <summary>
/// Defines the stage of a job application process.
/// </summary>
public enum JobStage
{
    Draft = 0,
    Applied = 1,
    Interviewing = 2,
    Offered = 3,
    Hired = 4,
    Rejected = 5
}
