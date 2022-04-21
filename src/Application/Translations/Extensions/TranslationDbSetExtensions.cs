using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Extensions;

public static class TranslationDbSetExtensions
{
    public static Task<Translation?> FindByTexts(this DbSet<Translation> translations, Text first, Text second,
        CancellationToken cancellationToken) =>
        translations.FirstOrDefaultAsync(
            t => t.First == first && t.Second == second
                || t.First == second && t.Second == first,
            cancellationToken);
}