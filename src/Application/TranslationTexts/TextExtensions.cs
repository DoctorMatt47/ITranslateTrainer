using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.TranslationTexts;

internal static class TextExtensions
{
    public static IQueryable<TranslationText> GetRandomCanBeOption(
        this IQueryable<TranslationText> texts,
        string language,
        int count)
    {
        return texts
            .Where(t => t.Language == language.ToLowerInvariant())
            .Shuffle()
            .Take(count);
    }
}
