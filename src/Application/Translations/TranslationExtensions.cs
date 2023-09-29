using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public static class TranslationExtensions
{
    public static Task<Translation?> FindByTexts(
        this IQueryable<Translation> translations,
        Text originalText,
        Text text,
        CancellationToken cancellationToken)
    {
        return translations.FirstOrDefaultAsync(
            t => t.OriginText == originalText
                && t.TranslationText == text
                || t.OriginText == text
                && t.TranslationText == originalText,
            cancellationToken);
    }
}
