// ReSharper disable RedundantDefaultMemberInitializer

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Text : HasId<int>
{
    private readonly List<Translation> _translations = new();

    public required string Value { get; set; }
    public required string Language { get; init; }

    public IEnumerable<Translation> Translations => _translations.AsReadOnly();

    public IEnumerable<Text> GetTranslationTexts()
    {
        var firstTexts = _translations
            .Where(t => t.OriginTextId == Id)
            .Select(t => t.TranslationText);

        var secondTexts = _translations
            .Where(t => t.TranslationTextId == Id)
            .Select(t => t.OriginText);

        return firstTexts.Concat(secondTexts);
    }
}
