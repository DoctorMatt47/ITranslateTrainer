using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Texts;

internal static class TextExtensions
{
    public static IQueryable<TranslationText> GetRandomCanBeOption(
        this IQueryable<TranslationText> texts,
        string language,
        int count)
    {
        return texts
            .Where(t => t.CanBeOption && t.Language == language.ToLowerInvariant())
            .Shuffle()
            .Take(count);
    }

    public static IQueryable<TranslationText> GetRandomCanBeTested(
        this IQueryable<TranslationText> texts,
        string language,
        int count)
    {
        return texts
            .Where(t => t.CanBeTested && t.Language == language.ToLowerInvariant())
            .Shuffle()
            .Take(count);
    }
}
