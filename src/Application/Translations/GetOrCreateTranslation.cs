using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record GetOrCreateTranslation(
    TextRequest OriginText,
    TextRequest TranslationText)
    : IRequest<Translation>;

public class GetOrCreateTranslationHandler(
    IAppDbContext context,
    ISender mediator)
    : IRequestHandler<GetOrCreateTranslation, Translation>
{
    public async Task<Translation> Handle(GetOrCreateTranslation request, CancellationToken cancellationToken)
    {
        var ((originText, originLanguage), (translationText, translationLanguage)) = request;

        var firstText = await mediator.Send(
            new GetOrCreateText(originText, originLanguage),
            cancellationToken);

        var secondText = await mediator.Send(
            new GetOrCreateText(translationText, translationLanguage),
            cancellationToken);

        var translation = await context.Set<Translation>().FindByTexts(firstText, secondText, cancellationToken);

        if (translation is not null)
        {
            return translation;
        }

        var translationToAdd = new Translation
        {
            OriginText = firstText,
            TranslationText = secondText,
        };

        await context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}
