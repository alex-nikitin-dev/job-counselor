using JobCounselor.Application.Commands.GenerateBaseResume;
using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using Moq;
using Xunit;

namespace JobCounselor.Application.Tests.Commands;

/// <summary>
/// Unit tests for <see cref="GenerateBaseResumeCommandHandler"/> covering
/// successful resume generation and missing profile scenarios.
/// </summary>
public class GenerateBaseResumeCommandHandlerTests
{
    /// <summary>
    /// When the requested profile exists the resume service should be
    /// invoked and its result returned.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileFound_GeneratesResume()
    {
        var id = Guid.NewGuid();
        var profile = new Profile(id, "name", "e@e", "1", "sum");
        var resume = new Resume(Guid.NewGuid(), id, "content");
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(profile);
        var service = new Mock<IResumeService>();
        service.Setup(s => s.GenerateBaseResume(profile)).Returns(resume);
        var handler = new GenerateBaseResumeCommandHandler(repo.Object, service.Object);
        var command = new GenerateBaseResumeCommand(id);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(resume, result);
    }

    /// <summary>
    /// If the profile cannot be found the handler should throw an
    /// <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileMissing_Throws()
    {
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Profile?)null);
        var handler = new GenerateBaseResumeCommandHandler(repo.Object, Mock.Of<IResumeService>());
        var command = new GenerateBaseResumeCommand(Guid.NewGuid());

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
