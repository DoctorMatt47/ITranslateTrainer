using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public record DeleteTranslationCommand(int Id) : IRequest;

public class DeleteTranslationCommandHandler(IAppDbContext context) : IRequestHandler<DeleteTranslationCommand>
{
    public async Task Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translation = await context.Set<Translation>()
            .Include(t => t.OriginText)
            .ThenInclude(t => t.Translations)
            .Include(t => t.TranslationText)
            .ThenInclude(t => t.Translations)
            .FirstByIdOrThrowAsync(request.Id, cancellationToken);

        context.Set<Translation>().Remove(translation);

        var originTextUsed = await AnyOtherTranslationWithTextId(
            translation.OriginTextId,
            request.Id,
            cancellationToken);

        var translationTextUsed = await AnyOtherTranslationWithTextId(
            translation.TranslationTextId,
            request.Id,
            cancellationToken);

        if (!originTextUsed)
        {
            context.Set<Text>().Remove(translation.OriginText);
        }

        if (!translationTextUsed)
        {
            context.Set<Text>().Remove(translation.TranslationText);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> AnyOtherTranslationWithTextId(
        int textId,
        int excludeTranslationId,
        CancellationToken cancellationToken)
    {
        return await context.Set<Translation>()
            .AnyAsync(
                t => (t.OriginTextId == textId
                        || t.TranslationTextId == textId)
                    && t.Id != excludeTranslationId,
                cancellationToken);
    }
}
