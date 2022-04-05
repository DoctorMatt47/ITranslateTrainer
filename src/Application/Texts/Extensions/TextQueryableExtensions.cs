using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Texts.Extensions;

public static class TextQueryableExtensions
{
    public static IQueryable<Text> GetRandomCanBeOption(this IQueryable<Text> texts, Language language, int count) =>
        texts.Where(t => t.CanBeOption && t.Language == language).Shuffle().Take(count);

    public static IQueryable<Text> GetRandomCanBeTested(this IQueryable<Text> texts, Language language, int count) =>
        texts.Where(t => t.CanBeTested && t.Language == language).Shuffle().Take(count);
}
