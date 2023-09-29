using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.TranslationTexts;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record GetOrCreateTranslation(
    string OriginText,
    string OriginLanguage,
    string TranslationText,
    string TranslationLanguage) : IRequest<Translation>;

public class GetOrCreateTranslationHandler(
    ITranslateDbContext context,
    ISender mediator) : IRequestHandler<GetOrCreateTranslation, Translation>
{
    public async Task<Translation> Handle(GetOrCreateTranslation request, CancellationToken cancellationToken)
    {
        var (originText, originLanguage, translationText, translationLanguage) = request;

        var firstTextRequest = new GetOrCreateText(originText, originLanguage);
        var firstText = await mediator.Send(firstTextRequest, cancellationToken);

        var secondTextRequest = new GetOrCreateText(translationText, translationLanguage);
        var secondText = await mediator.Send(secondTextRequest, cancellationToken);

        var translation = await context.Set<Translation>().FindByTexts(firstText, secondText, cancellationToken);

        if (translation is not null) return translation;

        var translationToAdd = new Translation
        {
            OriginText = firstText,
            TranslationText = secondText,
        };

        await context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}
