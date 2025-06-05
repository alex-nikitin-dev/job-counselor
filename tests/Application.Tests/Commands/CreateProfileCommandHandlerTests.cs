using JobCounselor.Application.Commands.CreateProfile;
using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using Moq;
using Xunit;

namespace JobCounselor.Application.Tests.Commands;

/// <summary>
/// Unit tests for <see cref="CreateProfileCommandHandler"/>.
/// These tests exercise the happy path and error propagation
/// using mocked repository dependencies.
/// </summary>
public class CreateProfileCommandHandlerTests
{
    /// <summary>
    /// Verifies that a new profile is persisted using the repository
    /// and the created instance is returned from the handler.
    /// </summary>
    [Fact]
    public async Task Handle_CreatesProfile()
    {
        var repo = new Mock<IProfileRepository>();
        var handler = new CreateProfileCommandHandler(repo.Object);
        var command = new CreateProfileCommand("John Doe", "john@example.com", "555", "summary");

        var result = await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal("John Doe", result.FullName);
    }

    /// <summary>
    /// Ensures that exceptions thrown by the repository are surfaced
    /// to the caller of the handler.
    /// </summary>
    [Fact]
    public async Task Handle_RepositoryThrows_PropagatesException()
    {
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("failure"));
        var handler = new CreateProfileCommandHandler(repo.Object);
        var command = new CreateProfileCommand("Jane", "jane@example.com", "555", "summary");

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
