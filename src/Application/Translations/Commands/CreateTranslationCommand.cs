using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Commands;

public record CreateTranslationCommand(CreateTextRequest FirstText, CreateTextRequest SecondText) :
    IRequest<IntIdResponse>;

public record CreateTranslationCommandHandler(ITranslateDbContext Context) :
    IRequestHandler<CreateTranslationCommand, IntIdResponse>
{
    public async Task<IntIdResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var ((firstString, firstLanguage), (secondString, secondLanguage)) = request;

        var firstText = await Context.Texts.FirstOrDefaultAsync(
            t => t.String == firstString && t.Language == firstLanguage,
            cancellationToken);

        var secondText = await Context.Texts.FirstOrDefaultAsync(
            t => t.String == secondString && t.Language == secondLanguage,
            cancellationToken);

        if (firstText is not null && secondText is not null)
        {
            var translation = await Context.Translations.FirstOrDefaultAsync(
                t => t.First == firstText && t.Second == secondText,
                cancellationToken);
            if (translation is not null) return new IntIdResponse(translation.Id);
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

        await Context.SaveChangesAsync();
        return new IntIdResponse(translationToAdd.Id);
    }
}