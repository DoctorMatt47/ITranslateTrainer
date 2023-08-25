using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.TranslationTexts;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

internal record GetOrCreateTranslation(
        string OriginText,
        string OriginLanguage,
        string TranslationText,
        string TranslationLanguage)
    : IRequest<Translation>;

internal class GetOrCreateTranslationHandler : IRequestHandler<GetOrCreateTranslation, Translation>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public GetOrCreateTranslationHandler(ITranslateDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Translation> Handle(GetOrCreateTranslation request, CancellationToken cancellationToken)
    {
        var (originText, originLanguage, translationText, translationLanguage) = request;

        var firstTextRequest = new GetOrCreateText(originText, originLanguage);
        var firstText = await _mediator.Send(firstTextRequest, cancellationToken);

        var secondTextRequest = new GetOrCreateText(translationText, translationLanguage);
        var secondText = await _mediator.Send(secondTextRequest, cancellationToken);

        var translation = await _context.Set<Translation>().FindByTexts(firstText, secondText, cancellationToken);

        if (translation is not null) return translation;

        var translationToAdd = new Translation
        {
            OriginText = firstText,
            TranslationText = secondText,
        };

        await _context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}
