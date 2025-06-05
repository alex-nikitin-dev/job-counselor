using JobCounselor.Application.Interfaces;
using JobCounselor.Domain.Entities;
using MediatR;

namespace JobCounselor.Application.Commands.GenerateCoverLetter;

/// <summary>
/// Uses <see cref="ICoverLetterService"/> to generate a cover letter.
/// </summary>
public class GenerateCoverLetterCommandHandler : IRequestHandler<GenerateCoverLetterCommand, CoverLetter>
{
    private readonly IProfileRepository _repository;
    private readonly ICoverLetterService _service;

    public GenerateCoverLetterCommandHandler(IProfileRepository repository, ICoverLetterService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<CoverLetter> Handle(GenerateCoverLetterCommand request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByIdAsync(request.ProfileId, cancellationToken) ??
                       throw new InvalidOperationException("Profile not found");
        var letter = _service.GenerateCoverLetter(profile, request.Posting);
        return letter;
    }
}
