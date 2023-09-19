﻿using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public record DeleteTranslationCommand(int Id) : IRequest;

internal class DeleteTranslationCommandHandler(ITranslateDbContext context) : IRequestHandler<DeleteTranslationCommand>
{
    public async Task Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translationToDelete = await context.Set<Translation>()
            .Include(t => t.OriginText)
            .Include(t => t.TranslationText)
            .FirstAsync(t => t.Id == request.Id, cancellationToken);

        context.Set<Translation>().Remove(translationToDelete);

        var firstTextTranslations = translationToDelete.OriginText.GetTranslationTexts();

        if (firstTextTranslations.Count() <= 1)
        {
            context.Set<Text>().Remove(translationToDelete.OriginText);
        }

        var secondTextTranslations = translationToDelete.TranslationText.GetTranslationTexts();

        if (secondTextTranslations.Count() <= 1)
        {
            context.Set<Text>().Remove(translationToDelete.TranslationText);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
