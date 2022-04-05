using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Texts.Extensions;

public static class TextExtensions
{
    public static IEnumerable<Text> GetTranslationTexts(this Text text) =>
        text.Translations.Select(t => t.First == text ? t.Second : t.First);
}
