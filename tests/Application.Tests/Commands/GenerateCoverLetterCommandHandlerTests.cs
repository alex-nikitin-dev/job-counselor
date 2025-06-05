using JobCounselor.Application.Commands.GenerateCoverLetter;
using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using JobCounselor.Domain.Enums;
using Moq;
using Xunit;

namespace JobCounselor.Application.Tests.Commands;

/// <summary>
/// Tests for <see cref="GenerateCoverLetterCommandHandler"/> verifying
/// service interaction and error handling when a profile is missing.
/// </summary>
public class GenerateCoverLetterCommandHandlerTests
{
    /// <summary>
    /// When the profile exists the cover letter service should be
    /// called and its result returned.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileFound_GeneratesLetter()
    {
        var id = Guid.NewGuid();
        var profile = new Profile(id, "name", "e@e", "1", "sum");
        var posting = new JobPosting(Guid.NewGuid(), "title", "comp", "desc");
        posting.MoveToStage(JobStage.Applied);
        var letter = new CoverLetter(Guid.NewGuid(), id, posting.Id, "body");
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(profile);
        var service = new Mock<ICoverLetterService>();
        service.Setup(s => s.GenerateCoverLetter(profile, posting)).Returns(letter);
        var handler = new GenerateCoverLetterCommandHandler(repo.Object, service.Object);
        var command = new GenerateCoverLetterCommand(id, posting);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(letter, result);
    }

    /// <summary>
    /// If the profile cannot be located the handler should throw an exception.
    /// </summary>
    [Fact]
    public async Task Handle_ProfileMissing_Throws()
    {
        var posting = new JobPosting(Guid.NewGuid(), "title", "comp", "desc");
        var repo = new Mock<IProfileRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Profile?)null);
        var handler = new GenerateCoverLetterCommandHandler(repo.Object, Mock.Of<ICoverLetterService>());
        var command = new GenerateCoverLetterCommand(Guid.NewGuid(), posting);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
