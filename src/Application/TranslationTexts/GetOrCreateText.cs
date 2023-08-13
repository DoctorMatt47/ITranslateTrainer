using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.TranslationTexts;

public record GetOrCreateText(
        string String,
        string Language)
    : IRequest<TranslationText>;

internal class GetOrCreateTextHandler : IRequestHandler<GetOrCreateText, TranslationText>
{
    private readonly ITranslateDbContext _context;

    public GetOrCreateTextHandler(ITranslateDbContext context) => _context = context;

    public async Task<TranslationText> Handle(GetOrCreateText request, CancellationToken cancellationToken)
    {
        var (textString, language) = request;

        var text = await FindInLocalOrInDb(_context.Set<TranslationText>());

        if (text is not null) return text;

        var newText = new TranslationText(textString, language);
        await _context.Set<TranslationText>().AddAsync(newText, cancellationToken);

        return newText;

        // Tries to find in local, if not, requests database.
        // It is necessary for bulk addition to prevent duplicates.
        async Task<TranslationText?> FindInLocalOrInDb(DbSet<TranslationText> texts)
        {
            var textsInLocal = texts.Local.FirstOrDefault(t =>
                t.Text == textString.ToLowerInvariant() && t.Language == language.ToLowerInvariant());

            if (textsInLocal is not null) return textsInLocal;

            return await texts.FirstOrDefaultAsync(
                t => t.Text == textString.ToLowerInvariant() && t.Language == language.ToLowerInvariant(),
                cancellationToken);
        }
    }
}
