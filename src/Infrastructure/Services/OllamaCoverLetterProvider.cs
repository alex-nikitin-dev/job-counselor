using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace JobCounselor.Infrastructure.Services;

/// <summary>
/// Default implementation of <see cref="IAiCoverLetterProvider"/> using a local
/// Ollama API instance.
/// </summary>
public class OllamaCoverLetterProvider : IAiCoverLetterProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OllamaCoverLetterProvider> _logger;
    private const string Endpoint = "http://ollama:11434/api/generate";

    /// <summary>
    /// Initializes the provider with the supplied <see cref="HttpClient"/> and logger.
    /// </summary>
    public OllamaCoverLetterProvider(HttpClient httpClient, ILogger<OllamaCoverLetterProvider> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken)
    {
        var request = new { model = "llama3", prompt };

        using var response = await _httpClient.PostAsJsonAsync(Endpoint, request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: cancellationToken);
        var text = result?.Response ?? string.Empty;
        _logger.LogInformation("Generated cover letter {Length} characters", text.Length);
        return text;
    }

    private sealed class OllamaResponse
    {
        public string Response { get; set; } = string.Empty;
    }
}
