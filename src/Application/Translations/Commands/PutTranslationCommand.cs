using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record PutTranslationCommand(TextString FirstText, Language FirstLanguage, TextString SecondText,
    Language SecondLanguage) : IRequest<UintIdResponse>;

public record CreateTranslationCommandHandler(IMediator _mediator, ITranslateDbContext _context) :
    IRequestHandler<PutTranslationCommand, UintIdResponse>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMediator _mediator = _mediator;

    public async Task<UintIdResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getTranslation = new GetOrCreateTranslationRequest(firstText, firstLanguage, secondText, secondLanguage);
        var translation = await _mediator.Send(getTranslation, cancellationToken);

        await _context.SaveChangesAsync();

        return new UintIdResponse(translation.Id);
    }
}
