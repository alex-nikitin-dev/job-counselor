using JobCounselor.Application.Commands.AdaptResume;
using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using JobCounselor.Domain.Enums;
using Moq;
using Xunit;

namespace JobCounselor.Application.Tests.Commands;

/// <summary>
/// Tests for <see cref="AdaptResumeCommandHandler"/> verifying that the
/// resume service is invoked correctly and exceptions are propagated.
/// </summary>
public class AdaptResumeCommandHandlerTests
{
    /// <summary>
    /// Successful adaptation should return the resume produced by the service.
    /// </summary>
    [Fact]
    public async Task Handle_AdaptsResume()
    {
        var resume = new Resume(Guid.NewGuid(), Guid.NewGuid(), "orig");
        var posting = new JobPosting(Guid.NewGuid(), "title", "comp", "desc");
        posting.MoveToStage(JobStage.Applied);
        var adapted = new Resume(Guid.NewGuid(), resume.ProfileId, "adapted");
        var service = new Mock<IResumeService>();
        service.Setup(s => s.AdaptResume(resume, posting)).Returns(adapted);
        var handler = new AdaptResumeCommandHandler(service.Object);
        var command = new AdaptResumeCommand(resume, posting);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(adapted, result);
    }

    /// <summary>
    /// Any exceptions thrown by the service should be rethrown by the handler.
    /// </summary>
    [Fact]
    public async Task Handle_ServiceThrows_PropagatesException()
    {
        var resume = new Resume(Guid.NewGuid(), Guid.NewGuid(), "orig");
        var posting = new JobPosting(Guid.NewGuid(), "title", "comp", "desc");
        var service = new Mock<IResumeService>();
        service.Setup(s => s.AdaptResume(resume, posting)).Throws(new InvalidOperationException());
        var handler = new AdaptResumeCommandHandler(service.Object);
        var command = new AdaptResumeCommand(resume, posting);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
