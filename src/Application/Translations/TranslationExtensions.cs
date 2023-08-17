using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

internal static class TranslationExtensions
{
    public static Task<Translation?> FindByTexts(
        this IQueryable<Translation> translations,
        TranslationText originalText,
        TranslationText translationText,
        CancellationToken cancellationToken)
    {
        return translations.FirstOrDefaultAsync(
            t => (t.OriginText == originalText && t.TranslationText == translationText)
                || (t.OriginText == translationText && t.TranslationText == originalText),
            cancellationToken);
    }
}
