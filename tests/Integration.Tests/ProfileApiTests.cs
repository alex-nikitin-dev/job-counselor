using System.Net;
using JobCounselor.Application.Commands.CreateProfile;
using JobCounselor.Application.Commands.UpdateProfile;
using Xunit;

namespace JobCounselor.Integration.Tests;

/// <summary>
/// Integration tests exercising the minimal API endpoints using a real
/// PostgreSQL database and in-memory authentication.
/// </summary>
public class ProfileApiTests : IClassFixture<PostgresWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProfileApiTests(PostgresWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Creating a new profile should return a successful HTTP status code.
    /// </summary>
    [Fact]
    public async Task Post_Profile_ReturnsSuccess()
    {
        var command = new CreateProfileCommand("Alice", "alice@example.com", "555", "summary");

        var response = await _client.PostAsJsonAsync("/api/v1/profiles", command);

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Updating a non-existent profile results in a server error from the API.
    /// </summary>
    [Fact]
    public async Task Put_ProfileMissing_ReturnsServerError()
    {
        var id = Guid.NewGuid();
        var command = new UpdateProfileCommand(id, "Bob", "bob@example.com", "555", "summary");

        var response = await _client.PutAsJsonAsync($"/api/v1/profiles/{id}", command);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
