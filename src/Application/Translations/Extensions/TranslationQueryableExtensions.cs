using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Extensions;

internal static class TranslationDbSetExtensions
{
    public static Task<Translation?> FindByTexts(
        this IQueryable<Translation> translations,
        Text first,
        Text second,
        CancellationToken cancellationToken)
    {
        return translations.FirstOrDefaultAsync(
            t => (t.First == first && t.Second == second) || (t.First == second && t.Second == first),
            cancellationToken);
    }
}
