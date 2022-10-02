using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Texts.Extensions;

public static class TextQueryableExtensions
{
    public static IQueryable<Text> GetRandomCanBeOption(this IQueryable<Text> texts, string language, int count)
    {
        return texts.Where(t => t.CanBeOption && t.Language == language.ToLowerInvariant()).Shuffle().Take(count);
    }

    public static IQueryable<Text> GetRandomCanBeTested(this IQueryable<Text> texts, string language, int count)
    {
        return texts.Where(t => t.CanBeTested && t.Language == language.ToLowerInvariant()).Shuffle().Take(count);
    }
}