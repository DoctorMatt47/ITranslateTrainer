using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.TranslationTexts;

public record GetOrCreateText(
        string String,
        string Language)
    : IRequest<Text>;

internal class GetOrCreateTextHandler : IRequestHandler<GetOrCreateText, Text>
{
    private readonly ITranslateDbContext _context;

    public GetOrCreateTextHandler(ITranslateDbContext context) => _context = context;

    public async Task<Text> Handle(GetOrCreateText request, CancellationToken cancellationToken)
    {
        var (text, language) = request;

        var translationText = await FindInLocalOrInDb(_context.Set<Text>());

        if (translationText is not null) return translationText;

        var newText = new Text
        {
            Value = text,
            Language = language,
        };

        await _context.Set<Text>().AddAsync(newText, cancellationToken);

        return newText;

        // Tries to find in local, if not, requests database.
        // It is necessary for bulk addition to prevent duplicates.
        async Task<Text?> FindInLocalOrInDb(DbSet<Text> texts)
        {
            var textsInLocal = texts.Local.FirstOrDefault(t =>
                t.Value == text.ToLowerInvariant() && t.Language == language.ToLowerInvariant());

            if (textsInLocal is not null) return textsInLocal;

            return await texts.FirstOrDefaultAsync(
                t => t.Value == text.ToLowerInvariant() && t.Language == language.ToLowerInvariant(),
                cancellationToken);
        }
    }
}
