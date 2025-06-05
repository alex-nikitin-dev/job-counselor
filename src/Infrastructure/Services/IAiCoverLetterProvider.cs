namespace JobCounselor.Infrastructure.Services;

/// <summary>
/// Abstraction for providers capable of generating cover letter text
/// using an AI model.
/// </summary>
public interface IAiCoverLetterProvider
{
    /// <summary>
    /// Generates cover letter text from the supplied prompt.
    /// </summary>
    /// <param name="prompt">Prompt describing the cover letter to create.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The generated cover letter text.</returns>
    Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken);
}
