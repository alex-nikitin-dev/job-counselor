using JobCounselor.Application.Commands.UpdateProfile;
using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using Moq;
using Xunit;

namespace JobCounselor.Application.Tests.Commands;

/// <summary>
/// Tests for <see cref="UpdateProfileCommandHandler"/> ensuring
/// existing profiles are updated and missing profiles trigger errors.
/// </summary>
public class UpdateProfileCommandHandlerTests
{
    /// <summary>
    /// When the profile exists the handler should update it and
    /// return the modified instance.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileExists_UpdatesProfile()
    {
        var id = Guid.NewGuid();
        var profile = new Profile(id, "Old", "old@example.com", "1", "old");
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(profile);
        var handler = new UpdateProfileCommandHandler(repo.Object);
        var command = new UpdateProfileCommand(id, "New", "new@example.com", "2", "new");

        var result = await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.UpdateAsync(profile, It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal("New", result.FullName);
    }

    /// <summary>
    /// If the repository cannot locate the profile an exception
    /// should be thrown by the handler.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileMissing_Throws()
    {
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Profile?)null);
        var handler = new UpdateProfileCommandHandler(repo.Object);
        var command = new UpdateProfileCommand(Guid.NewGuid(), "x", "x@x", "x", "x");

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
