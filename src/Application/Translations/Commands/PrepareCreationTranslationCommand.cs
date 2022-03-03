using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Commands;

public record PrepareCreationTranslationCommand(CreateTextCommand FirstText, CreateTextCommand SecondText) :
    IRequest<Translation>;

public record PrepareCreationTranslationCommandHandler(ITranslateDbContext Context) :
    IRequestHandler<PrepareCreationTranslationCommand, Translation>
{
    public async Task<Translation> Handle(PrepareCreationTranslationCommand request,
        CancellationToken cancellationToken)
    {
        var ((firstString, firstLanguage), (secondString, secondLanguage)) = request;

        if (firstLanguage == secondLanguage) throw new BadRequestException("Languages are the same");

        var firstText = await Context.Texts.FirstOrDefaultAsync(
            t => t.String == firstString && t.Language == firstLanguage,
            cancellationToken);

        var secondText = await Context.Texts.FirstOrDefaultAsync(
            t => t.String == secondString && t.Language == secondLanguage,
            cancellationToken);

        if (firstText is not null && secondText is not null)
        {
            var translation = await Context.Translations.FirstOrDefaultAsync(
                t => t.First == firstText && t.Second == secondText
                    || t.Second == secondText && t.First == firstText,
                cancellationToken);
            if (translation is not null) return translation;
        }

        if (firstText is null)
        {
            firstText = new Text {String = firstString, Language = firstLanguage};
            await Context.Texts.AddAsync(firstText, cancellationToken);
        }

        if (secondText is null)
        {
            secondText = new Text {String = secondString, Language = secondLanguage};
            await Context.Texts.AddAsync(secondText, cancellationToken);
        }

        var translationToAdd = new Translation {First = firstText, Second = secondText};
        await Context.Translations.AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}
