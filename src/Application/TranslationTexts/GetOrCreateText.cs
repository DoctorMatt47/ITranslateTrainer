﻿using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.TranslationTexts;

public record GetOrCreateText(
        string Text,
        string Language)
    : IRequest<Text>;

internal class GetOrCreateTextHandler : IRequestHandler<GetOrCreateText, Text>
{
    private readonly ITranslateDbContext _context;

    public GetOrCreateTextHandler(ITranslateDbContext context) => _context = context;

    public async Task<Text> Handle(GetOrCreateText request, CancellationToken cancellationToken)
    {
        var text = await FindInLocalOrInDb(_context.Set<Text>());

        if (text is not null) return text;

        text = new Text
        {
            Value = request.Text,
            Language = request.Language,
        };

        await _context.Set<Text>().AddAsync(text, cancellationToken);

        return text;

        // Tries to find in local, if not, requests database.
        // It is necessary for bulk addition to prevent duplicates.
        async Task<Text?> FindInLocalOrInDb(DbSet<Text> texts)
        {
            var textsInLocal = texts.Local.FirstOrDefault(t =>
                t.Value == request.Text.ToLowerInvariant() && t.Language == request.Language.ToLowerInvariant());

            if (textsInLocal is not null) return textsInLocal;

            return await texts.FirstOrDefaultAsync(
                t => t.Value == request.Text.ToLowerInvariant() && t.Language == request.Language.ToLowerInvariant(),
                cancellationToken);
        }
    }
}
